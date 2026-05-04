using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    public AudioClip splashSound;
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Get the AudioSource we will put on the player
            AudioSource playerAudio = player.GetComponent<AudioSource>();

            if (playerAudio != null)
            {
                // PlayOneShot allows multiple sounds to overlap without cutting off
                playerAudio.PlayOneShot(splashSound);
            }
        }
    }
}