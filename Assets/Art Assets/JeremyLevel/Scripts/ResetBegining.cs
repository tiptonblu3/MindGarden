using UnityEngine;

public class ResetBegining : MonoBehaviour
{
    public GameObject Dialog1;
    public GameObject NPCAppear;


    private void OnTriggerEnter(Collider other)
    {
        NPCAppear.SetActive(true);
        Dialog1.SetActive(true);

    }
}
