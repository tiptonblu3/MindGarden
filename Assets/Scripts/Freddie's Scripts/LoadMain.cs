using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMain : MonoBehaviour
{
     public SaveState saveStateScript;
    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }
    public void Start()
    {
        saveStateScript = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        Destroy(saveStateScript.gameObject);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
