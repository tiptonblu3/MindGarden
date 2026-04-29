using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public Player_Movement player;
    void awake()
    {
        player = GetComponent<Player_Movement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.isDead = true;
        }
    }
}
