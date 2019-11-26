using UnityEngine;

class CausticsProjector : MonoBehaviour
{
    public int FramesPerSecond = 30;
    public Texture[] causticsTextures = null;

    private new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {

        if (causticsTextures != null && causticsTextures.Length >= 1)
        {
            int causticsIndex = (int)(Time.time * FramesPerSecond) % causticsTextures.Length;
            renderer.sharedMaterial.SetTexture("_EmissionMap", causticsTextures[causticsIndex]);
        }

        var lightDirection = Quaternion.Euler(0, 4 * Time.time, 0) * new Vector3(1, 0, 0);
        var lightMatrix = Matrix4x4.TRS(
          new Vector3(0, 0, 0),
          Quaternion.LookRotation(lightDirection,
                                  new Vector3(lightDirection.z, lightDirection.x, lightDirection.y)),
                                  Vector3.one);
        renderer.sharedMaterial.SetMatrix("_CausticsLightOrientation", lightMatrix);
    }
}
