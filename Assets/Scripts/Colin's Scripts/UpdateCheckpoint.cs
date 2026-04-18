using System;
using UnityEngine;

public class UpdateCheckpoint : MonoBehaviour
{
    // Variables & References
    #region

    public int checkpointIndex;
    public CheckPointReturner checkPointReturner;

    #endregion

    // OnTriggerEnter
    #region

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPointReturner.CurrentCheckPointIndex = checkpointIndex;
        }
    }

    #endregion
}
