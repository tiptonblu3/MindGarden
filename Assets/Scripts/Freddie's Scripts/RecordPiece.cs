using UnityEngine;

public class RecordPiece : MonoBehaviour, IInteractable
{
    public CheckPoints CheckPoints;
    public GameObject SaveProgressObject;
    private SaveProgress saveProgress;

    void Awake()
    {
        CheckPoints = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<CheckPoints>();
        SaveProgressObject = GameObject.FindGameObjectWithTag("SaveProgress");
        saveProgress = SaveProgressObject.GetComponent<SaveProgress>();
    }


    public void Interact()
    {
        Debug.Log("Record Piece Picked Up!");
        CheckPoints.CurrentCheckPointIndex++; // Increment the check point index
        saveProgress.SaveGame(); // Save the progress
        gameObject.SetActive(false); // This will disable the record object
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
