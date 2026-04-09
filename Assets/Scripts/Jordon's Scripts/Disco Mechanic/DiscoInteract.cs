using UnityEngine;

public class DiscoInteract : MonoBehaviour, IInteractable
{
   public PlatformManager platmanscript;
   void Awake()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
    public void Interact()
    {
        Debug.Log("Disco Time! Moving player");
        platmanscript.StartDiscoSequence();
        
    }
}
