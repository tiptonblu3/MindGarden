using UnityEngine;

public class RotateFans : MonoBehaviour
{
    public Transform target; // The object to rotate around
    public float speed = 20f;
    void Awake()
    {
        target = transform.parent.Find("Fan_Middle");
    }

    void Update() 
    {
        // Position of the center, The axis of rotation, and The speed (degrees per second)
        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
    }
}
