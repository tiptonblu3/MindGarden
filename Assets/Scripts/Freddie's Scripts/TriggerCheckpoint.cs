using UnityEngine;

public class TriggerCheckpoint : MonoBehaviour
{
    public CheckPoints CheckPoints;
    public GameObject Player;
    void Awake()
    {
        CheckPoints = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<CheckPoints>();
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPoints.CurrentCheckPointIndex++; // Increment the check point index
            gameObject.SetActive(false); // This will disable the record object
        }
        
    }
}
