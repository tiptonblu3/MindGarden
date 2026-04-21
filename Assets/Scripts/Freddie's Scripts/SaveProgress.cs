using UnityEngine;

public class SaveProgress : MonoBehaviour
{

    string sceneName = SceneManager.GetActiveScene().name;
    string path = Path.Combine(Application.persistentDataPath, sceneName + "_Save.xml");

    //public void
     

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
