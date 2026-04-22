using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointReturner : MonoBehaviour
{
    public GameObject Player;
    public Transform PlayerTransform;
    public List <GameObject> CheckPointList; // List of checkpoints in the level
    public int CurrentCheckPointIndex = -1; // Index of the current checkpoint
    public Player_Movement PlayerMovement;
    public static event Action<int> OnCheckpointChanged;

    public int DiscNum = 0; //number of discs to check if its starting nightmare mode
    public GameObject EndTrigger;
    public EyeChase EyeChase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the player by tag
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = Player.transform;
        PlayerMovement = Player.GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.isDead)
        {
            Respawn();
            PlayerMovement.isDead = false;
        }
    }

    public void Respawn()
    {
        if (CurrentCheckPointIndex < 0 || CurrentCheckPointIndex >= CheckPointList.Count)
        {
            Debug.LogWarning("Player died before hitting a checkpoint!");
            return;
        }

        // 1. Move the player to the saved checkpoint position
        PlayerTransform.position = CheckPointList[CurrentCheckPointIndex].transform.position;

        // 2. Find the bird and tell it to reset AND start moving again
        BirdBehavior bird = FindAnyObjectByType<BirdBehavior>();
        if (bird != null)
        {
            // This calls the function we updated in the previous step
            bird.ResetBirdOnDeath(CurrentCheckPointIndex);
        }

        EyeBehavior eye = FindAnyObjectByType<EyeBehavior>();
        if (eye != null && CurrentCheckPointIndex == 3)
        {
            eye.ResetEyeOnDeath(CurrentCheckPointIndex);
        }
    }

    // GetCurrentCheckpointIndex
    #region

    public int GetCurrentCheckpointIndex()
    {
        return CurrentCheckPointIndex;
    }

    #endregion

    // SetCheckpoint
    #region

    public void SetCheckpoint(int index)
    {
        CurrentCheckPointIndex = index;
        OnCheckpointChanged?.Invoke(index);
    }

    #endregion

    public void DiscCheck()
    {
        if (DiscNum >= 3)
        {
            EndTrigger.SetActive(true);
            EyeChase.Chase = true;
        }
    }
}
