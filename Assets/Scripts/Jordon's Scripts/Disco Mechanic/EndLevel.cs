using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public PlatformManager platmanscript;
    public GameObject NPCAppear;
    public GameObject Dialog2;
    public GameObject TriggerToEnd;
    public bool EndBeat = false;


    private void OnTriggerEnter(Collider other)
    {
        platmanscript.ResetAllPlatforms();
        NPCAppear.SetActive(true);
        Dialog2.SetActive(true);
        TriggerToEnd.SetActive(false);
        EndBeat = true;
    }
}