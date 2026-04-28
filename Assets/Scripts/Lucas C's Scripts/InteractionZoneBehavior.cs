using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionZoneBehavior : MonoBehaviour
{
    private bool interactivityCheck; //used to check if the player is in range of the valve
    public GameObject pipeObject; // Reference to the pipe object that will be rotated
    void Start()
    {
        
    }

    void Update()
    {

        if (interactivityCheck && InputSystem.actions["Interact"].triggered)
        {
            pipeObject.GetComponent<WaterPipeBehavior>().TogglePipe();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) interactivityCheck = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) interactivityCheck = false;
    }
}
