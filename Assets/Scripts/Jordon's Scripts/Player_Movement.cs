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
    public PlayerInput playerInput;
    public CinemachineCamera playerCam;
    private Vector2 lookInput;
    private CinemachineInputAxisController axisController;
    private CinemachineOrbitalFollow orbitalFollow;
    public PhysicsMaterial slipperyMat; 
    public PhysicsMaterial NormalMat;
    private Collider playerCollider;
    public Animator animator;


    [Header("Player Variables")]
    public float speed = 7f;
    public bool IsGrounded;
    public float rotationSpeed = 320f;
    public bool isDead = false;
    

    [Header("Interaction")]
    public IInteractable currentInteractable;
    
    [Header("Master Settings")]
    [Range(0.1f, 10f)] public float masterSens = 2f; //for customizable sensitivity within settings
    [Header("Camera Settings")]
    public float verticalMatchRatio = 0.02f; //to make vertical movement match horizontal movement.
    public float mouseWeight = 15f;       // Base strength for mouse
    public float controllerWeight = 65f; //multiplier specifically for controllers
    public float verticalScale = 1f;
    public bool invertY = true;
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
    private float rayRadius = 0.3f; // Roughly the width of your player
    private float rayDistance = 1.3f; // Adjust based on your player height


    [Header("Jump Variables")]
    public float JumpHeight = 2f;
    public float MaxJumpHeight = 15f;
    public float fallmultiplier = 5f;
    public bool CanJump = true; //can be disabled so that the player can't jump in certain situations, such as being in the air or interacting with something.
    private bool isHoldingJump;
    private bool isjumping;
    private float jumpTimeCounter;
    public float maxJumpTime = 0.1f; // Maximum time the player can hold the jump button to achieve higher jumps
    private float lastGroundedTime;

    [Header("Fan Mechanics")]
    public bool externalVelocityLock; // For Colin's Fix
    public float externalLockTimer;
    public bool isInFan;

    #region Update, FixedUpdate, LateUpdate, and start
    private void Start()
{
    // Find the orbital follow component on your Cinemachine Camera
    if (playerCam != null)
    {
        orbitalFollow = playerCam.GetComponent<CinemachineOrbitalFollow>();
    }
    
    // Ensure the playerInput is assigned (either via Inspector or code)
    if (playerInput == null)
    {
        playerInput = GetComponent<PlayerInput>();
    }
}
    private void Awake()
    {
        #region Get Components
        playerCollider = GetComponent<Collider>();
        playerInput = GetComponent<PlayerInput>();
        #endregion

        Cursor.lockState = CursorLockMode.Locked;
        // The cursor is automatically invisible when locked
        Cursor.visible = false;
        currentSpeedMultiplier = 1f;
        animator = GetComponentInChildren<Animator>();

    }

    private void Update()
    {

        IsGrounded = Physics.SphereCast(transform.position, rayRadius, Vector3.down, out RaycastHit hit, rayDistance); HandleStamina();
        //Debug.Log($"Holding: {isHoldingJump} | IsJumping: {isjumping} | Counter: {jumpTimeCounter}");
        HandleCameraFOV();
        ApplySensitivity();
        HandleFriction();
        animator.SetBool("IsJumping", isjumping || !IsGrounded);
    }
    
    void FixedUpdate()
    {
        ManageMovement();
        HandleMaxJump();
        FallingGravity();
        if (IsGrounded && !isjumping)
            {
                // High force (e.g., 40-50) effectively "glues" you to steep ramps
                rb.AddForce(Vector3.down * 50f, ForceMode.Force);
            }
            if (IsGrounded) lastGroundedTime = Time.time;
    }
    #endregion


#region Input System Callbacks
    private void OnMove(InputValue inputValue)
    {
        MoveInputVector = inputValue.Get<Vector2>();
    }

