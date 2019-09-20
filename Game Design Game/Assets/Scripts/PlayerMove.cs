using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 50;

    public float jumpVelocity = 5f;

    private Rigidbody rigidBody;
    private bool onGround = true;

    private float lastDashTime = -1;
    private bool isDashing = false;
    private int dashDirection = 0;
    public float dashCooldown = 1.0f;
    public float dashRange = 500;
    public float dashDuration = 0.003f;
    public float playerRotation = 0;

    private float distanceFromOrigin = 0;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        distanceFromOrigin = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Keep the camera at origin, constantly track player from origin   
        var camera = GetComponentInChildren<Camera>();
        var idealPosition = Vector3.zero + Vector3.up * transform.position.y;
        camera.transform.position = idealPosition + Vector3.up * 0.5f;
        camera.transform.LookAt(transform.position);
        transform.LookAt(idealPosition);
        // Keep the position always the same distance from origin
        transform.position = new Vector3(transform.position.x, 0, transform.position.z).normalized * distanceFromOrigin + 
            new Vector3(0, transform.position.y, 0);

        float moveHorizontal = Input.GetAxis("Horizontal");
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, Vector3.zero);

        if (Input.GetKeyDown("z") && onGround)//jump
        {
            rigidBody.AddForce(Vector3.up * jumpVelocity * Time.deltaTime, ForceMode.Impulse);
            onGround = false;
        }

        if (Input.GetKeyDown("c") && 
            ((Time.time - lastDashTime) >= dashCooldown) && 
            !isDashing && 
            moveHorizontal != 0) //dash
        {
            lastDashTime = Time.time;
            isDashing = true;
            dashDirection = (moveHorizontal > 0) ? 1 : -1;

            //rigidBody.AddForce(Vector3.up * 1.5f * Time.deltaTime, ForceMode.Impulse);
            //transform.RotateAround(Vector3.zero, Vector3.up, moveHorizontal * speed * dashRange * Time.deltaTime);
            //canDash = false;
        }

        if ((Time.time - lastDashTime) < dashDuration && isDashing)
        {
            float distanceThisFrame = dashRange * (Time.deltaTime / dashDuration);
            transform.RotateAround(Vector3.zero, Vector3.up, dashDirection * distanceThisFrame);
            rigidBody.useGravity = false;
            // set accel to zero
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
        else
        {
            // perform movement as normal
            isDashing = false;
            rigidBody.useGravity = true;
            transform.RotateAround(Vector3.zero, Vector3.up, moveHorizontal * speed * Time.deltaTime);
        }

        playerRotation = rigidBody.transform.rotation.eulerAngles.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.impulse);
        if (collision.impulse.y > 0)
        {
            onGround = true;
        }
        isDashing = false;
        // canDash = true;
    }

    private void OnCollisionStay(Collision collision)
    {

    }
}
