using UnityEngine;

public class FallDeath : MonoBehaviour
{
    // Variables & References
    #region

    // Reference
    public CheckPointReturner checkPoint;
    public Transform PlayerTransform;
    public GameObject RespawnRefCube;

    // For controlling the color of the trigger.
    public Color triggerColorSolid = Color.red;
    public Color triggerColorOutline = new Color(1, 0, 0, 0.25f);

    #endregion

    // OnTriggerEnter
    #region

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentCheckPoint = checkPoint.GetCurrentCheckpointIndex();

            if (currentCheckPoint >= 0)
            {
                checkPoint.Respawn();
            } 
            else
            {
                NoCheckRespawn();
            }
        }
    }

    #endregion

    // NoCheckRespawn
    #region

    void NoCheckRespawn()
    {
        PlayerTransform.position = RespawnRefCube.transform.position;
    }

    #endregion

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
