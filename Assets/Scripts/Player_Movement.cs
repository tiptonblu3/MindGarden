using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class Player_Movement : MonoBehaviour
{
    
    [Header("Reference")]
    public Rigidbody rb;
    public Transform cameraTransform;
    public Vector2 MoveInputVector;
    public CinemachineCamera playerCam;
    private Vector2 lookInput;

    [Header("Player Variables")]
    public float speed = 7f;
    public bool IsGrounded;
    public float rotationSpeed = 320f;

    [Header("Interaction")]
    public IInteractable currentInteractable;

    [Header("Camera Settings")]
    public float Sens = 2f; //senstivity for camera movement
    public float normalFOV = 60f;  //normal fov
    public float fovTransitionSpeed = 5f; 

    [Header("Sprint Variables")]
    public float MaxStamina = 100f;
    public float CurrentStamina = 100f;
    private float StaminaDrainRate = 10f;
    private float StaminaRegenRate = 5f;
    private float RegenThreshold = 30f;
    private float SprintMultiplier = 1.5f;
    private bool isSprinting;
    private bool isExhausted;
    private float currentSpeedMultiplier;

    [Header("Jump Variables")]
    public float JumpHeight = 2f;
    public float MaxJumpHeight = 15f;
    public float fallmultiplier = 5f;
    private bool isHoldingJump;
    private bool isjumping;
    private float jumpTimeCounter;
    public float maxJumpTime = 0.1f; // Maximum time the player can hold the jump button to achieve higher jumps

    #region Update, FixedUpdate, LateUpdate, and start
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // The cursor is automatically invisible when locked
        Cursor.visible = false;
        currentSpeedMultiplier = 1f;

    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        HandleStamina();
        //Debug.Log($"Holding: {isHoldingJump} | IsJumping: {isjumping} | Counter: {jumpTimeCounter}");
        HandleCameraFOV();
    }
    
    void FixedUpdate()
    {
        ManageMovement();
        HandleMaxJump();
        FallingGravity();
    }
    #endregion


#region Input System Callbacks
    private void OnMove(InputValue inputValue)
    {
        MoveInputVector = inputValue.Get<Vector2>();
    }

public void OnSprint(InputValue value)
{
    bool GamepadActive = Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame;
            
    if (GamepadActive)
        {
             //activates if you are using controller
            if (value.isPressed) //toggle sprint
            {
                if (isSprinting)
                {
                    isSprinting = false;
                }
                else if (CurrentStamina > 0 && !isExhausted && MoveInputVector.magnitude > 0.1f)
                {
                    isSprinting = true;
                }
            }
        }
    else //activates if you are using keyboard
    {
            isSprinting = value.isPressed; // Hold to sprint
    }
}

    public void OnJump(InputValue Value)
    {
        isHoldingJump = Value.isPressed;        // Only jump when the button is first pressed AND the player is grounded
        if (isHoldingJump){
            if (IsGrounded)
                {
                    isjumping = true;// Calculates the upward velocity needed to reach the desired jump height
                    jumpTimeCounter = maxJumpTime;
                        float jumpVelocity = Mathf.Sqrt(JumpHeight * -2 * Physics.gravity.y);
                        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);
                }
        }
        else
            {
                isjumping = false; // Stop the boost immediately when button is released
            }   
    }

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            ExecuteInteraction();
        }
    }


#endregion

private void ManageMovement()
    {
        Vector3 forward = cameraTransform.forward; //move forward and back in relation to camera
        Vector3 right = cameraTransform.right; //move left and right in relation to camera
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

        float currentSpeed = speed * currentSpeedMultiplier;
         Vector3 moveDirection = forward * MoveInputVector.y + right * MoveInputVector.x;        //move player object based on directional data and speed variable
         rb.linearVelocity = new Vector3(moveDirection.x * currentSpeed, rb.linearVelocity.y, moveDirection.z * currentSpeed);
            if(moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

            }
    }

    
private void ExecuteInteraction()
{
    if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    else
        {}
}

#region Camera Settings
private void HandleCameraFOV()
{
    if (playerCam == null) {
        Debug.LogWarning("Player Camera reference is missing!");
        return;
    }

    // Calculate target: if sprinting, multiply the base. If not, just use base.
    float targetFOV = isSprinting ? (normalFOV * 1.3f) : normalFOV;

    // Smoothly lerp towards the dynamic target
    playerCam.Lens.FieldOfView = Mathf.Lerp(playerCam.Lens.FieldOfView, targetFOV, fovTransitionSpeed * Time.deltaTime);
}



#endregion



    #region Setups For movement


private void HandleMaxJump() // this will create the tiny jump effect by increasing gravity for a moment.
{
// handles falling to make it at a faster speed
if (isHoldingJump && isjumping)
    {
    if (jumpTimeCounter > 0)
        {
            //Debug.DrawRay(transform.position, Vector3.up * 2, Color.green);
            rb.AddForce(Vector3.up * MaxJumpHeight, ForceMode.Acceleration);
            jumpTimeCounter -= Time.fixedDeltaTime; // Decrease the counter based on time passed
        }
        else
        {
            isjumping = false; // Stop the boost when max jump time is reached
        }
    }
}
private void FallingGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * (fallmultiplier - 1), ForceMode.Acceleration);
        }
    }

private void HandleStamina()
{
    //disables sprint if stopped moving or ran out of air
    if ((isSprinting && MoveInputVector.magnitude < 0.1f) || CurrentStamina <= 0)
        {
            isSprinting = false;
        }
    // If stamina reaches zero, mark player as exhausted
    if (CurrentStamina <= 0)
        {
            isExhausted = true;
        }

    // Recover from exhaustion once stamina reaches the threshold
    if (isExhausted && CurrentStamina >= RegenThreshold)
        {
            isExhausted = false;
        }

    // Drain stamina if sprinting, moving, and not exhausted
    if (isSprinting && MoveInputVector.magnitude > 0 && CurrentStamina > 0 && !isExhausted)
        {
            CurrentStamina -= StaminaDrainRate * Time.deltaTime;
            currentSpeedMultiplier = SprintMultiplier;
        }
    else
        {   // Regenerate stamina when not sprinting
            CurrentStamina += StaminaRegenRate * Time.deltaTime;
            currentSpeedMultiplier = 1f;
        }

    // Ensures stamina stays within valid bounds
    CurrentStamina = Mathf.Clamp(CurrentStamina, 0, MaxStamina);
}
#endregion



}

