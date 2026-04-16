using UnityEngine;

public class GlideKiller : MonoBehaviour
{
    // Variables & References
    #region

    // Reference to the GlidePickUp
    public GlidePickUp glidePickup;

    // For controlling the color of the trigger.
    public Color triggerColorSolid = Color.green;
    public Color triggerColorOutline = new Color(0, 1, 0, 0.25f);

    #endregion

    // OnTriggerEnter
    #region

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        glidePickup.ReEnableObject();
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
    }

    #endregion
}
