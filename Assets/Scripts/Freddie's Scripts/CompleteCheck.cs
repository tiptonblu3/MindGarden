using UnityEngine;

public class CompleteCheck : MonoBehaviour
{
    public SaveState saveStateScript;
    public bool Tutorial;
    public GameObject TutorialCheck;
    public bool L1;
    public GameObject L1Check;
    public bool L2;
    public GameObject L2Check;
    public bool L3;
    public GameObject L3Check;

    void Start()
    {
        
    }

    void Update()
    {
        Checker();
        
        saveStateScript = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        
    }


    public void Checker()
    {
        if (saveStateScript.Tutorial == true && Tutorial == true)
        {
            TutorialCheck.SetActive(true);
        }
        if (saveStateScript.L1 == true && L1 == true)
        {
            L1Check.SetActive(true);
        }
        if (saveStateScript.L2 == true && L2 == true)
        {
            L2Check.SetActive(true);
        }
        if (saveStateScript.L3 == true && L3 == true)
        {
            L3Check.SetActive(true);
        }
    }

}
