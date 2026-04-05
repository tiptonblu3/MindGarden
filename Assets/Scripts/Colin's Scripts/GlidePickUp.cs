using UnityEngine;

public class GlidePickUp : MonoBehaviour, IInteractable
{

    // Variables
    #region

    // Makes sure the power up can only be picked up once
    public bool canBePickedUp = true;
    public bool IsGlideUnlocked = false;

    // Makes object intangable when picked up (Not destroyed so it can still be referenced)
    private Collider objectCollider;
    private Renderer objectRenderer;

    #endregion

    // Awake
    #region

    // Gets the components
    private void Awake()
    {
        objectCollider = GetComponent<Collider>();
        objectRenderer = GetComponent<Renderer>();
    }

    #endregion

    // Interact
    #region

    // Checks if the player can pick up the glide
    // If grabbed, disable
    public void Interact()
    {
        if (!canBePickedUp)
        {
            Debug.Log("Cannot pick up");
            return;
        }

        Debug.Log("Picked up!");

        DisableObject();
    }

    #endregion

    // DisableObject
    #region

    // Disables object visually when grabbed
    private void DisableObject()
    {
        canBePickedUp = false;
        IsGlideUnlocked = true;
        
        if (objectRenderer != null)
            objectRenderer.enabled = false;

        if (objectCollider != null)
            objectCollider.enabled = false;
    }

    #endregion
}