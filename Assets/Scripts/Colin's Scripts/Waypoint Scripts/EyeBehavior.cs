using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehavior : MonoBehaviour
{
    // Variables & References
    #region

    public CheckPointReturner checkPointReturner;

    public List<Transform> waypoints; // The list of waypoints the bird will go to.

    public float moveSpeed = 5f;

    public int triggerCheckpointIndex = 1; // Which checkpoint triggers this object

    private bool isMoving = false;
    private bool inCheckpoint4Sequence = false;

    private int currentWaypointIndex = -1;
    private int lastCheckpointHandled = -1;


    private Renderer rend;

    public EyeChase eyeChase;
    public float timeBeforeChase = 2f;

    #endregion

    // Awake
    #region

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        eyeChase = GetComponent<EyeChase>();
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
        

        // React to checkpoints 3 and 4
        if (index == 2 && index != lastCheckpointHandled && !isMoving)
        {
            lastCheckpointHandled = index;
            StartCoroutine(MoveToNextWaypoint());
        }

    }

    #endregion

    // MoveSequence
    #region

    IEnumerator MoveToNextWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Count - 1)
            yield break;

        isMoving = true;

        // Move to next waypoint
        currentWaypointIndex++;

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

        isMoving = false;
    }

    #endregion

    // Checkpoint4Sequence
    #region

    public IEnumerator Checkpoint4Sequence()
    {
        inCheckpoint4Sequence = true;
        isMoving = true;

        // wait 5 seconds
        yield return new WaitForSeconds(timeBeforeChase);

        // go to waypoint
        int newWaypointIndex = 1; // adjust if needed
        Vector3 target1 = waypoints[newWaypointIndex].position;

        while (Vector3.Distance(transform.position, target1) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target1,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        isMoving = false;

        eyeChase.Chase = true;
    }

    #endregion

    // ResetEyeOneDeath
    #region

    public void ResetEyeOnDeath(int checkpointIndex)
    {
        // Only care if we died during or after checkpoint 4 sequence start point
        if (!inCheckpoint4Sequence)
            return;

        StopAllCoroutines();
        isMoving = false;
        transform.position = waypoints[0].position;
        eyeChase.Chase = false;

        StartCoroutine(Checkpoint4Sequence());
    }

    #endregion
}
