using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Linq;

// for cam transition
using Unity.Cinemachine;
using UnityEngine.UI;


public class RecordPlayer : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public class RecordData
    {
        public string recordName;      // The UI Display Name
        public GameObject levelPortal; // The Level GameObject to activate
        public Transform menuVisual;   // The 2D/3D visual icon in the menu
    }
    public GameObject RecordSelect;
    public PauseMenu Pause;

    [SerializeField] private GameObject defaultButton;
    [SerializeField] private GameObject PauseStartButton;
    [SerializeField] private GameObject LYKText;
    [SerializeField] private CinemachineCamera RecordSelectCam;
    [SerializeField] private MonoBehaviour playerMovementScript; 

    [Header("Record Selection Menu")]
    public Transform centerPosition; // Empty positions for record transition
    public Transform topPosition;
    public Transform bottomPosition;
    public float transitionSpeed = 5f;
    private bool canMoveSelection = true; 
    private bool isMenuReady = false;


    public List<RecordData> allRecords = new List<RecordData>();
    private int CurrentRecordIndex = 0; // Index of the currently selected record
    public TextMeshProUGUI RecordChoiceTxt;
    public GameObject RecordID;
    public GameObject OldRecordID; 

    private void SortRecords()
    {
        if (allRecords.Count > 0)
        {
            // This sorts the list alphabetically based on the string 'recordName'
            // Tutorial usually comes last alphabetically, or you can prefix with numbers
            allRecords = allRecords.OrderBy(r => r.recordName).ToList();
        }
    }

    public void Interact()
    {
        if (allRecords.Count == 0)
        {
            Debug.LogWarning("You don't have any records yet!");
            return;
        }
        SortRecords();

        RecordSelect.SetActive(true);
        LYKText.SetActive(false); // Hide text on menu open
        RecordSelectCam.Priority = 25;
        if (playerMovementScript != null) 
            playerMovementScript.enabled = false;

        Cursor.lockState = CursorLockMode.None; // Unlock Cursor
        Cursor.visible = true; // Make Cursor Visible
        CurrentRecordIndex = (CurrentRecordIndex) % allRecords.Count;
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
        CurrentRecordIndex = (CurrentRecordIndex + 1) % allRecords.Count; // Move to the next record, loop back to the start if at the end
        Debug.Log("Selected Record: " + allRecords[CurrentRecordIndex].recordName); // Log the currently selected record
    }

    public void PreviousRecord()
    {
        if (CurrentRecordIndex == 0)
        {
            CurrentRecordIndex = allRecords.Count - 1; // Move to the last record if currently at the first record
        }
        else
        {
            CurrentRecordIndex = (CurrentRecordIndex - 1) % allRecords.Count; // Move to the Previous record, loop back to the end if at the start
        }

        Debug.Log("Selected Record: " + allRecords[CurrentRecordIndex].recordName); // Log the currently selected record
    }

    public void PlayRecord()
    {
        // Set up portal to level
        if (OldRecordID != null)
        {
            OldRecordID.SetActive(false); // Deactivate the previously played record
        }
        Debug.Log("Playing Record: " + allRecords[CurrentRecordIndex].recordName); // DEBUG LOG TO SEE IF TRIGGERED

        RecordID = allRecords[CurrentRecordIndex].levelPortal;

        if (RecordID != null)
        {
            RecordID.SetActive(true);
            OldRecordID = RecordID;
        }

        CloseRecordSelect();
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
        if (allRecords.Count > 0)
        {
            RecordChoiceTxt.text = allRecords[CurrentRecordIndex].recordName;
        }
    }
    private void UpdateRecordPositions()
    {
        int count = allRecords.Count;
        if (count == 0) return;

        int topIndex = (CurrentRecordIndex - 1 + count) % count;
        int centerIndex = CurrentRecordIndex;
        int bottomIndex = (CurrentRecordIndex + 1) % count;

        for (int i = 0; i < count; i++)
        {
            // Get each specific visual asset inside the list entry
            Transform visual = allRecords[i].menuVisual;
            if (visual == null) continue;

            Vector3 targetLocalPos;
            Vector3 targetScale;

            // Determine where this specific record should be sitting
            if (i == centerIndex)
            {
                targetLocalPos = centerPosition.localPosition;
                targetScale = Vector3.one * 1.5f;
                visual.Rotate(0, 0, 50f * Time.deltaTime);
            }
            else if (i == topIndex)
            {
                targetLocalPos = topPosition.localPosition;
                targetScale = Vector3.one * 0.7f;
            }
            else if (i == bottomIndex)
            {
                targetLocalPos = bottomPosition.localPosition;
                targetScale = Vector3.one * 0.7f;
            }
            else
            {
                targetLocalPos = centerPosition.localPosition + (Vector3.right * 1000f);
                targetScale = Vector3.zero;
            }

            RectTransform rect = visual as RectTransform;
            if (rect != null)
            {
                // Make sure your target positions are also RectTransforms
                rect.localPosition = Vector3.Lerp(rect.localPosition, targetLocalPos, Time.deltaTime * transitionSpeed);
            }
            visual.localScale = Vector3.Lerp(visual.localScale, targetScale, Time.deltaTime * transitionSpeed);
        }
    }

    private void SnapRecordPositions()
    {
        int count = allRecords.Count;
        if (count == 0) return;

        int centerIndex = CurrentRecordIndex;
        int topIndex = (CurrentRecordIndex - 1 + count) % count;
        int bottomIndex = (CurrentRecordIndex + 1) % count;

        for (int i = 0; i < count; i++)
        {      
            Transform visual = allRecords[i].menuVisual;

            if (visual == null) continue;

            if (i == centerIndex)
                visual.localPosition = centerPosition.localPosition;
            else if (i == topIndex)
                visual.localPosition = topPosition.localPosition;
            else if (i == bottomIndex)
                visual.localPosition = bottomPosition.localPosition;
            else
                visual.localPosition = centerPosition.localPosition + (centerPosition.right * 10f);
        }
    }

    void Update()
    {
        if (!RecordSelect.activeSelf) return;

        if (isMenuReady)
        {
            float verticalInput = 0f;

            // Check Keyboard
            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed) verticalInput = 1f;
                else if (Keyboard.current.sKey.isPressed) verticalInput = -1f;
            }

            // Check Gamepad
            if (Gamepad.current != null)
            {
                float stickY = Gamepad.current.leftStick.y.ReadValue();
                if (Mathf.Abs(stickY) > 0.5f) verticalInput = stickY;
            }

            // Selection Movement
            if (canMoveSelection && Mathf.Abs(verticalInput) > 0.5f)
            {
                if (verticalInput > 0.5f) PreviousRecord();
                else NextRecord();

                StartCoroutine(InputCooldown());
            }

            // Submit (F, Enter or South Button)
            bool submitPressed = false;
            if (Keyboard.current != null && (Keyboard.current.fKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame))
                submitPressed = true;
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
                submitPressed = true;

            if (submitPressed)
            {
                PlayRecord();
            }

            // Cancel (Esc or East Button)
            bool cancelPressed = false;
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
                cancelPressed = true;
            if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
                cancelPressed = true;

            if (cancelPressed)
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