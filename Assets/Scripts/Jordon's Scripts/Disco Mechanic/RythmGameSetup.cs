using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RythmGameSetup : MonoBehaviour
{
    public bool GameStart = false; 
        public Vector3 StartPosition = new Vector3(0, 0, 0); //where the player will start

    public DancePlatform[] dancePlatforms; //this is for each dance platform option to instantiate
// Up, down, left, right. This is the order for both arrays
    public GameObject[] arrowPrefabs; //this is for each arrow option to instantiate
    public GameObject Platform;
    private GameObject currentPlatform; // make note of the currently spawned platform to destroy it if method is reference dagain
    
    public GameObject playerObject; //reference to the player object to move them to the start point

    public void MovePlayerToStart()
    {
                RemoveOldPlatform();
                // Move the Player to the start point
                playerObject.transform.position = StartPosition;
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null) {
                    rb.linearVelocity = Vector3.zero; // Stop falling/moving
                    rb.position = StartPosition;      // Teleport the physics body
                }

            Vector3 platformSpawnPos = StartPosition;
            platformSpawnPos.y -= 3f; // Subtract from the Y value directly
            currentPlatform = Instantiate(Platform, platformSpawnPos, Quaternion.identity);
    }

    public void RemoveOldPlatform()
    {
        if (currentPlatform != null)
                {
                    Destroy(currentPlatform);
                }
    }

    public void Update()
    {
        if (GameStart)
        {
            MovePlayerToStart();
            GameStart = false;
        }
    }




}