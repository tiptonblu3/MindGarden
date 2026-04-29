using UnityEngine;
using UnityEngine.AI;

public class Serpy : MonoBehaviour
{
    public float movementSpeed = 3.5f;
    public float stoppingDistance = 1.5f;
    public Transform initialSpawnPoint; 
    public Transform AdvancePoint; // Point to move to when advancing Serpy

    private NavMeshAgent agent;
    private GameObject player;
    private Player_Movement playerM;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Apply settings to the agent
        agent.speed = movementSpeed;
        agent.stoppingDistance = stoppingDistance;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerM = player.GetComponent<Player_Movement>();
        }
    }

    void Update()
    {
        if (playerM != null && !playerM.isDead)
        {
            ChasePlayer();
        }
        else if (playerM != null && playerM.isDead)
        {
            if (Vector3.Distance(transform.position, initialSpawnPoint.position) > 0.5f)
            {
                ResetSerpy();
            }
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player") && playerM != null)
        {
            playerM.isDead = true;
            Debug.Log("Serpy caught the player!");
        }
    }

    void ChasePlayer()
    {
        if (player != null && agent.enabled)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    public void ResetSerpy()
    {

        agent.enabled = false; 
        
        transform.position = initialSpawnPoint.position;
        transform.rotation = initialSpawnPoint.rotation;

        // Re-enable so it can move again later
        agent.enabled = true; 
        Debug.Log("Serpy has been reset.");
    }

    public void AdvanceSerpy()
    {        
        transform.position = AdvancePoint.position;
        transform.rotation = AdvancePoint.rotation;

        agent.enabled = true; 
        Debug.Log("Serpy has been Advanced.");
    }

    public void StopSerpy()
    {
        agent.enabled = false; 
        Debug.Log("Serpy has been stopped.");
    }
}
    