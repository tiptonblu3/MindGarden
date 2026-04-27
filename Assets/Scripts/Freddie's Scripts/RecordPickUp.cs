using UnityEngine;

public class RecordPickUp : MonoBehaviour, IInteractable
{
    public RecordPlayer RecordPlayer;
    public GameObject Level;
    public bool IsFloating = true; // Option to enable or disable floating animation
    private Vector3 startPos; // Store the initial position of the record for animation purposes
    public void Interact()
    {
        Debug.Log("Record Picked Up!");
        RecordPlayer.records.Add(Level);
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
