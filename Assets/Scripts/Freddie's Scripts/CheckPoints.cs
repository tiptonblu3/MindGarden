using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public GameObject Player;
    public Transform PlayerTransform;
    public List <GameObject> CheckPointList; // List of checkpoints in the level
    public int CurrentCheckPointIndex = 0; // Index of the current checkpoint
    public Player_Movement PlayerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        // Find the player by tag
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = Player.transform;
        PlayerMovement = Player.GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Respawn();
        if (PlayerMovement.isDead)
        {
            Respawn();
            PlayerMovement.isDead = false;
        }
    }

    public void Respawn()
    {
        if (PlayerTransform == null) return; // Safety check
        // Move the player
        PlayerTransform.position = CheckPointList[CurrentCheckPointIndex].transform.position;
        
        Debug.Log("Player Respawned to index: " + CurrentCheckPointIndex);
    }
    public void ResetCheckpoints()
    {
        CurrentCheckPointIndex = 0; // Reset to the first checkpoint
    }
}
