using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string sceneToLoad;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            // Load the next level
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
