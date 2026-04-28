using UnityEngine;

public class StudioEndTrigger : MonoBehaviour
{
    public NighmarManager nightmar;
    public GameObject Platforms;
    public GameObject Bridge;


    private void OnTriggerEnter(Collider other)
    {
        nightmar.isNighmarActive = true;
        Platforms.SetActive(false);
        Bridge.SetActive(true);
    }
}
