using UnityEngine;

public class RecordPlayer : MonoBehaviour, IInteractable
{
    public GameObject RecordSelect;
    //public GameObject cursor;
    public void Interact()
    {
        RecordSelect.SetActive(true);
        Time.timeScale = 0f; // Pause Game
        Cursor.lockState = CursorLockMode.None; // Unlock Cursor
        Cursor.visible = true; // Make Cursor Visible
    }
    public void CloseRecordSelect()
    {
        RecordSelect.SetActive(false);
        Time.timeScale = 1f; // Unpause Game
        Cursor.lockState = CursorLockMode.Locked; // Lock Cursor
        Cursor.visible = false; // Make Cursor Invisible
    }
    public void PlayRecord()
    {
        // Set up portal to level
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
