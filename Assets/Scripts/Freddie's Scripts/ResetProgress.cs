using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetProgress : MonoBehaviour, IInteractable
{
    public SaveState saveStateScript;
    public string sceneToLoad;
    public float DelayTime = 1f; // Time to wait before loading the scene after resetting progress

     void Start()
    {
        if (saveStateScript != null)
        {
            saveStateScript = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        }
    }
    public void Interact()
    {
        saveStateScript.ResetProgress();
        Debug.Log("Progress reset!");

        // Wait for a short moment to ensure the progress is reset before loading the scene
        Invoke("LoadScene", DelayTime); // Adjust the delay as needed
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            saveStateScript.ResetProgress();
            Debug.Log("Progress reset!");

            // Wait for a short moment to ensure the progress is reset before loading the scene
            Invoke("LoadScene", DelayTime); // Adjust the delay as needed
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
