using UnityEngine;

public class EyeKill : MonoBehaviour
{
    // Variables & References
    #region

    // Reference
    public CheckPointReturner checkPoint;
    public Transform PlayerTransform;
    public GameObject RespawnRefCube;

    #endregion

    // OnCollisionEnter
    #region

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
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
}
