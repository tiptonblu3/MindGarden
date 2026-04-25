using UnityEngine;

public class LV3Disc2 : MonoBehaviour,  IInteractable
{
    
    public CheckPointReturner cpr;
    private Collider col;
    public Material Skybox_Alt;
    public bool eyechange = false;

    private void Start()
    {
        // If you forgot to drag it in, this will try to find it for you
        if (cpr == null)
        {
            cpr = FindAnyObjectByType<CheckPointReturner>();
        }
        col = GetComponent<Collider>();
    }
    public void Interact()
    {
        Debug.Log("Record Piece Picked Up!");
        cpr.DiscNum = 2; // Increment the discs depending on how many were grabbed
        //Raise Void to right below checkpoint here
        eyechange = true;
        if (col != null) col.enabled = true;
        //if (rend != null) rend.enabled = true;

        if (Skybox_Alt != null)
        {
            Debug.Log("Skybox switch triggered");
            RenderSettings.skybox = Skybox_Alt;
            DynamicGI.UpdateEnvironment();
        }
        gameObject.SetActive(false); // This will disable the record object
    }
    
}


