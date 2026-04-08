using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public RecordPlayer Record;
    public GameObject SettingsMenu;
    
    [SerializeField] private Player_Movement PlayerMovement;

    private bool isPaused;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    private void Pause()
    { 
        if (Record != null){
        Record.CloseRecordSelect(); // Ensure the record selection menu is closed when pausing
        }
        isPaused = true;
        Time.timeScale = 0f;
        // Stop player movement.
        if (PlayerMovement != null)
            PlayerMovement.enabled = false;

        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (PlayerMovement != null)
            PlayerMovement.enabled = true;

        pauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}