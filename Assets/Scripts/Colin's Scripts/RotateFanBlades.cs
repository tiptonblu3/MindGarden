using UnityEngine;

public class RotateFans : MonoBehaviour
{
    public Transform target; // The object to rotate around
    public float speed = 20f;
    void Awake()
    {
        target = GameObject.Find("Fan_Middle").GetComponent<Transform>();
    }

    void Update() 
    {
        // Position of the center, The axis of rotation, and The speed (degrees per second)
        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
    }
}
