using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;

[System.Serializable]
public class GameData
{
    public int currentCheckpointIndex = 0;
}

public class SaveProgress : MonoBehaviour
{
    [Header("Settings")]
    
    [Header("References")]
    public GameData data = new GameData();
    [SerializeField] private CheckPoints checkpointScript;
    public bool save;
    public bool newGame;
    public bool loadGame;

    private string SavePath => Path.Combine(Application.persistentDataPath, $"{SceneManager.GetActiveScene().name}_Save.xml");

    void Start()
    {
        InitializeReferences();
        LoadGame();
    }
    void Update()
    {
        if (save)
        {
            SaveGame();
            save = false;
        }
        if (newGame)
        {
            NewGame();
            newGame = false;
        }
        if (loadGame)
        {
            LoadGame();
            loadGame = false;
        }
    }

    private void InitializeReferences()
    {
        // If not assigned in Inspector, try to find it via tag
        if (checkpointScript == null)
        {
            GameObject checkpointsObj = GameObject.FindWithTag("Checkpoints");
            if (checkpointsObj != null)
            {
                checkpointScript = checkpointsObj.GetComponent<CheckPoints>();
            }
        }

        if (checkpointScript == null)
        {
            Debug.LogError($"<color=red>SaveProgress:</color> No CheckPoints script found in scene!");
        }
    }
    public void BoolToData()
    {
        if (save)
        {
            SaveGame();
            save = false;
        }
        if (newGame)
        {
            NewGame();
            LoadGame();
            newGame = false;
        }
        if (loadGame)
        {
            LoadGame();
            loadGame = false;
        }

    }
    public void NewGame()
    {
        data = new GameData(); // Resets to defaults
        SaveGame();
        ApplyDataToCheckpoint();
        Debug.Log("New Game initialized and saved.");
    }

    public void SaveGame()
    {
        if (checkpointScript != null)
        {
            data.currentCheckpointIndex = checkpointScript.CurrentCheckPointIndex;
        }

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            using (FileStream stream = new FileStream(SavePath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
            Debug.Log($"<color=green>Game Saved:</color> {SavePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Save failed to {SavePath}: {e.Message}");
        }
    }

    public void LoadGame()
    {
        if (File.Exists(SavePath))
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                using (FileStream stream = new FileStream(SavePath, FileMode.Open))
                {
                    data = (GameData)serializer.Deserialize(stream);
                }
                Debug.Log($"<color=cyan>Game Loaded:</color> {SavePath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Load failed: {e.Message}");
                data = new GameData(); // Fallback to new data on corruption
            }
        }
        else
        {
            data = new GameData();
            Debug.Log("No save file found. Initialized new data.");
        }

        ApplyDataToCheckpoint();
    }

    public void ApplyDataToCheckpoint()
    {
        if (checkpointScript != null)
        {
            checkpointScript.CurrentCheckPointIndex = data.currentCheckpointIndex;
            Debug.Log($"Checkpoint set to index: {data.currentCheckpointIndex}");
        }
    }
}