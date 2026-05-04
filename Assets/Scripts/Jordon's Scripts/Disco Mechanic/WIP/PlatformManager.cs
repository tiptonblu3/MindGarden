using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("References")]
    public List<PathBehavior> platforms; // List of all platforms in the scene to be able to pull from
    public Transform playerTransform; //to spawn the stuff in relation to the player
    public Transform targetGoal; //where to move to which would change based on the players position
    public GameObject playerObject; // to move the player to the start position
    public GameObject StartPlatform; // start platform
    public Vector3 StartPosition = new Vector3(0, 0, 0);
    public bool canSpawn = true;
    private PathBehavior currentStandingPlatform;

    [Header("Direction Settings")]
    // Set this in the inspector (e.g., X = 1, Y = 0, Z = 0 for the X-axis)
    public Vector3 spawnDirection = new Vector3(1, 0, 0); 
    public Vector3 sideDirection = new Vector3(0, 0, 1); // Used for the 3-wide spacing
    
    
    [Header("Platform Spawn Settings")]
    public bool sequenceActive = false;
    public float cooldownTime = 2.5f;
    public float forwardOffset = 8f;
    public float sideSpacing = 6f;
    public float stopDistance = 10f; // Distance from goal to stop spawning
    private float lastSpawnTime;
    private Vector3 lastSpawnAnchor;


    [Header("Animated Character Settings")]
    public Animator DiscoAnimator;
    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject UpArrow;
    public GameObject DownArrow; 
    public NighmarManager NM;
    public GameObject DiscoGuy;
    public GameObject Eyeball;
    public GameObject NewVoid;
    public GameObject OldVoid;

    [Header("Animated Character Settings")]
    public AudioSource musicSource;
    public AudioClip IdleMus;
    public AudioClip DanceMus;
    public AudioClip DistortDanceMus;
    public AudioClip DistortIdleMus;

    void Update()
    {
        //If the disco is active, keep trying to generate steps
        if (sequenceActive)
        {
           GenerateNextStep();
           AnimateCharacter();
        }
    }

    public void StartDiscoSequence()
    {
            currentStandingPlatform = null;
            ResetAllPlatforms();
            if (NM.isNighmarActive)
                {
                    musicSource.Stop(); // Kill the old song
                    musicSource.clip = DistortDanceMus; // Swap the file
                    musicSource.Play();
                }
            else
                {
                    musicSource.Stop(); // Kill the old song
                    musicSource.clip = DanceMus; // Swap the file
                    musicSource.Play();
                }

                // Move the Player to the start point
                Rigidbody rb = playerObject.GetComponent<Rigidbody>();

                playerObject.transform.position = StartPosition;
                playerObject.transform.rotation = Quaternion.Euler(0, -90, 0);


                if (rb != null) {
                    rb.linearVelocity = Vector3.zero; // Stop falling/moving
                    rb.angularVelocity = Vector3.zero; // Stops any spinning
                    rb.position = StartPosition;    // Teleport the physics body
                    rb.rotation = Quaternion.Euler(0, -90, 0);
                }
        Physics.SyncTransforms();

        float verticalDrop = 1.5f; 
        Vector3 platformSpawnPos = new Vector3(StartPosition.x, StartPosition.y - verticalDrop, StartPosition.z);
        StartPlatform.transform.position = platformSpawnPos;


        lastSpawnAnchor = platformSpawnPos;
        sequenceActive = true;
        lastSpawnTime = Time.time;
}

public void GenerateNextStep()
    {
        // Debug.Log("Generating next step...");
        // Check if the player reaches the end of the level area
        
        if (!sequenceActive) return;
        if (Time.time < lastSpawnTime + cooldownTime) return;        // Checks for the cooldown

        if (currentStandingPlatform != null)
        {
            currentStandingPlatform.HideArrow();
        }



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
        lastSpawnAnchor += spawnDirection.normalized * forwardOffset;

        // Only pick from the not used list
        List<PathBehavior> availablePool = new List<PathBehavior>(); 
        foreach (var plat in platforms)
        {
            if (plat != currentStandingPlatform)
            {
                availablePool.Add(plat);
            }
        }

        


        //shuffle from available options and not from used options
        for (int i = 0; i < availablePool.Count; i++) { 
            PathBehavior temp = availablePool[i];
            int randomIndex = Random.Range(i, availablePool.Count);
            availablePool[i] = availablePool[randomIndex];
            availablePool[randomIndex] = temp;
        }


        int correctIndex = Random.Range(0, 3);

        //spawn next row
        for (int i = 0; i < 3; i++)
        {
            bool isRightOption = (i == correctIndex);
            Vector3 spawnpos = lastSpawnAnchor + (sideDirection.normalized* ((i - 1) * sideSpacing));
            spawnpos.y = lastSpawnAnchor.y;


            availablePool[i].transform.position = spawnpos;
            availablePool[i].SetPlatform(isRightOption);


            // position platform relative to the achor point
            if (isRightOption)
            {
                currentStandingPlatform = availablePool[i];
            }
        }

    }


    public void ResetAllPlatforms()
    {
        sequenceActive = false;
        DiscoGuy.SetActive(false);
        foreach (var plat in platforms)
        {
            plat.ResetPlatform();
        }
        lastSpawnAnchor = StartPosition;
            
            musicSource.Stop(); // Kill the old song
            musicSource.clip = IdleMus; // Swap the file
            
            musicSource.Play();
    }

    public void AnimateCharacter()
    {
        DiscoGuy.SetActive(true);
        if(NM.isNighmarActive && !NM.hasInitializedNightmare)
        {
            DiscoAnimator.SetBool("NightmareMode", true);
            
            Eyeball.SetActive(true);
            DiscoGuy.SetActive(false);
            NewVoid.SetActive(true);
            OldVoid.SetActive(false);
        
        }
        else
        {
            //if arrow is active in scene then play animation
            DiscoAnimator.SetBool("LeftArrow", LeftArrow.activeInHierarchy);
            DiscoAnimator.SetBool("RightArrow", RightArrow.activeInHierarchy);
            DiscoAnimator.SetBool("UpArrow", UpArrow.activeInHierarchy);
            DiscoAnimator.SetBool("DownArrow", DownArrow.activeInHierarchy);
        }

    }


}
