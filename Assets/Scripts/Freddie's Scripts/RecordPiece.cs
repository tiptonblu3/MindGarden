using UnityEngine;

public class RecordPiece : MonoBehaviour, IInteractable
{
    public CheckPoints CheckPoints;

    void Awake()
    {
        CheckPoints = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<CheckPoints>();
    }

    public void Interact()
    {
        if (gameObject.activeInHierarchy)
        {
            Debug.Log("Record Piece Picked Up!");
            CheckPoints.CurrentCheckPointIndex++; // Increment the check point index
            gameObject.SetActive(false); // This will disable the record object
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