public void OnSprint(InputValue value)
{
        bool isGamepad = playerInput != null && playerInput.currentControlScheme == "Gamepad";// Determine device & what multiplier to apply

    if (isGamepad)
    {
        // TOGGLE: Only trigger when the button is first pressed down
        if (value.isPressed) 
        {
            // Toggle the state
            isSprinting = !isSprinting;

            // Optional: If you want to prevent toggling ON while exhausted or standing still
            if (isSprinting && (isExhausted || CurrentStamina <= 0 || MoveInputVector.magnitude < 0.1f))
            {
                isSprinting = false;
            }
        }
    }
    else 
    {
        // HOLD: Standard keyboard behavior
        isSprinting = value.isPressed;
    }
}

    public void OnJump(InputValue Value)
    {
        isHoldingJump = Value.isPressed;        // Only jump when the button is first pressed AND the player is grounded
        if (isHoldingJump && CanJump && IsGrounded){
                    isjumping = true;// Calculates the upward velocity needed to reach the desired jump height
                    jumpTimeCounter = maxJumpTime;
                        float jumpVelocity = Mathf.Sqrt(JumpHeight * -2 * Physics.gravity.y);
                        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);
                        lastGroundedTime = 0;
              
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

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    #endregion


    private void ManageMovement()
    {
        if (isInFan)
            return;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        float currentSpeed = speed * currentSpeedMultiplier;

        Vector3 moveDirection = forward * MoveInputVector.y + right * MoveInputVector.x;

        rb.linearVelocity = new Vector3(
            moveDirection.x * currentSpeed,
            rb.linearVelocity.y,
            moveDirection.z * currentSpeed
        );

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            rb.MoveRotation(
                Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                )
            );
        }

        bool isMoving = moveDirection.magnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);
    }


    private void ExecuteInteraction()
{
    if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    else
        {

        }
}

#region Camera Settings
private void HandleCameraFOV()
{
    if (playerCam == null) {
        Debug.LogWarning("Player Camera reference is missing!");
        return;
    }

    // if sprinting, multiply the base. If not, just use base.
    float targetFOV = isSprinting ? (normalFOV * 1.3f) : normalFOV;

    // Smoothly transition towards the dynamic target
    playerCam.Lens.FieldOfView = Mathf.Lerp(playerCam.Lens.FieldOfView, targetFOV, fovTransitionSpeed * Time.deltaTime);
}



#endregion

private void ApplySensitivity()
{
    if (orbitalFollow == null) return;
    bool isGamepad = playerInput != null && playerInput.currentControlScheme == "Gamepad";// Determine device & what multiplier to apply
    if (lookInput.sqrMagnitude > 0.01f) 
    {
        // Check if the current input is coming from a Mouse or a Gamepad 
        if (Gamepad.current != null && Gamepad.current.rightStick.ReadValue().sqrMagnitude > 0.01f)
            isGamepad = true;
        else
            isGamepad = false;
    }
    
    
    
    // Apply weight to adjust overal sensitivity based on device type.
    float finalSens = isGamepad ? (masterSens * controllerWeight) : (masterSens * mouseWeight);

    // Setup horizontal movement seperate
    var hAxis = orbitalFollow.HorizontalAxis;
    hAxis.Value += lookInput.x * finalSens * Time.deltaTime;
    orbitalFollow.HorizontalAxis = hAxis;

    // Setup vertical movement seperate)
    var vAxis = orbitalFollow.VerticalAxis;
    float yInput = invertY ? -lookInput.y : lookInput.y;

    // Makes up and down camera movement closer to horizontal camera movement
    float vMove = yInput * (finalSens * verticalScale * verticalMatchRatio) * Time.deltaTime;
    
    vAxis.Value += vMove;
    
    // Keep the camera from getting stuck at the very top or bottom
    vAxis.Value = Mathf.Clamp01(vAxis.Value);
    
    orbitalFollow.VerticalAxis = vAxis;
}

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
        
    // If stamina reaches zero, mark player as exhausted
    if (isSprinting)
    {
        // Stop if we ran out of stamina
        if (CurrentStamina <= 0)
        {
            isExhausted = true;
            isSprinting = false;
        }
        
        // Stop if the player stops moving the stick/keys
        if (MoveInputVector.magnitude <= 0.0f)
        {
            isSprinting = false;
        }
    }

    // Recover from exhaustion once stamina reaches the threshold
    if (isExhausted && CurrentStamina >= RegenThreshold)
        {
            isExhausted = false;
        }

    // Drain stamina if sprinting, moving, and not exhausted
    if (isSprinting )
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
    private void HandleFriction()
    {
        if (playerCollider == null) return;

        // If grounded and not moving
        if (IsGrounded && MoveInputVector.magnitude < 0.01f)
        {
            // Apply high friction so we don't slide down bridges
            playerCollider.material = NormalMat;
            
        }
        else
        {
            // We are either moving or in the air: be slippery to avoid wall-clumping
            playerCollider.material = slipperyMat;
        }
    }
    #endregion

    // FOR GLIDE STATE - SORRY I HAD TO TOUCH THIS SCRIPT
    public bool IsHoldingJump()
    {
        return isHoldingJump;
    }
}

