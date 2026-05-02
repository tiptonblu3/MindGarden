using UnityEngine;

public class RecordPickUp : MonoBehaviour, IInteractable
{
    public RecordPlayer RecordPlayer;
    public GameObject Level;
    public bool IsFloating = true; // Option to enable or disable floating animation
    private Vector3 startPos; // Store the initial position of the record for animation purposes
    public Transform MenuVisualAsset;
    public AudioClip PickupSound;
    public AudioSource musicSource;

    
    public void Interact()
    {
        AudioSource.PlayClipAtPoint(PickupSound, transform.position);
        RecordPlayer.RecordData newRecord = new RecordPlayer.RecordData
        {
            recordName = Level.name,
            levelPortal = Level,
            menuVisual = MenuVisualAsset
        };

        RecordPlayer.allRecords.Add(newRecord);
        gameObject.SetActive(false); // This will disable the record object
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       if (IsFloating)
        {
            transform.Rotate(Vector3.up, 80 * Time.deltaTime);
            float bounce = Mathf.Sin(Time.time * 2) * 0.1f;
            transform.position = new Vector3(startPos.x, startPos.y + bounce, startPos.z);
        } 
    }
}
