using UnityEngine;

public class Checkpoint4Bool : MonoBehaviour
{
    public EyeWaypoints eyeWaypoints;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            eyeWaypoints.finalCheckpointReached = true;
        }
    }
}
