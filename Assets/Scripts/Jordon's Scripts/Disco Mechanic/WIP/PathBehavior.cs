using UnityEngine;
using System.Numerics;

public class PathBehavior : MonoBehaviour
{
    public UnityEngine.Vector3 originalPosition;
    private Collider myCollider;
    public GameObject MyDirectionalArrow; //for the unique arrow for each game object to point to the next one in the sequence

    void Awake() 
    {
        myCollider = GetComponent<Collider>();
        originalPosition = transform.position; // The origional home spot
        if(MyDirectionalArrow != null) MyDirectionalArrow.SetActive(false);
    }

    public void SetPlatform(bool isCorrect) 
    {
        myCollider.isTrigger = !isCorrect; //if bool is false it turns off the collider
        if (MyDirectionalArrow != null)
        {
            MyDirectionalArrow.SetActive(isCorrect);
            MyDirectionalArrow.transform.position = transform.position + UnityEngine.Vector3.up * 2f;
        }
    }

    public void ResetPlatform() 
    {
        transform.position = originalPosition;
        myCollider.isTrigger = false; // reset to solid for the next round
    }
}