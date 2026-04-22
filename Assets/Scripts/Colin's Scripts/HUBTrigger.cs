using UnityEngine;
using UnityEngine.SceneManagement;

public class HUBTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("HUB");
        }
    }
}
