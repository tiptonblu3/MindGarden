using UnityEngine;
using TMPro;


public class UIDiscUpdater : MonoBehaviour
{
    public GameObject disc1;
    public GameObject disc2;
    public GameObject disc3;

    public TextMeshProUGUI UIDiscText;

    //gameObject.activeInHierarchy


    // Update is called once per frame
    void Update()
    {
        CheckDiscStatus();
    }

    void CheckDiscStatus()
    {
        int count = 0;

        // Check each disc and increment the counter if it's collected (inactive)
        if (disc1 != null && !disc1.activeInHierarchy) count++;
        if (disc2 != null && !disc2.activeInHierarchy) count++;
        if (disc3 != null && !disc3.activeInHierarchy) count++;

        // Update the text once at the end
        UIDiscText.text = $"{count}/3 Discs Collected";
    }



}
