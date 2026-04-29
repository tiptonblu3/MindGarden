using UnityEngine;

public class StudioEndTrigger : MonoBehaviour
{
    public NighmarManager nightmar;
    public GameObject Platforms;
    public GameObject Bridge;
    public PlatformManager platmanscript;

    private void OnTriggerEnter(Collider other)
    {
        
        platmanscript.musicSource.Stop();
        nightmar.isNighmarActive = true;
        Platforms.SetActive(false);
        Bridge.SetActive(true);
    }
}
