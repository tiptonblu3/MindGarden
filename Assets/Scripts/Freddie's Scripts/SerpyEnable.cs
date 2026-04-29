using UnityEngine;

public class SerpyEnable : MonoBehaviour
{
    public GameObject serpy; // Reference to the Serpy GameObject
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Serpy enabled");
            serpy.SetActive(true);
        }
    }
}
