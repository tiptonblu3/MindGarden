using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the thing hitting the zone is the Player
        if (other.CompareTag("Player"))
        {
            
            SceneManager.LoadScene("HUB");
            
        }
    }
}
