using UnityEngine;

public class EyeChase : MonoBehaviour
{
    public GameObject player;
    public float EyeSpeed = 0.3f;
    public bool Chase = false;
    public bool canChase = true;

    [Header("Jordon's evil bird scripts")] //I love this fuckign bird
    public float detectionDistance = 5f;
    public float sphereRadius = 0.5f;
    public LayerMask obstacleLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        LookAtPlayer();

        if (Chase && canChase)
        {
            ChasePlayer();
        }
    }

    public void ChasePlayer()
    {
        Vector3 moveDirection = transform.forward * Time.fixedDeltaTime * EyeSpeed; // Adjust speed as needed
        RaycastHit hit;
        if (player != null)
        {
            if (Physics.SphereCast(transform.position, sphereRadius, moveDirection, out hit, detectionDistance, obstacleLayer))
            {
                Vector3 wallNormal = hit.normal;

                Vector3 avoidDirection = Vector3.ProjectOnPlane(moveDirection, wallNormal);

                transform.position += avoidDirection.normalized * EyeSpeed * Time.deltaTime;
            }

            else
            {
                transform.position += transform.forward * EyeSpeed * Time.deltaTime;
            }
        }
    }

    public void LookAtPlayer()
    {
        if (player != null)
        {
            transform.LookAt(player.transform);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}