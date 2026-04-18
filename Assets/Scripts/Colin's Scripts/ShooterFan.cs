using System;
using System.Collections;
using UnityEngine;

public class ShooterFan : MonoBehaviour
{
    // Variables
    #region

    // Force of the push
    public float force = 20f;

    // For controlling the color of the trigger.
    public Color triggerColorSolid = Color.green;
    public Color triggerColorOutline = new Color(0, 1, 0, 0.25f);

    #endregion

    // OnTriggerEnter
    #region

    // Just shoots the player in the y axis of fan
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        Player_Movement pm = other.GetComponent<Player_Movement>();

        if (rb == null || pm == null) return;

        pm.isInFan = true;

        rb.useGravity = false;
        rb.linearVelocity = transform.up.normalized * force;
    }

    #endregion

    // OnTriggerExit
    #region

    // Stops it when they leave
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Player_Movement pm = other.GetComponent<Player_Movement>();
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (pm != null)
            pm.isInFan = false;

        if (rb != null)
            rb.useGravity = true;
    }

    #endregion

    // OnDrawGizmos
    #region

    // Purely for visual debugging of the fan's trigger area. Colors it green.
    void OnDrawGizmos()
    {
        Gizmos.color = triggerColorSolid;
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
        }

        Gizmos.color = triggerColorOutline;
        Gizmos.DrawCube(col.bounds.center, col.bounds.size);
        Debug.DrawRay(transform.position, transform.up * 5f, Color.red, 2f);
    }

    #endregion
}
