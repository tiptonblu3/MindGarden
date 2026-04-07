using UnityEngine;

public class DiscoInteract : MonoBehaviour, IInteractable
{
   public RythmGameSetup gameScript;
   void Awake()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
    public void Interact()
    {
        Debug.Log("Disco Time! Moving player");
        //here it would move the player and start the script for the disco platform sequence
        gameScript.GameStart = true;
        
    }
}
