using UnityEngine;

public class DiscDisabler : MonoBehaviour
{
    public CheckPoints CheckP;
    public GameObject Disc;
    public int DiscNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckP = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<CheckPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckP.CurrentCheckPointIndex == DiscNum)
        {
            Disc.SetActive(false);
        }
    }
}
