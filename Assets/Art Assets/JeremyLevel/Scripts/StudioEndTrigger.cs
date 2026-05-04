using UnityEngine;

public class StudioEndTrigger : MonoBehaviour
{
    public NighmarManager nightmar;
    public GameObject Platforms;
    public GameObject Bridge;
    public PlatformManager platmanscript;
    public AudioSource musicSource;
    public AudioClip DistortIdleMus;

    private void OnTriggerEnter(Collider other)
    {
        musicSource.Stop(); // Kill the old song
        musicSource.clip = DistortIdleMus;
        musicSource.Play();
        nightmar.isNighmarActive = true;
        Platforms.SetActive(false);
        Bridge.SetActive(true);
    }
}
