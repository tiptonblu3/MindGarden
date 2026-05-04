using UnityEngine;

public class BirdWaypoints : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the Bird to follow
    public float moveSpeed = 5f; // Speed at which the Bird moves
    public int currentWaypointIndex = 0; // Index of the current waypoint
    public bool isMoving = true; // Flag to check if the Bird is currently moving
    public CheckPoints checkPoints; // Reference to the CheckPoints script
    public GameObject Bird; // Reference to the Bird GameObject
    public Animator anim; // Reference to the Animator component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checkPoints = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<CheckPoints>();
        anim = GetComponentInChildren<Animator>();
        Bird = GameObject.FindGameObjectWithTag("Bird");
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
            anim.SetBool("isMovingBird", true);

            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            Vector3 direction = (targetPosition - Bird.transform.position).normalized;
            Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z);

            if (flatDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(flatDirection);

                Bird.transform.rotation = Quaternion.Slerp(
                    Bird.transform.rotation,
                    targetRotation,
                    5f * Time.deltaTime
                );
            }

            Bird.transform.position = Vector3.MoveTowards(
                Bird.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(Bird.transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                anim.SetBool("isMovingBird", false);
            }
        }
    }
}
