using UnityEngine;

public class Lvl2App : MonoBehaviour
{
    public CheckPoints CheckPoints;
    public DiscoStateTracker DiscoStateTracker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckPoints = FindObjectOfType<CheckPoints>();
        DiscoStateTracker = FindObjectOfType<DiscoStateTracker>();
    }

    public void Update()
    {
        if (CheckPoints.CurrentCheckPointIndex == 1 && !DiscoStateTracker.checkpoint1Activated)
        {
            DiscoStateTracker.checkpoint1Activated = true;
        }
        else if (CheckPoints.CurrentCheckPointIndex == 2 && !DiscoStateTracker.checkpoint2Activated)
        {
            DiscoStateTracker.checkpoint2Activated = true;
        }
        else if (CheckPoints.CurrentCheckPointIndex == 3 && !DiscoStateTracker.checkpoint3Activated)
        {
            DiscoStateTracker.PuzzButton.puzzleSolved = true;
            DiscoStateTracker.NightMan.isNighmarActive = true;
            DiscoStateTracker.checkpoint3Activated = true;
        }
    }
}
