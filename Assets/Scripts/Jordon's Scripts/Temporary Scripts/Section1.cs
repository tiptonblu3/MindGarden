using UnityEngine;

public class Section1 : MonoBehaviour
{
    public RythmGameSetup gameScript;
    public GameObject[] Prefabs; //Things that will be turned on when the player enters the trigger
    public GameObject[] PastPrefabs; //Things that will be turned off when the player enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger");
        //enable objects in array and selection
        for (int i = 0; i < Prefabs.Length; i++)
        {
            Prefabs[i].SetActive(true);
        }
        for (int i = 0; i < PastPrefabs.Length; i++)
        {
            PastPrefabs[i].SetActive(false);
        }
        gameScript.RemoveOldPlatform();
    }
}
