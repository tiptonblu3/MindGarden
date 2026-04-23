using UnityEngine;

public class LV3Disc1 : MonoBehaviour,  IInteractable
{
    
    public CheckPointReturner cpr;
    private void Start()
    {
        // If you forgot to drag it in, this will try to find it for you
        if (cpr == null)
        {
            cpr = FindAnyObjectByType<CheckPointReturner>();
        }
    }
    public void Interact()
    {
        Debug.Log("Record Piece Picked Up!");
        cpr.DiscNum = 1; // Increment the discs depending on how many were grabbed
        //Raise Void to right below checkpoint here
        gameObject.SetActive(false); // This will disable the record object
    }
    
}


