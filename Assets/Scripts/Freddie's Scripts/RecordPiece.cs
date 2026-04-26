using UnityEngine;

public class RecordPiece : MonoBehaviour, IInteractable
{
    public bool IsFloating = true; // Option to enable or disable floating animation
    private Vector3 startPos; // Store the initial position of the record for animation purposes
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
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFloating)
        {
            transform.Rotate(Vector3.up, 50 * Time.deltaTime);
            float bounce = Mathf.Sin(Time.time * 2) * 0.1f;
            transform.position = new Vector3(startPos.x, startPos.y + bounce, startPos.z);
        }
    }
}
