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
    [SerializeField] private GameObject pauseMenuContent;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private CinemachineCamera pauseMenuCam;
    [SerializeField] private CinemachineCamera freeLookCam;
    [SerializeField] private ThoughtBubbleChain chain;
    [SerializeField] private GameObject[] subMenus;
    [SerializeField] private PauseCamSync pauseCamSync;

    [Header("Controller Navigation")]
    [SerializeField] private Button pauseMenuFirstButton;
    [SerializeField] private Button settingsMenuFirstButton;
    [SerializeField] private Button hubConfirmFirstButton;
    [SerializeField] private Button quitConfirmFirstButton;
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
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                bool closedAMenu = false;
                foreach (var menu in subMenus)
                {
                    if (menu.activeSelf)
                    {
                        menu.SetActive(false);
                        pauseMenuContent.SetActive(true);
                        closedAMenu = true;
                        break;
                    }
                }
                if (!closedAMenu) Resume();
            }
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
        pauseMenuCam.Priority = 20;
        freeLookCam.Priority = 10;
        chain.Activate();

        playerMovement.GetComponent<PlayerInput>().actions["Look"].Disable(); // Prevents camera movement at all in pause
        pauseMenuCam.GetComponent<CinemachineCamera>().PreviousStateIsValid = false; // SUPPOSED TO prevent the camera from spinning around when you open the menu
        pauseCamSync.onPause(); // Locks the pause menu camera rotation to prevent spinning

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Properly disables WASD menu navigation
        var navigate = uiInputModule.actionsAsset.FindAction("UI/Navigate");
        foreach (var binding in navigate.bindings)
        {
            if (binding.path.Contains("Keyboard") || binding.path.Contains("arrow"))
                navigate.ApplyBindingOverride(new InputBinding { path = binding.path, overridePath = "" });
        }

        // Locks jumping and menu navigation with thumbstick in pause menu with controller
        if (Gamepad.current != null)
        {
            playerMovement.GetComponent<PlayerInput>().actions["Jump"].Disable();

            // Sets the highlighted button when you open the menu every time to always be the same
            StartCoroutine(SelectFirstButton(pauseMenuFirstButton));

            navigate = uiInputModule.actionsAsset.FindAction("UI/Navigate");
            foreach (var binding in navigate.bindings)
            {
                // Stops thumbstick navigation in pause menu
                if (binding.path.Contains("leftStick") || binding.path.Contains("rightStick"))
                    navigate.ApplyBindingOverride(new InputBinding { path = binding.path, overridePath = "" });
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuCam.Priority = 10;
        freeLookCam.Priority = 20;
        var navigate = uiInputModule.actionsAsset.FindAction("UI/Navigate");
        var orbital = freeLookCam.GetComponent<CinemachineOrbitalFollow>();

        if (playerMovement != null)
            playerMovement.enabled = true;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        if (settingsMenu != null)
            settingsMenu.SetActive(false);
        if (pauseMenuContent != null)
            pauseMenuContent.SetActive(true);
        pauseCamSync.onResume();

        // Counters the freeze in Pause to stop jittering when coming back
        if (orbital != null)
            orbital.HorizontalAxis.Value = pauseMenuCam.transform.eulerAngles.y;
        freeLookCam.PreviousStateIsValid = false; 

        EventSystem.current.SetSelectedGameObject(null);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Should fix the camera transition spin thing 
        playerMovement.GetComponent<PlayerInput>().actions["Look"].Enable();/*
        pauseMenuCam.transform.position = Camera.main.transform.position;
        pauseMenuCam.transform.rotation = Camera.main.transform.rotation;*/


        // Locks jumping in pause menu with controller
        if (Gamepad.current != null)
        {
            playerMovement.GetComponent<PlayerInput>().actions["Jump"].Enable();

            navigate.Enable();
        }

        navigate.RemoveAllBindingOverrides();

    }

    // Highlights a first button for the controller to work
    private IEnumerator SelectFirstButton(Button button)
    {
        yield return null;
        if (playerMovement.GetComponent<PlayerInput>().currentControlScheme == "Gamepad")
        {
            if (button != null)
                EventSystem.current.SetSelectedGameObject(button.gameObject);
        }
    }

    // Disables and enables parts of the menu so that the bubbles dont rerender and look all jittery 
    // when changing menus. Also sets the first button to be selected for controller navigation.
    public void OpenSettingsMenu()
    {
        pauseMenuContent.SetActive(false);
        settingsMenu.SetActive(true);
        if (Gamepad.current != null)
            StartCoroutine(SelectFirstButton(settingsMenuFirstButton));
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenuContent.SetActive(true);
        if (Gamepad.current != null)
            StartCoroutine(SelectFirstButton(pauseMenuFirstButton));
    }
    public void OnHubConfirmOpen()
    {
        if (Gamepad.current != null)
            StartCoroutine(SelectFirstButton(hubConfirmFirstButton));
    }

    public void OnQuitConfirmOpen()
    {
        if (Gamepad.current != null)
            StartCoroutine(SelectFirstButton(quitConfirmFirstButton));
    }
}