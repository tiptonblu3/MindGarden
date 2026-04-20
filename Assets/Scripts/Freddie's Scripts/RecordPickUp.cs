using UnityEngine;

public class RecordPickUp : MonoBehaviour, IInteractable
{
    public RecordPlayer RecordPlayer;
    public GameObject Level;
    public void Interact()
    {
        Debug.Log("Record Picked Up!");
        RecordPlayer.records.Add(Level);
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
