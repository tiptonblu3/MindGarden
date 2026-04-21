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
        if (!hasTriggered && other.CompareTag("Player"))
        {
            checkPointReturner.CurrentCheckPointIndex = checkpointIndex;
            hasTriggered = true;

            checkPointReturner.SetCheckpoint(checkpointIndex);
        }
    }

    #endregion
}
