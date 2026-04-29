using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveState : MonoBehaviour
{
    public static SaveState Instance; // Singleton reference

    public CheckPoints checkPointsScript;
    
    [System.Serializable]
    public class SaveData
    {
        public int checkpointIndex;
    }

    public bool save;
    public bool load;

    void Awake()
    {
        // Singleton Pattern: Ensures only one SaveState exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject checkpointsObj = GameObject.FindWithTag("Checkpoints");
        if (checkpointsObj != null)
        {
            checkPointsScript = checkpointsObj.GetComponent<CheckPoints>();
            // Instead of calling LoadProgress directly, start the Coroutine
            StartCoroutine(LoadAfterInitialization()); 
        }
    }

    System.Collections.IEnumerator LoadAfterInitialization()
    {
        // Wait until the end of the frame so all Awake/Start calls are finished
        yield return new WaitForEndOfFrame();
        LoadProgress();
    }

    void Update() 
    {
        if (save)
        {
            SaveProgress();
            save = false;
        }
        if (load)
        {
            LoadProgress();
            load = false;
        }
    }

    public void SaveProgress()
    {
        if (checkPointsScript == null) return;

        SaveData data = new SaveData { checkpointIndex = checkPointsScript.CurrentCheckPointIndex };
        string json = JsonUtility.ToJson(data);
        string path = GetSavePath();

        File.WriteAllText(path, json);
        Debug.Log($"Progress Saved: {path}");
    }

    public void LoadProgress()
    {
        string path = GetSavePath();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (checkPointsScript != null)
            {
                checkPointsScript.CurrentCheckPointIndex = data.checkpointIndex;
                checkPointsScript.Respawn();
            }
            Debug.Log($"Progress Loaded: {path}");
        }
    }

    public void ResetProgress()
    {
        if (checkPointsScript != null)
        {
            checkPointsScript.CurrentCheckPointIndex = 0;
            SaveProgress(); // Save the reset state immediately
        }
        Debug.Log("Progress reset!");
    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "Save_" + SceneManager.GetActiveScene().name + ".json");
    }
}