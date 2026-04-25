using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBehavior : MonoBehaviour
{
    // Variables & References
    #region

    private Collider col;
    private Renderer rend;

    #endregion

    // Awake
    #region

    private void Awake()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    #endregion

    // HandleCheckpoint
    #region

    public void HandleCheckpoint()
    {
        if (col != null) col.enabled = false;
        if (rend != null) rend.enabled = false;
    }

    #endregion
}
