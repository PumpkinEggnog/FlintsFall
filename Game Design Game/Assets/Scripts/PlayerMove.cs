using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 50;

    public float jumpVelocity = 5f;

    private Rigidbody rigidBody;
   
    private HitBoxHandler hitBoxHandler;

    private bool onGround = false;
    private bool isJumping = false;
    private bool isWallJumping = false;

    private bool wallSliding = false;
    private float wallSlideSpeed = 3;

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
        hitBoxHandler = GetComponent<HitBoxHandler>();
        rigidBody = GetComponent<Rigidbody>();
        distanceFromOrigin = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2));
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            if (onGround && !wallSliding)
            {
                isJumping = true;
            }
            else if (wallSliding && !onGround)
            {
                isWallJumping = true;
            }
            
        }

        if (Input.GetKeyDown("c") && 
            ((Time.time - lastDashTime) >= dashCooldown) && 
            !isDashing && 
            Input.GetAxis("Horizontal") != 0) //dash
        {
            hitBoxHandler.setInvincible(true, .5f);
            lastDashTime = Time.time;
            isDashing = true;
            dashDirection = (Input.GetAxis("Horizontal") > 0) ? 1 : -1;

            //rigidBody.AddForce(Vector3.up * 1.5f * Time.deltaTime, ForceMode.Impulse);
            //transform.RotateAround(Vector3.zero, Vector3.up, moveHorizontal * speed * dashRange * Time.deltaTime);
            //canDash = false;
        }
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

        if (isJumping)//jump
        {
            // Debug.Log("Jumping start " + isJumping);
            onGround = false;
            isJumping = false;
            // Debug.Log("Jumping end " + isJumping);

            rigidBody.AddForce(Vector3.up * jumpVelocity * Time.deltaTime, ForceMode.Impulse);
        }

        if (isWallJumping)//jump
        {
            isWallJumping = false;
            // Debug.Log("wall jump activated");
            
            Vector3 wallJumpVector = Vector3.left + (Vector3.up) * jumpVelocity * Time.deltaTime; // left is always the vector facing away from the character facing.
            // Debug.Log(wallJumpVector);
            rigidBody.velocity = new Vector3(0, 0, 0);

            rigidBody.AddForce(wallJumpVector, ForceMode.Impulse);
        }

        /*
        //MORE WALLJUMP/SLIDE STUFF
        if (wallSliding == true)
        {
            //rigidBody.AddForce(1, 3, 1);
            
            
            if (Input.GetKeyDown("z") && Input.GetKeyDown("left"))
            {
                rigidBody.AddForce(Vector3.left + (Vector3.up) * jumpVelocity * Time.deltaTime, ForceMode.Impulse);
                Debug.Log("That was a left wallJump");
            }
            if(Input.GetKeyDown("z") && Input.GetKeyDown("right"))
            {
                rigidBody.AddForce(Vector3.left + (Vector3.up) * jumpVelocity * Time.deltaTime, ForceMode.Impulse);
                Debug.Log("That was a right wallJump");
            }
        }
        //
        */

        // if (Input.GetKeyDown("c") && 
        //     ((Time.time - lastDashTime) >= dashCooldown) && 
        //     !isDashing && 
        //     moveHorizontal != 0) //dash
        // {
        //     lastDashTime = Time.time;
        //     isDashing = true;
        //     dashDirection = (moveHorizontal > 0) ? 1 : -1;

        //     //rigidBody.AddForce(Vector3.up * 1.5f * Time.deltaTime, ForceMode.Impulse);
        //     //transform.RotateAround(Vector3.zero, Vector3.up, moveHorizontal * speed * dashRange * Time.deltaTime);
        //     //canDash = false;
        // }

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
            // resume normal movement
            isDashing = false;
            rigidBody.useGravity = true;
            transform.RotateAround(Vector3.zero, Vector3.up, moveHorizontal * speed * Time.deltaTime);
        }

        playerRotation = rigidBody.transform.rotation.eulerAngles.y;
    }

    public void OnCollisionEnter(Collision collision)
    {
        /*//Debug.Log(collision.impulse);
        if (collision.impulse.y >= 0)
        {
            onGround = true;
        }*/

        // if(collision.gameObject.tag == "floor")
        // {
        //     onGround = true;
        // }

        isDashing = false;
        // canDash = true;
        //Detect collisions between the GameObjects with Colliders attached
        //WallJumping
        if (collision.gameObject.tag == "Floor")
        {
            onGround = true;
            isJumping = false;
            // Debug.Log("Jumping" + isJumping);
            // wallSliding = false;
            // Debug.Log("Wallsliding" + wallSliding);
        }


        if (collision.gameObject.tag == "Wall")
        {
            wallSliding = true;
            //Debug.Log("Wallsliding" + wallSliding);
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onGround = true;
            // Debug.Log("onGround " + onGround);
            // isJumping = false;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        onGround = false;
        // Debug.Log("onGround " + onGround);
        wallSliding = false;
        // Debug.Log("Wallsliding " + wallSliding);

    }

}
