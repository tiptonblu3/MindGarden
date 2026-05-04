using UnityEngine;

public class StopMusic : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip OminiousMus;
    void OnTriggerEnter(Collider other)
    {
        
            
            musicSource.Stop(); // Kill the old song
            musicSource.clip = OminiousMus; // Swap the file
            musicSource.Play();
            
        
    }
}
