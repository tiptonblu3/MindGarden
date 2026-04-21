using UnityEngine;

public class BuildingTriggerReached : MonoBehaviour
{
    public BirdGuidance bird;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in trigger");
            bird.IncrementWaypoint();
            //  Disable this trigger so isn't reactivatable and doesn't fire twice 
            gameObject.SetActive(false); 
        }
    }
}
