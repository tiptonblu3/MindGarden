using UnityEngine;

public class RaycastWatcher : MonoBehaviour
{
    public Transform target; // The object to watch
    public float maxDistance = 50f;
    public float moveSpeed = 5f;       // How fast the camera moves
    public float stopDistance = 2f;    // Don't get closer than this
    public LayerMask detectionMask; // Use layers to ignore the watcher itself

    void Update()
    {
        if (target == null) return;

        // 1. Always look at the target to keep it centered
        transform.LookAt(target);

        // 2. Calculate the direction and distance
        Vector3 direction = target.position - transform.position;
        float currentDistance = Vector3.Distance(transform.position, target.position);

        // 3. Perform the Raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, detectionMask))
        {
            // Check if the ray hit our target
            if (hit.transform == target)
            {
                Debug.DrawRay(transform.position, direction.normalized * hit.distance, Color.green);

                // 4. MOVE LOGIC: Move forward if we are still too far away
                Debug.Log("Target visible");
            }
            else
            {
                if (hit.distance > stopDistance)
                {
                    // Move the camera forward along its local Z axis
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    Debug.Log("Approaching target...");
                }
                else
                {
                    
                }
                // The ray hit something else (a wall/obstacle)
                Debug.Log("Path blocked by: " + hit.transform.name);
                Debug.DrawRay(transform.position, direction.normalized * hit.distance, Color.red);

                // OPTIONAL: Stop moving if blocked, or keep moving to "peek" around the corner
            }
        }
    }
}