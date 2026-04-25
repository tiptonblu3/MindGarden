using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBehavior : MonoBehaviour
{
    // Variables & References
    #region

    private Collider col;
    private Renderer rend;
    public CheckPointReturner OnCheckpointChanged;

    #endregion

    // Awake
    #region

    private void Awake()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        if (OnCheckpointChanged == null)
        {
            OnCheckpointChanged = FindAnyObjectByType<CheckPointReturner>();
        }    
    }

    #endregion

    // OnEnable
    #region

    private void OnEnable()
    {

        CheckPointReturner.OnCheckpointChanged -= HandleCheckpoint;
     }

    #endregion

    // OnDisable
    #region

    private void OnDisable()
    {
        CheckPointReturner.OnCheckpointChanged -= HandleCheckpoint;
    }

    #endregion

    // HandleCheckpoint
    #region

    private void HandleCheckpoint(int index)
    {
        if (index == 1)
        {
            if (col != null) col.enabled = false;
            if (rend != null) rend.enabled = false;
        }
    }

    #endregion
}
