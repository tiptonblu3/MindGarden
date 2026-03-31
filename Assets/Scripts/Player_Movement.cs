using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    
    [Header("Reference")]
    public Rigidbody rb;
    public Vector2 MoveInputVector;

    [Header("Player Variables")]
    public float speed = 10f;
    public float Sens = 2f; //senstivity for camera movement
    public bool IsGrounded;
    
    [Header("Jump Variables")]
    public float JumpHeight = 4f;
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

    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

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

#endregion

private void ManageMovement()
    {
        //assign directional data based on input
        Vector3 posChange = transform.right * MoveInputVector.x + transform.forward * MoveInputVector.y;
        //move player object based on directional data and speed variable
        rb.linearVelocity = new Vector3(posChange.x * speed, rb.linearVelocity.y, posChange.z * speed ); //Speed times time is to fix framerate changes causing speed disparities
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
