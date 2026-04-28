using UnityEngine;

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
    
    void Update()
    {
        if (checkpoint1Activated)
        {
            CheckPoints.CurrentCheckPointIndex = 1;
            DiscPiece1.SetActive(false);
        }
        if (checkpoint2Activated)
        {
            if (!checkpoint1Activated) checkpoint1Activated=true; //in case they skip the first one, it will still update the checkpoint index and make sure the first disc piece is gone
            CheckPoints.CurrentCheckPointIndex = 2;
            discoDial.dialogueIndex = 2;
            DiscPiece1.SetActive(false);
            DiscPiece2.SetActive(false);
            Guy1.SetActive(false);
            Dialogue1.SetActive(false);
            CheckPoints.Respawn();
        }
        if (checkpoint3Activated)
        {
            if (!checkpoint1Activated) checkpoint1Activated=true; //in case they skip the first one, it will still update the checkpoint index and make sure the first disc piece is gone
            if (!checkpoint1Activated) checkpoint1Activated=true; //in case they skip the first one, it will still update the checkpoint index and make sure the first disc piece is gone
            CheckPoints.CurrentCheckPointIndex = 3;
            discoDial2.dialogueIndex = 4;
            NightMan.isNighmarActive = true;
            PuzzButton.puzzleSolved = true;
            DiscPiece1.SetActive(false);
            DiscPiece2.SetActive(false);
            DiscPiece3.SetActive(false);
            CheckPoints.Respawn();

        }

    }


}
