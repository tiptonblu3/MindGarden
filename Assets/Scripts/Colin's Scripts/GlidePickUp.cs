using UnityEngine;

public class GlidePickUp : MonoBehaviour
{
    // References
    #region

    private GliderState playerInRange;

    #endregion

    // OnTriggerEnter
    #region

    // Detects if the player is touching the Glide
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GliderState>(out GliderState player))
        {
            playerInRange = player;
        }
    }

    #endregion

    //
}