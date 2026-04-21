using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehavior : MonoBehaviour
{
    // Variables & References
    #region

    public CheckPointReturner checkPointReturner;

    public List<Transform> waypoints; // The list of waypoints the bird will go to.

    public float moveSpeed = 5f;
    public float liftHeight = 1.5f;
    public float liftSpeed = 3f;
    public int triggerCheckpointIndex = 1; // Which checkpoint triggers this object

    private int currentWaypointIndex = -1;
    private int lastCheckpointHandled = -1;

    private int targetWaypointIndex = -1;
    private Coroutine currentRoutine;


    [Header("Jordon's Modifications")]
    public bool isMoving = false;
    public float detectionRadius = 1.5f;



    #endregion

    // Start
    #region

    void Start()
    {
        StartCoroutine(MoveToTargetWaypoint());
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
        if (index != lastCheckpointHandled)
        {
            lastCheckpointHandled = index;

            // Set new target waypoint
            targetWaypointIndex = index;

            // Stop current movement and start new one
            if (currentRoutine != null)
            {
                StopCoroutine(currentRoutine);
            }

            currentRoutine = StartCoroutine(MoveToTargetWaypoint());
        }
    }

    #endregion
    
    // MoveSequence
    #region

    IEnumerator MoveToTargetWaypoint()
    {
        if (waypoints.Count == 0 || currentWaypointIndex >= waypoints.Count) yield break; //make sure there are waypoints to move to

        isMoving = true;


        // Select active waypoint move the index forward
        if (currentWaypointIndex < waypoints.Count - 1)
        {
            currentWaypointIndex++;
        }
        Vector3 targetPos = waypoints[currentWaypointIndex].position;

        // Move to waypoint while avoiding walls
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            // This is how it avoids things specificially ignoring "Player" and "Ignore_Raycast"
            LayerMask avoidanceLayer = ~LayerMask.GetMask("Player", "Ignore_Raycast"); 
            bool isObstacleInWay = Physics.CheckSphere(transform.position, detectionRadius, avoidanceLayer);

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

        isMoving = false;
        Debug.Log("Reached Waypoint: " + currentWaypointIndex);
    }

    // Visual Debugging
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    

    #endregion

}
