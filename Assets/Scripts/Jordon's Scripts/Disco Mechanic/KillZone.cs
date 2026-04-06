using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Drag your Respawn Point object into this slot in the Inspector
    public Transform respawnPoint;
    public RythmGameSetup gameScript;
    public GameObject[] LevelPieces; //Things that will be turned off when the player enters the trigger

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the thing hitting the zone is the Player
        if (other.CompareTag("Player"))
        {
            if (respawnPoint != null)
            {
                // 2. Move the Player (other) to the respawn point
                other.transform.position = respawnPoint.position;

                // 3. Stop physics momentum so they don't keep falling speed
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            else
            {
                
            }
            for (int i = 0; i < LevelPieces.Length; i++)
        {
            LevelPieces[i].SetActive(false);
        }
        }
        gameScript.RemoveOldPlatform();
    }
}