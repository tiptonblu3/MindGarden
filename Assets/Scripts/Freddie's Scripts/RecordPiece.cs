using UnityEngine;

public class RecordPiece : MonoBehaviour, IInteractable
{
    public CheckPoints CheckPoints;
    public AudioSource musicSource;
    

    void Awake()
    {
        CheckPoints = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<CheckPoints>();
    }

    public void Interact()
    {
        if (gameObject.activeInHierarchy)
        {
            musicSource.Play();
            Debug.Log("Record Piece Picked Up!");
            CheckPoints.CurrentCheckPointIndex++; // Increment the check point index
            Invoke("SetDisactive", 0.5f);
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void SetDisactive()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
