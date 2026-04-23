using UnityEngine;

public class LV3Disc3 : MonoBehaviour,  IInteractable
{
    
    public CheckPointReturner cpr;
    public EyeBehavior eye;
    public GameObject EndTrigger;
    public BirdBehavior Birbah;
    private void Start()
    {
        // If you forgot to drag it in, this will try to find it for you
        if (cpr == null)
        {
            cpr = FindAnyObjectByType<CheckPointReturner>();
        }
        if (eye == null) eye = FindAnyObjectByType<EyeBehavior>();
    }
    public void Interact()
    {
        Debug.Log("Record Piece Picked Up!");
        cpr.DiscNum = 3; // Increment the discs depending on how many were grabbed
        if (eye != null)
        {
            eye.StartCoroutine(eye.Checkpoint4Sequence());
        }
        EndTrigger.SetActive(true);
        gameObject.SetActive(false); // This will disable the record object
        Birbah.moveSpeed = 20;
    }
    
}


