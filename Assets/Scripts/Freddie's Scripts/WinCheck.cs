using UnityEngine;

public class WinCheck : MonoBehaviour
{
    public SaveState saveStateScript;
    public GameObject Win;
    public GameObject OLD;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (saveStateScript != null)
        {
            saveStateScript = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        }
        if (saveStateScript.Complete == true)
        {
            Win.SetActive(true);
            OLD.SetActive(false);
        }
    }
}
