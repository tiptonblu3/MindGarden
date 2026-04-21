using UnityEngine;
using System.Collections.Generic;

public class BirdGuidance : MonoBehaviour
{
    //large sphere raycast if any object collides with it besides the player make the object rise upwards till it isn't
    //when trigger is entered increment Current waypoint to go to next point
    public int riseSpeed = 3;
    public List<Transform> waypoints;
    public int currentWaypointIndex = 0;
    public float detectionRadius = 2f;
    // Update is called once per frame
    private bool canMove = false;
    void Update()
    {
        
        if (waypoints.Count == 0 || currentWaypointIndex >= waypoints.Count || !canMove) return;

        // 1. Get the direction to the actual waypoint
        Vector3 waypointPos = waypoints[currentWaypointIndex].position;
        
        // 2. Setup Avoidance
        LayerMask avoidanceLayer = ~LayerMask.GetMask("Player", "Ignore_Raycast"); 
        bool isObstacleInWay = Physics.CheckSphere(transform.position, detectionRadius, avoidanceLayer);

        // 3. Define our destination
        Vector3 finalDestination = waypointPos;

        if (isObstacleInWay)
        {
            // Instead of just going UP, we aim for a point that is 
            // ABOVE the current position but still TOWARDS the waypoint.
            Vector3 forwardStep = (waypointPos - transform.position).normalized;
            finalDestination = transform.position + (Vector3.up * 2.0f);
        }

        // 4. Move toward that destination
        transform.position = Vector3.MoveTowards(transform.position, finalDestination, riseSpeed * Time.deltaTime);

        // 5. Rotation (Only on Y axis)
        Vector3 moveDir = finalDestination - transform.position;
        moveDir.y = 0;
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);
        }
        
    }

    public void IncrementWaypoint()
    {
        canMove = true;
        if (currentWaypointIndex < waypoints.Count - 1)
        {
            currentWaypointIndex++;
        }
    }
    /* For testing purposes
    private void OnDrawGizmos()
    {
        // Draw the sphere in the scene so we can see the radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    */
}
