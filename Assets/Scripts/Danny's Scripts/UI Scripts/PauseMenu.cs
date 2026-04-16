using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseMenuBubble1, pauseMenuBubble2;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenuCam;

    private RecordPlayer record;
    private Player_Movement playerMovement;
    private bool isPaused;

    void Awake()
    {
        record = FindAnyObjectByType<RecordPlayer>();
        playerMovement = FindAnyObjectByType<Player_Movement>();

        if (playerMovement == null)
            Debug.LogWarning("PauseMenu: Player_Movement not found in scene!");
        if (pauseMenu == null)
            Debug.LogWarning("PauseMenu: pauseMenu reference not assigned!");
        if (settingsMenu == null)
            Debug.LogWarning("PauseMenu: settingsMenu reference not assigned!");
    }

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
        if (record != null)
        {
            // Closes the Record Select screen if it's open when the player tries to pause.
            record.CloseRecordSelect();
        }

        isPaused = true;
        /*        Time.timeScale = 0f;

                if (playerMovement != null)
                    playerMovement.enabled = false;*/

        StartCoroutine(ShowUIAfterDelay(0.3f));

        pauseMenu.SetActive(true);
        pauseMenuCam.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        // Time.timeScale = 1f;
        pauseMenuCam.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
            pauseMenuBubble1.SetActive(true);
            pauseMenuBubble2.SetActive(true);
        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator ShowUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        pauseMenu.SetActive(true);
        pauseMenuBubble1.SetActive(true);
        pauseMenuBubble2.SetActive(true);
    }
}