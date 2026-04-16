using UnityEngine;

public class PuzzleButton : MonoBehaviour, IInteractable
{
    public PlatformManager platmanscript;
    public bool puzzleSolved = false;
    void Awake()
    {
        GetComponent<Renderer>().material.color = Color.yellowNice;
    }
    public void Interact()
    {
        if (!puzzleSolved) 
        {
            Debug.Log("Puzzle needs to be completed");
        }
        else
        {
            Debug.Log("Puzzle Already completed");
        }
        
        //platmanscript.StartDiscoSequence();

    }
}
