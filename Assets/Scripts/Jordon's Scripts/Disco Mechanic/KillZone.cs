using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Drag your Respawn Point object into this slot in the Inspector
    public Player_Movement playermove;
    public bool iFell;
    public PlatformManager platmanscript;
    public discoDialogue discoScript;

    

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the thing hitting the zone is the Player
        if (other.CompareTag("Player"))
        {
            playermove.isDead = true; // Set the player's isDead flag to true
            platmanscript.ResetAllPlatforms();
            iFell = true;
            discoScript.subIndex = 0;
            discoScript.dialogueIndex = 1;
        }
    }
}