using UnityEngine;
using System.Collections;

public class DiscoStateTracker : MonoBehaviour
{
    [Header("References")] 
    public CheckPoints CheckPoints;
    public discoDialogue discoDial;
    public discoDialogue discoDial2;
    public NighmarManager NightMan;
    public PuzzleButton PuzzButton;

    [Header("Things that have to despawn")]
    public GameObject DiscPiece1;
    public GameObject DiscPiece2;
    public GameObject DiscPiece3;
    public GameObject Guy1;
    public GameObject Dialogue1;
    public GameObject Guy2;
    public GameObject Dialogue2;
    /*
    Jordon's Notes
    ================

        There are four checkpoints I set it up like this so that when the player falls it doesn't allow them to skip parts 
        of the level but allows the player to progress and teleport back when they fall to their latest checkpoint regardless
        of where they are at in the level.

        Checkpoint 0: just the begining of the level nothing has been activated yet

        Checkpoint 1: This would just have the first disc item collected which would reference CheckPoints.CurrentCheckPointIndex;
        to make it 1 so that it matches the state of the game a

        Checkpoint 2: This is similar to checkpoint 1 as it is just incrementing CheckPoints.CurrentCheckPointIndex; to 
        account for the location of where the respawn would be 
        as well as the disco dialogue being updated to the right point of 2

        Checkpoint 3: For this one these are the bools that would be influenced, 
        first PuzzleButton.puzzleSolved would be set to true
        CheckPoints.CurrentCheckPointIndex would be incremented to 3, 
        and discoDialogue.dialogueIndex would be set to 4 to account for the fact that the player has solved the puzzle and would be in the right place in the dialogue when they respawn
        And finially NighmarManager.isNighmarActive would be set to true.
    
    */
    [Header("Checkpoint 1")] 
    public bool checkpoint1Activated = false;
    [Header("Checkpoint 2")] 
    public bool checkpoint2Activated = false;
    [Header("Checkpoint 3")] 
    public bool checkpoint3Activated = false;

    void Start()
    {
        // Start the routine to change state after the scene is ready
        StartCoroutine(StabilizeAndSetState());
    }
    
    IEnumerator StabilizeAndSetState()
    {
        yield return new WaitForEndOfFrame();
        if (checkpoint3Activated)
        {
            ApplyCheckpoint3();
        }
        else if (checkpoint2Activated)
        {
            ApplyCheckpoint2();
        }
        else if (checkpoint1Activated)
        {
            ApplyCheckpoint1();

        }
    }

    void ApplyCheckpoint3()
    {
        if (discoDial2 != null) 
            {
                discoDial2.dialogueIndex = 4;
                discoDial2.DialogueStart(); // Or whatever function your script uses to refresh the text
            }

        if (CheckPoints != null) CheckPoints.CurrentCheckPointIndex = 3;       
        if (NightMan != null) NightMan.isNighmarActive = true;
        if (PuzzButton != null) PuzzButton.puzzleSolved = true;

        // Saftey Check for 2 and 1
        SafeDisable(DiscPiece1);
        SafeDisable(DiscPiece2);
        SafeDisable(DiscPiece3);
        SafeDisable(Guy1);
        SafeDisable(Dialogue1);

        SafeEnable(Guy2);
        SafeEnable(Dialogue2);
    }
    void ApplyCheckpoint2()
    {
        if (CheckPoints != null) CheckPoints.CurrentCheckPointIndex = 2;
        if (discoDial != null) discoDial.dialogueIndex = 2;

        // Saftey Check for 1
        SafeDisable(DiscPiece1);
        SafeDisable(DiscPiece2);
        SafeDisable(Guy1);
        SafeDisable(Dialogue1);
        
    }
    void ApplyCheckpoint1()
    {
        if (CheckPoints != null) CheckPoints.CurrentCheckPointIndex = 1;
        
        SafeDisable(DiscPiece1);
    }

    void SafeDisable(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(false);
        }
    }
    void SafeEnable(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Missing Reference: You forgot to assign an object in the State Tracker!");
        }
    }
}
