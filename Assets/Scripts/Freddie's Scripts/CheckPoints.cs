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
        // Find the player by tag
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = Player.transform;
        PlayerMovement = Player.GetComponent<Player_Movement>();
        Spawn(); // Move the player to the first checkpoint at the start of the game
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
        // Move the player to the position of the current checkpoint
        PlayerTransform.position = CheckPointList[CurrentCheckPointIndex].transform.position;
    }
    public void ResetCheckpoints()
    {
        CurrentCheckPointIndex = 0; // Reset to the first checkpoint
    }
    System.Collections.IEnumerator InitialRespawn()
    {
        // Wait until the end of the first frame so physics/cameras are settled
        yield return new WaitForEndOfFrame();
        Respawn();
    }

    public void Spawn()
    {
        StartCoroutine(InitialRespawn());
    }
}
