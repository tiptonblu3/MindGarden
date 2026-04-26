using UnityEngine;

public class PipeTurn : MonoBehaviour, IInteractable
{
    public WaterPipeBehavior pipeBehavior;
    void Start()
    {
    }
    public void Interact()
    {
        Debug.Log("Pipe Activated!");
        //here it would move the player and start the script for the disco platform sequence
        pipeBehavior.TogglePipe();

    }

}
