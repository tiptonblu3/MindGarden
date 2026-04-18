using UnityEngine;

public class PipeTurn1 : MonoBehaviour, IInteractable
{
    public WaterPipeBehavior pipeBehavior;
    void Start()
    {
        pipeBehavior = GameObject.Find("TEMPSmallPipe1 Variant").GetComponent<WaterPipeBehavior>();
    }
    public void Interact()
    {
        Debug.Log("Pipe Activated!");
        //here it would move the player and start the script for the disco platform sequence
        pipeBehavior.TogglePipe();

    }

}
