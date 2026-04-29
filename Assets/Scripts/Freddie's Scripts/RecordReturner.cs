using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordReturner : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Load the HUB level
        SceneManager.LoadScene("HUB");
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
