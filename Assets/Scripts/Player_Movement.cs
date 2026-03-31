using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    
    [Header("Reference")]
    public Rigidbody rb;
    public Transform cameraTransform;
    public Vector2 MoveInputVector;

    [Header("Player Variables")]
    public float speed = 10f;
    public float Sens = 2f; //senstivity for camera movement
    public bool IsGrounded;
    public float rotationSpeed = 320f;

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
    public float JumpHeight = 7f;
    public float MaxJumpHeight = 7f;
    public float fallmultiplier = 2f;
    private bool isHoldingJump;
    private bool isjumping;
    private float jumpTimeCounter;
    public float maxJumpTime = 0.5f; // Maximum time the player can hold the jump button to achieve higher jumps

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
            isSprinting = value.isPressed;
    }

#endregion

private void ManageMovement()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

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
    private void HandleStamina()
    {
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
        {
            // Regenerate stamina when not sprinting
            CurrentStamina += StaminaRegenRate * Time.deltaTime;
            currentSpeedMultiplier = 1f;
        }

        // Ensures stamina stays within valid bounds
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, MaxStamina);
    }

public void OnJump(InputValue Value)
    {
        isHoldingJump = Value.isPressed;

        // Only jump when the button is first pressed AND the player is grounded
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




}
