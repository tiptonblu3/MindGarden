using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class RecordPlayer : MonoBehaviour, IInteractable
{
    public GameObject RecordSelect;
    public PauseMenu Pause;

    public void Interact()
    {
        RecordSelect.SetActive(true);
        Time.timeScale = 0f; // Pause Game
        Cursor.lockState = CursorLockMode.None; // Unlock Cursor
        Cursor.visible = true; // Make Cursor Visible
        CurrentRecordIndex = (CurrentRecordIndex) % records.Count;
    }
    public void CloseRecordSelect()
    {
        RecordSelect.SetActive(false);
        Time.timeScale = 1f; // Unpause Game
        Cursor.lockState = CursorLockMode.Locked; // Lock Cursor
        Cursor.visible = false; // Make Cursor Invisible
    }

    public List<GameObject> records; // List of records available to play
    private int CurrentRecordIndex = 0; // Index of the currently selected record
    public TextMeshProUGUI RecordChoiceTxt;
    public GameObject RecordID;
    public GameObject OldRecordID;


    public void NextRecord()
    {
        CurrentRecordIndex = (CurrentRecordIndex + 1) % records.Count; // Move to the next record, loop back to the start if at the end
        Debug.Log("Selected Record: " + records[CurrentRecordIndex]); // Log the currently selected record
        
    }

    public void PreviousRecord()
    {
        if (CurrentRecordIndex == 0)
        {
            CurrentRecordIndex = records.Count - 1; // Move to the last record if currently at the first record
        }
        else
        {
            CurrentRecordIndex = (CurrentRecordIndex - 1) % records.Count; // Move to the Previous record, loop back to the end if at the start
        }
        
        Debug.Log("Selected Record: " + records[CurrentRecordIndex]); // Log the currently selected record
    }

    public void PlayRecord()
    {
        // Set up portal to level
        if (OldRecordID != null)
        {
            OldRecordID.SetActive(false); // Deactivate the previously played record
        }
        Debug.Log("Playing Record: " + records[CurrentRecordIndex]); // Log the currently selected record
        GameObject RecordID = records[CurrentRecordIndex]; // Find the GameObject with the name of the Record
        RecordID.SetActive(true); // Activate the GameObject to play the record
        OldRecordID = RecordID; // Set the currently played record as the old record for the next time a record is played
        CloseRecordSelect(); // Close the record selection menu
    }
    
    
    private void UpdateText()
    {
        RecordChoiceTxt.text = records[CurrentRecordIndex].name;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }
}
