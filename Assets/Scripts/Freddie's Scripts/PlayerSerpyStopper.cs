using UnityEngine;

public class PlayerSerpyStopper : MonoBehaviour
{
    public Serpy serpy; // Reference to the Serpy script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player has entered the trigger
        {
            serpy.StopSerpy(); // Call the method to stop Serpy's movement
        }
    }
}
