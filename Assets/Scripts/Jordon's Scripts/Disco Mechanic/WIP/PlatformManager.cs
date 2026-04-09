using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("References")]
    public List<PathBehavior> platforms; // List of all platforms in the scene to be able to pull from
    public Transform playerTransform; //to spawn the stuff in relation to the player
    public Transform targetGoal; //where to move to which would change based on the players position
    public PathBehavior PathBehaviorScript;
    public PathStart PathStartScript;
    public GameObject playerObject; // to move the player to the start position
    public GameObject StartPlatform; // start platform
    public Vector3 StartPosition = new Vector3(0, 0, 0);
    public bool canSpawn = true;
    
    
    [Header("Platform Spawn Settings")]
    public bool sequenceActive = true;
    public float cooldownTime = 1.5f;
    public float forwardOffset = 5f;
    public float sideSpacing = 2.5f;
    public float stopDistance = 10f; // Distance from goal to stop spawning

    private float lastSpawnTime;

    void Update()
    {
        // If the disco is active, keep trying to generate steps
        //if (sequenceActive)
        //{
        //    GenerateNextStep();
        //}
    }
    public void StartDiscoSequence()
    {
                // Move the Player to the start point
                playerObject.transform.position = StartPosition;
                playerObject.transform.rotation = Quaternion.Euler(0, -90, 0);


                Rigidbody rb = playerObject.GetComponent<Rigidbody>();
                if (rb != null) {
                    rb.linearVelocity = Vector3.zero; // Stop falling/moving
                    rb.angularVelocity = Vector3.zero; // Stops any spinning
                    rb.position = StartPosition;    // Teleport the physics body
                    rb.rotation = playerObject.transform.rotation;
                }

            float verticalDrop = 1.5f; 
            Vector3 platformSpawnPos = new Vector3(StartPosition.x, StartPosition.y - verticalDrop, StartPosition.z);
            
            StartPlatform.transform.position = platformSpawnPos;


        //sequenceActive = true;
        GenerateNextStep();
}

public void GenerateNextStep()
    {
        Debug.Log("Generating next step...");
        // Check if the player reaches the end of the level area
        if (!sequenceActive) return;

        // Checks for the cooldown
        if (Time.time < lastSpawnTime + cooldownTime) return;

        // Checks if player has passed the finish line in order to stop spawning platforms
        float distanceToGoal = Vector3.Distance(playerTransform.position, targetGoal.position);
        if (distanceToGoal < stopDistance)
        {
            sequenceActive = false;
            Debug.Log("Reached the end! Disco over.");
            return;
        }

        // Set last spawn time based on current input
        lastSpawnTime = Time.time;

        // Shuffle and pick 3 platforms
        List<PathBehavior> pool = new List<PathBehavior>(platforms);
        for (int i = 0; i < pool.Count; i++) {
            PathBehavior temp = pool[i];
            int randomIndex = Random.Range(i, pool.Count);
            pool[i] = pool[randomIndex];
            pool[randomIndex] = temp;
        }

        int correctIndex = Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            // Reset the platforms that were NOT chosen back to home
            if (i == 0) ResetAllUnused(pool);
            bool isRightOption = (i == correctIndex);

            Vector3 spawnPos = playerTransform.position + (playerTransform.forward * forwardOffset);
            spawnPos += playerTransform.right * ((i - 1) * sideSpacing);
            spawnPos.y = playerTransform.position.y;
            

            pool[i].transform.position = spawnPos;
            pool[i].SetPlatform(isRightOption); //transfers the bool to the platform to set the collider 


        }
    }

    private void ResetAllUnused(List<PathBehavior> activePool)
    {
        
        // This ensures the 4th platform (not picked) goes home
        foreach (var plat in platforms)
        {
            if (!activePool.GetRange(0, 3).Contains(plat))
            {
                plat.ResetPlatform();
            }
        }
    }
    public void ResetAllPlatforms()
    {
        foreach (var plat in platforms)
        {
            plat.ResetPlatform();
        }
    }



}
