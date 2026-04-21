using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenuCam;
    [SerializeField] private CinemachineCamera freeLookCam;
    [SerializeField] private ThoughtBubbleChain chain;

    [Header("Controller Navigation")]
    [SerializeField] private Button pauseMenuFirstButton;
    [SerializeField] private Button settingsMenuFirstButton;
    [SerializeField] private UnityEngine.InputSystem.UI.InputSystemUIInputModule uiInputModule;

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
        // Keyboard pause input
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        // Controller pause input
        if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)
        {
            if (isPaused) Resume();
            else Pause();
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
        pauseMenu.SetActive(true);
        pauseMenuCam.SetActive(true);
        freeLookCam.enabled = false; // This will freeze the main cam while the pause menu is active to prevent that weird spin when unpausing

        chain.Activate();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Locks jumping and menu navigation with thumbstick in pause menu with controller
        if (Gamepad.current != null)
        {
            playerMovement.GetComponent<PlayerInput>().actions["Jump"].Disable();

            var navigate = uiInputModule.actionsAsset.FindAction("UI/Navigate");
            foreach (var binding in navigate.bindings)
            {
                // Stops thumbstick navigation in pause menu
                if (binding.path.Contains("leftStick") || binding.path.Contains("rightStick"))
                    navigate.ApplyBindingOverride(new InputBinding { path = binding.path, overridePath = "" });
            }
        }

        // Sets the highlighted button when you open the menu every time to always be the same
        StartCoroutine(SelectFirstButton(pauseMenuFirstButton));
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuCam.SetActive(false);
        freeLookCam.enabled = true; 

        if (playerMovement != null)
            playerMovement.enabled = true;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Unlocks jumping in pause menu with controller
        if (Gamepad.current != null)
        {
            playerMovement.GetComponent<PlayerInput>().actions["Jump"].Enable();

            var navigate = uiInputModule.actionsAsset.FindAction("UI/Navigate");
            navigate.RemoveAllBindingOverrides();
        }
    }

    // Both set the highlighted button when you open or close the settings menu to be the same
    public void OnOpenSettings()
    {
        StartCoroutine(SelectFirstButton(settingsMenuFirstButton));
    }

    public void OnCloseSettings()
    {
        StartCoroutine(SelectFirstButton(pauseMenuFirstButton));
    }

    private IEnumerator SelectFirstButton(Button button)
    {
        yield return null;

        if (button != null)
            EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

}