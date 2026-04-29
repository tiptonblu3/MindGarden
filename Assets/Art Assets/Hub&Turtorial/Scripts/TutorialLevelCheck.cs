using UnityEngine;
using System.Collections;

public class TutorialLevelCheck : MonoBehaviour
{
    public CheckPoints Checkpoint;
    public GlidePickUp Glider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StabilizeAndSetState());
    }


    IEnumerator StabilizeAndSetState()
    {
        yield return new WaitForEndOfFrame();
        Checkpointcheck();
    }


    void Checkpointcheck()
    {
        if(Checkpoint.CurrentCheckPointIndex >= 1)
        {
            Glider.IsGlideUnlocked = true;
        }
    }

}
