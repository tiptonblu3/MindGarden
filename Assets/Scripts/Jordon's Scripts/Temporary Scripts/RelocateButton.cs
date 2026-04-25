using UnityEngine;

public class RelocateButton : MonoBehaviour, IInteractable
{
    public GameObject playerObject; //reference to the player object to move them to the start point
    public Vector3 StartPosition = new Vector3(0, 0, 0);
   void Awake()
    {
        GetComponent<Renderer>().material.color = Color.aquamarine;
    }
    public void Interact()
    {
        Debug.Log("End Button Pressed! Moving player");
        //here it would move the player and start the script for the disco platform sequence
        playerObject.transform.position = StartPosition;
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null) {
                    rb.linearVelocity = Vector3.zero; // Stop falling/moving
                    rb.position = StartPosition;      // Teleport the physics body
                }
        
    }
}
