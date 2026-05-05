using UnityEngine;

public class CompleteCheck : MonoBehaviour
{
    public SaveState saveStateScript;
    public bool Tutorial;
    public bool L1;
    public bool L2;
    public bool L3;
    public GameObject Check;
    

    void Start()
    {
        if (saveStateScript != null)
        {
            saveStateScript = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        }
    }

    void Update()
    {
        Checker();
    }


    public void Checker()
    {
        if (saveStateScript.Tutorial == true && Tutorial == true)
        {
            Check.SetActive(true);
        }
        if (saveStateScript.L1 == true && L1 == true)
        {
            Check.SetActive(true);
        }
        if (saveStateScript.L2 == true && L2 == true)
        {
            Check.SetActive(true);
        }
        if (saveStateScript.L3 == true && L3 == true)
        {
            Check.SetActive(true);
        }
    }





}
