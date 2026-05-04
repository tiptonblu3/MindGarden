using UnityEngine;

public class Level3DeathPlane : MonoBehaviour
{
    public Player_Movement player;
    public EyeWaypoints eyeWaypoints;

    // For controlling the color of the trigger.
    public Color triggerColorSolid = Color.red;
    public Color triggerColorOutline = new Color(1, 0, 0, 0.25f);

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        eyeWaypoints = GameObject.FindGameObjectWithTag("Eye").GetComponent<EyeWaypoints>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.isDead = true;
            eyeWaypoints.ResetPosition();
        }
    }

    // OnDrawGizmos
    #region

    // Purely for visual debugging of the death plain's trigger area. Colors it red.
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
