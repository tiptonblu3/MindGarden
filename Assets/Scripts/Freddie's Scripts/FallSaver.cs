using UnityEngine;

public class FallSaver : MonoBehaviour
{
    [Header("Target Tracking")]
    public Transform playerTransform; // Drag player here

    [Header("Thresholds")]
    public float FallThresholdY = -10f; 
    public float BeamHeightY = 10f;   

    [Header("Movement Settings")]
    public float BeamSpeed = 10f;
    public float PullSpeed = 15f;

    private bool IsRecovering = false;
    private bool IsLifting = false;
    private Rigidbody playerRb;

    void Start()
    {
        if (playerTransform != null)
        {
            playerRb = playerTransform.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Check if player fell
        if (playerTransform.position.y < FallThresholdY && !IsRecovering)
        {
            StartRescue();
        }
    }

    void FixedUpdate()
    {
        if (IsRecovering)
        {
            ExecuteRescue();
        }
    }

    void StartRescue()
    {
        IsRecovering = true;
        IsLifting = true;
        
        // Stop physics so the player doesn't keep falling/spinning
        if (playerRb != null)
        {
            playerRb.isKinematic = true;
            playerRb.linearVelocity = Vector3.zero; 
        }
    }

    void ExecuteRescue()
    {
        if (playerRb == null) return;

        if (IsLifting)
        {
            // Move player vertically to the target Y height
            Vector3 upTarget = new Vector3(playerTransform.position.x, BeamHeightY, playerTransform.position.z);
            Vector3 newPos = Vector3.MoveTowards(playerTransform.position, upTarget, BeamSpeed * Time.fixedDeltaTime);
            playerRb.MovePosition(newPos);

            // Check if height reached
            if (Mathf.Abs(playerTransform.position.y - BeamHeightY) < 0.05f)
            {
                IsLifting = false;
            }
        }
        else
        {
            // Pull player toward THIS object (the FallSaver)
            Vector3 newPos = Vector3.MoveTowards(playerTransform.position, transform.position, PullSpeed * Time.fixedDeltaTime);
            playerRb.MovePosition(newPos);

            // Finish rescue when close enough
            if (Vector3.Distance(playerTransform.position, transform.position) < 0.2f)
            {
                EndRescue();
            }
        }
    }

    void EndRescue()
    {
        IsRecovering = false;
        if (playerRb != null) playerRb.isKinematic = false;
        Debug.Log("Rescue Mission Complete.");
    }
}