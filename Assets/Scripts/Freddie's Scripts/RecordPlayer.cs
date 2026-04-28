using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// for cam transition
using Unity.Cinemachine;
using UnityEngine.UI;


public class RecordPlayer : MonoBehaviour, IInteractable
{
    public GameObject RecordSelect;
    public PauseMenu Pause;

    [SerializeField] private GameObject defaultButton;
    [SerializeField] private GameObject PauseStartButton;
    [SerializeField] private GameObject LYKText;
    [SerializeField] private CinemachineCamera RecordSelectCam;
    [SerializeField] private MonoBehaviour playerMovementScript; 

    [Header("Record Selection Menu")]
    public Transform[] recordVisuals; // put all 3 level record visual assets here
    public Transform centerPosition; // Empty positions for record transition
    public Transform topPosition;
    public Transform bottomPosition;
    public float transitionSpeed = 5f;
    private bool canMoveSelection = true; 
    private bool isMenuReady = false;


    public List<GameObject> records; // List of records available to play
    private int CurrentRecordIndex = 0; // Index of the currently selected record
    public TextMeshProUGUI RecordChoiceTxt;
    public GameObject RecordID;
    public GameObject OldRecordID; 
    
    private InputAction verticalMoveAction;
    private InputAction submitAction;
    private InputAction cancelAction;

    private void Awake()
    {
        // Initialize Gamepad Bindings
        verticalMoveAction = new InputAction("VerticalMove", binding: "<Gamepad>/leftStick/y");
        submitAction = new InputAction("Submit", binding: "<Gamepad>/buttonSouth");
        cancelAction = new InputAction("Cancel", binding: "<Gamepad>/buttonEast");

        // Add Keyboard Bindings
        verticalMoveAction.AddCompositeBinding("1DAxis")
            .With("Positive", "<Keyboard>/w")
            .With("Negative", "<Keyboard>/s");

        submitAction.AddBinding("<Keyboard>/e");
        submitAction.AddBinding("<Keyboard>/enter");

        cancelAction.AddBinding("<Keyboard>/escape");
    }
    // Quick reference methods to disable/enable ui input
    private void OnEnable()
    {
        verticalMoveAction?.Enable();
        submitAction?.Enable();
        cancelAction?.Enable();
    }

    private void OnDisable()
    {
        verticalMoveAction?.Disable();
        submitAction?.Disable();
        cancelAction?.Disable();
    }

    public void Interact()
    {
        RecordSelect.SetActive(true);
        LYKText.SetActive(false); // Hide text on menu open
        RecordSelectCam.Priority = 25;
        if (playerMovementScript != null) 
            playerMovementScript.enabled = false;

        Cursor.lockState = CursorLockMode.None; // Unlock Cursor
        Cursor.visible = true; // Make Cursor Visible
        CurrentRecordIndex = (CurrentRecordIndex) % records.Count;
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultButton);
        }

        SnapRecordPositions();
        StartCoroutine(MenuActivationDelay());
    }
    public void CloseRecordSelect()
    {
        isMenuReady = false;
        RecordSelect.SetActive(false);
        LYKText.SetActive(true); // Show text on menu close
        RecordSelectCam.Priority = 0; 
        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        Cursor.lockState = CursorLockMode.Locked; // Lock Cursor
        Cursor.visible = false; // Make Cursor Invisible
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(PauseStartButton);
        }
    }

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

    private System.Collections.IEnumerator MenuActivationDelay()
    {
        isMenuReady = false;
        // Wait a tenth of a second so the interaction input clears out
        yield return new WaitForSecondsRealtime(0.1f);
        isMenuReady = true;
    }

    private void UpdateText()
    {
        if (records.Count > 0)
        {
            RecordChoiceTxt.text = records[CurrentRecordIndex].name;
        }
    }
    private void UpdateRecordPositions()
    {
        // Figure out which records go where based on CurrentRecordIndex
        int topIndex = (CurrentRecordIndex - 1 + records.Count) % records.Count;
        int centerIndex = CurrentRecordIndex;
        int bottomIndex = (CurrentRecordIndex + 1) % records.Count;
        for (int i = 0; i < recordVisuals.Length; i++)
        {
            Vector3 targetPos;
            Vector3 targetScale;

            if (i == centerIndex)
            {
                // CENTER - big + rotating
                targetPos = centerPosition.position;
                targetScale = Vector3.one * 1.5f;
                recordVisuals[i].Rotate(0, 0, 50f * Time.deltaTime);
            }
            else if (i == topIndex)
            {
                // TOP - Small above center
                targetPos = topPosition.position;
                targetScale = Vector3.one * 0.7f;
            }
            else if (i == bottomIndex)
            {
                // BOTTOM - Small below center
                targetPos = bottomPosition.position;
                targetScale = Vector3.one * 0.7f;
            }
            else
            {
                // If there are more than 3 records in the index, don't show the extra
                targetPos = topPosition.position + Vector3.right * 200f;  // Off-screen to the right
                targetScale = Vector3.zero;
            }
            recordVisuals[i].position = Vector3.Lerp(recordVisuals[i].position, targetPos, Time.deltaTime * transitionSpeed);
            recordVisuals[i].localScale = Vector3.Lerp(recordVisuals[i].localScale, targetScale, Time.deltaTime * transitionSpeed);
        }
    }

    private void SnapRecordPositions()
    {
        int centerIndex = CurrentRecordIndex;
        int topIndex = (CurrentRecordIndex - 1 + recordVisuals.Length) % recordVisuals.Length;
        int bottomIndex = (CurrentRecordIndex + 1) % recordVisuals.Length;

        for (int i = 0; i < recordVisuals.Length; i++)
        {
            if (i == centerIndex) recordVisuals[i].position = centerPosition.position;
            else if (i == topIndex) recordVisuals[i].position = topPosition.position;
            else if (i == bottomIndex) recordVisuals[i].position = bottomPosition.position;
            else recordVisuals[i].position = centerPosition.position + (centerPosition.right * 10f);
        }
    }

    void Update()
    {
        if (RecordSelect.activeSelf)
        {
            float verticalInput = verticalMoveAction.ReadValue<float>();

            if (canMoveSelection)
            {
                if (verticalInput > 0.5f) // Pushing Up
                {
                    PreviousRecord();
                    StartCoroutine(InputCooldown());
                }
                else if (verticalInput < -0.5f) // Pushing Down
                {
                    NextRecord();
                    StartCoroutine(InputCooldown());
                }
            }

            if (submitAction.WasPressedThisFrame() && isMenuReady)
            {
                PlayRecord();
            }

            if (cancelAction.WasPressedThisFrame() && isMenuReady)
            {
                CloseRecordSelect();
            }
        }

        UpdateText();
        UpdateRecordPositions();
    }

    // Cooldown timer for menu
    private System.Collections.IEnumerator InputCooldown()
    {
        canMoveSelection = false;
        yield return new WaitForSecondsRealtime(0.2f);
        canMoveSelection = true;
    }

}