using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Drag your Respawn Point object into this slot in the Inspector
    public Player_Movement playermove;
    

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the thing hitting the zone is the Player
        if (other.CompareTag("Player"))
        {
            playermove.isDead = true; // Set the player's isDead flag to true
        }
    }
}