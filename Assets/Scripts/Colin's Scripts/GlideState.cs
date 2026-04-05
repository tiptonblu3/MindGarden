using UnityEngine;

public class GlideState : MonoBehaviour
{
    // Variables & References
    #region

    // References to the other scripts and components
    [Header("References")]
    public Player_Movement playerMovement;
    public GlidePickUp glidePickup;
    public Rigidbody rb;

    // Settings for Glide
    [Header("Glide Settings")]
    public float glideFallSpeed = -2f;
    public float glideGravityScale = 0.3f;

    // Sees if the player is gliding
    private bool isGliding;

    // For gliding stamina
    [Header("Glide Timer")]
    public float maxGlideTime = 10f;
    [SerializeField] private float currentGlideTime;

    #endregion

    // Update
    #region

    private void Update()
    {
        HandleGlideState();
        HandleGlideTimer();
        HandleGlideTimerReset();
    }

    #endregion

    // HandleGlideState
    #region

    // Handles if the player is able to glide or not
    private void HandleGlideState()
    {
        if (playerMovement == null || glidePickup == null || rb == null)
            return;

        bool glideUnlocked = glidePickup.IsGlideUnlocked;

        bool canGlide = currentGlideTime > 0f;

        // Must be holding jump AND not grounded AND glide unlocked AND have glide stamina
        if (playerMovement.IsGrounded == false &&
            glideUnlocked &&
            playerMovement.IsHoldingJump() &&
            canGlide)
        {
            StartGlide();
        }
        else
        {
            StopGlide();
        }
    }

    #endregion

    // StartGlide
    #region

    // Slows player's vertical movement
    private void StartGlide()
    {
        isGliding = true;

        Vector3 vel = rb.linearVelocity;

        // Clamp downward speed
        if (vel.y < glideFallSpeed)
        {
            vel.y = glideFallSpeed;
        }

        rb.linearVelocity = vel;
    }

    #endregion

    // StopGlide
    #region

    // Used when the Glide is stopped for whatever reason
    private void StopGlide()
    {
        isGliding = false;
    }

    #endregion

    // HandleGlideTimer
    #region

    // Drains glide stamina
    private void HandleGlideTimer()
    {
        if (isGliding)
        {
            currentGlideTime -= Time.deltaTime;

            if (currentGlideTime <= 0f)
            {
                currentGlideTime = 0f;
                StopGlide();
            }
        }
    }

    #endregion

    // HandleGlideTimerReset
    #region

    // Resets glide stamina when player lands
    private void HandleGlideTimerReset()
    {
        if (playerMovement.IsGrounded)
        {
            currentGlideTime = maxGlideTime;
        }
    }

    #endregion
}
