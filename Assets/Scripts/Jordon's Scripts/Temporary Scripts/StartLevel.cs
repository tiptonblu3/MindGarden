using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public GameObject[] Prefabs; //Things that will be turned on when the player enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger");
        //enable objects in array and selection
        for (int i = 0; i < Prefabs.Length; i++)
        {
            Prefabs[i].SetActive(true);
        }
    }
}
