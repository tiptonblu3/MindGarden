using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject[] LevelPieces; //Things that will be turned off when the player enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger");
        //enable objects in array and selection
        for (int i = 0; i < LevelPieces.Length; i++)
        {
            LevelPieces[i].SetActive(false);
        }
        


    }
}
