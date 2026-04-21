using System;
using UnityEngine;

public class UpdateCheckpoint : MonoBehaviour
{
    // Variables & References
    #region

    public int checkpointIndex;
    private bool hasTriggered = false;
    public CheckPointReturner checkPointReturner;

    #endregion

    // OnTriggerEnter
    #region

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            // This MUST call SetCheckpoint to trigger the bird!
            checkPointReturner.SetCheckpoint(checkpointIndex);
            Debug.Log("Triggered Checkpoint: " + checkpointIndex);
        }
    }

    #endregion
}
