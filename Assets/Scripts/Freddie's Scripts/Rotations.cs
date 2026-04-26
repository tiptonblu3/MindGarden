using UnityEngine;

public class Rotations : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed of rotation in degrees per second
    public bool rotateX = false; // Option to rotate around the X-axis
    public bool rotateY = false; // Option to rotate around the Y-axis
    public bool rotateZ = false; // Option to rotate around the Z-axis
    public bool floating = false; // Option to enable or disable floating animation
    private Vector3 startPos; // Store the initial position of the object for animation purposes
    private GameObject player; // Reference to the player object
    private GameObject camera; // Reference to the main camera
    public bool lookAtPlayer = false; // Option to enable or disable looking at the player
    public bool lookAtCamera = false; // Option to enable or disable looking at the camera
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Float();
        LookAt();
    }

    void Rotate()
    {
        if (rotateX)
        {
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        if (rotateY)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (rotateZ)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
    void Float()
    {
        if (floating)
        {
            float bounce = Mathf.Sin(Time.time * 2) * 0.1f;
            transform.position = new Vector3(startPos.x, startPos.y + bounce, startPos.z);
        }
    
    }
    void LookAt()
    {
        if (lookAtPlayer && player != null)
        {
            transform.LookAt(player.transform);
        }
        else if (lookAtCamera && camera != null)
        {
            transform.LookAt(camera.transform);
        }
    }
}
