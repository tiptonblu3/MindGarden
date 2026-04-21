using UnityEngine;

public class WaypointMarker : MonoBehaviour
{
    // For controlling the color of the waypoints.
    public Color triggerColorSolid = Color.blue;
    public Color triggerColorOutline = new Color(0, 0, 1, 0.25f);

    // Purely for visual debugging of the waypoints. Colors it blue.
    void OnDrawGizmos()
    {
        Gizmos.color = triggerColorSolid;
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            float radius = col.bounds.extents.magnitude;
            Gizmos.DrawWireSphere(col.bounds.center, radius);
        }

        Gizmos.color = triggerColorOutline;
        Gizmos.DrawSphere(col.bounds.center, col.bounds.extents.magnitude);
    }
}
