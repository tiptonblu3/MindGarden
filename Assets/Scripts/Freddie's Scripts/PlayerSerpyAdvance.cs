using UnityEngine;

public class PlayerSerpyAdvance : MonoBehaviour
{
    public Serpy serpy; // Reference to the Serpy script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player has entered the trigger
        {
            serpy.AdvanceSerpy(); // Call the method to advance Serpy's behavior
        }
    }
}
