using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class BirdBehavior : MonoBehaviour
{
    // Variables & References
    #region



    public List<Transform> waypoints; // The list of waypoints the bird will go to.

    public float moveSpeed = 5f;
    public float liftHeight = 1.5f;
    public float liftSpeed = 3f;
    public int triggerCheckpointIndex = 1; // Which checkpoint triggers this object
    public Animator anim;

    private int currentWaypointIndex = -1;
    //private int lastCheckpointHandled = -1;

    //private int targetWaypointIndex = -1;
    private Coroutine currentRoutine;


    [Header("Jordon's Modifications")]
    public bool isMovingBird = false;
    public float detectionRadius = 1.5f;
    public Vector3 sphereOffset = new Vector3(0, 5f, 0);


    #endregion

    // Start
    #region

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        currentRoutine = StartCoroutine(MoveToTargetWaypoint(0));
        
    }

    #endregion

    // OnEnable
    #region

    private void OnEnable()
    {
        CheckPointReturner.OnCheckpointChanged += HandleCheckpoint;
    }

    #endregion

    // OnDisable
    #region

    private void OnDisable()
    {
        CheckPointReturner.OnCheckpointChanged -= HandleCheckpoint;
    }

    #endregion

    // HandleCheckpoint
    #region

    private void HandleCheckpoint(int index)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
            currentRoutine = null;
        }

        // 2. Clear the 'isMoving' flag and start fresh
        isMovingBird = false;
        anim.SetBool("isMovingBird", false);

        // 3. Teleport and Start Move
        TeleportAndMove(index);
    }

    #endregion
    
    // MoveSequence
    #region

    IEnumerator MoveToTargetWaypoint(int targetIndex)
    {
        if (waypoints.Count == 0 || targetIndex >= waypoints.Count) yield break; //make sure there are waypoints to move to

        isMovingBird = true;
        anim.SetBool("isMovingBird", true);

        // Select active waypoint move the index forward
        if (currentWaypointIndex < waypoints.Count - 1)
        {
            currentWaypointIndex++;
        }
        Vector3 targetPos = waypoints[targetIndex].position;

        // Move to waypoint while avoiding walls
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            Vector3 spherePosition = transform.position + sphereOffset;
            // This is how it avoids things specificially ignoring "Player" and "Ignore_Raycast"
            LayerMask avoidanceLayer = ~LayerMask.GetMask("Player", "Ignore_Raycast"); 
            bool isObstacleInWay = Physics.CheckSphere(spherePosition, detectionRadius, avoidanceLayer);

            Vector3 finalDestination = targetPos;
            
            if (isObstacleInWay)
            {
                // If blocked, aim UP to clear the wall
                finalDestination = transform.position + (Vector3.up * 2.0f);
            }

            // Move bird
            transform.position = Vector3.MoveTowards(transform.position, finalDestination, moveSpeed * Time.deltaTime);

            // 3. Move to face its head towards accurate direction
            Vector3 moveDir = finalDestination - transform.position;
            moveDir.y = 0;
            if (moveDir.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);
            }

            yield return null; // Wait for next frame
        }

        isMovingBird = false;
        anim.SetBool("isMovingBird", false);
        currentWaypointIndex = targetIndex;
        Debug.Log("Reached Waypoint: " + targetIndex);
    }
    public void TeleportAndMove(int index)
    {
        // index is the Checkpoint the player just hit.
        if (index < 0 || index >= waypoints.Count) return;

        if (currentRoutine != null) StopCoroutine(currentRoutine);

        // 1. TELEPORT to the checkpoint the player is currently at
        transform.position = waypoints[index].position;

        // 2. CALCULATE NEXT WAYPOINT
        // We want to move to index + 1
        int nextWaypoint = index + 1;

        // 3. MOVE to the next one if it exists
        if (nextWaypoint < waypoints.Count)
        {
            currentRoutine = StartCoroutine(MoveToTargetWaypoint(nextWaypoint));
        }
        else
        {
            Debug.Log("Bird is at the final waypoint and has nowhere else to go!");
        }
    }

    // Call this from your Respawn script
    public void ResetBirdOnDeath(int currentIndex)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);

        // Teleport to player's current checkpoint
        transform.position = waypoints[currentIndex].position;

        // Start moving to the NEXT one immediately
        int nextIndex = currentIndex + 1;
        if (nextIndex < waypoints.Count)
        {
            currentRoutine = StartCoroutine(MoveToTargetWaypoint(nextIndex));
        }
    }

    // Visual Debugging
    private void OnDrawGizmosSelected()
    {
        Vector3 gizmoPos = transform.position + sphereOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmoPos, detectionRadius);
    }
    

    #endregion

}
