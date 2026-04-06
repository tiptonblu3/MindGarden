using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    public Player_Movement playerScript;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            Debug.Log("Player entered interaction range");
            playerScript.currentInteractable = interactable;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (playerScript.currentInteractable == interactable)
            {
                playerScript.currentInteractable = null;
                Debug.Log("Player exited interaction range");
            }
        }
    }
    
}
