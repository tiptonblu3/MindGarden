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
        if (currentWaypointIndex >= waypoints.Count - 1)
            yield break;

        // Lift up
        Vector3 lifted = transform.position + Vector3.up * liftHeight;

        while (Vector3.Distance(transform.position, lifted) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                lifted,
                liftSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Move to next waypoint
        int clampedIndex = Mathf.Clamp(targetWaypointIndex, 0, waypoints.Count - 1);
        currentWaypointIndex = clampedIndex;

        Vector3 target = waypoints[currentWaypointIndex].position;

        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
    }

    #endregion

}
