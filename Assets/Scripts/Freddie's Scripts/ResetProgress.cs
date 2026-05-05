using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetProgress : MonoBehaviour, IInteractable
{
    public SaveState saveStateScript;
    public string sceneToLoad;
    public float DelayTime = 1f; // Time to wait before loading the scene after resetting progress
    public bool Tutorial;
    public bool L1;
    public bool L2;
    public bool L3;

     void Start()
    {
        if (saveStateScript != null)
        {
            saveStateScript = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        }
    }
    private void OnEnable() 
    {
        if (saveStateScript != null)
        {
            saveStateScript = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        }
    }
    public void Interact()
    {
        // 1. Mark completion
        if (Tutorial) SaveState.Instance.Tutorial = true;
        if (L1) SaveState.Instance.L1 = true;
        if (L2) SaveState.Instance.L2 = true;
        if (L3) SaveState.Instance.L3 = true;

        // 2. Save the completion to the Global file
        SaveState.Instance.SaveGlobalProgress();

        // 3. Reset the checkpoint index for the CURRENT scene 
        // (Optional: You could also delete the checkpoint JSON file here)
        SaveState.Instance.ResetProgress();

        Invoke("LoadScene", DelayTime);
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            // 1. Mark completion
            if (Tutorial) SaveState.Instance.Tutorial = true;
            if (L1) SaveState.Instance.L1 = true;
            if (L2) SaveState.Instance.L2 = true;
            if (L3) SaveState.Instance.L3 = true;

            // 2. Save the completion to the Global file
            SaveState.Instance.SaveGlobalProgress();

            // 3. Reset the checkpoint index for the CURRENT scene 
            // (Optional: You could also delete the checkpoint JSON file here)
            SaveState.Instance.ResetProgress();

            Invoke("LoadScene", DelayTime);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
