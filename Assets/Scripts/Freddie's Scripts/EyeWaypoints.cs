using System.Collections;
using UnityEngine;

public class EyeWaypoints : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the eye to follow
    public float moveSpeed = 5f; // Speed at which the eye moves
    public int currentWaypointIndex = 0; // Index of the current waypoint
    public bool isMoving = false; // Flag to check if the eye is currently moving
    public CheckPoints checkPoints; // Reference to the CheckPoints script
    public GameObject Eye; // Reference to the Eye GameObject
    public EyeChase eyeChase;
    public bool finalCheckpointReached = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checkPoints = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<CheckPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();
    }

    public void MoveToWaypoint()
    {
        if (isMoving == true)
        {
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            Eye.transform.position = Vector3.MoveTowards(Eye.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(Eye.transform.position, targetPosition) < 0.1f)
            {
                //currentWaypointIndex++;
                isMoving = false;
            }
        }
    }

    public void ResetPosition()
    {
        eyeChase.Chase = false;
        eyeChase.canChase = false;

        if (finalCheckpointReached)
        {
            currentWaypointIndex = 0;
            Eye.transform.position = waypoints[0].position;
            isMoving = false;

            StopAllCoroutines();
            StartCoroutine(RestartChaseAfterDelay());
        }
    }

    public IEnumerator RestartChaseAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        eyeChase.canChase = true;
        eyeChase.Chase = true;
    }
}
