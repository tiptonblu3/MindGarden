using UnityEngine;

public class Level3DeathPlane : MonoBehaviour
{
    public Player_Movement player;
    public EyeWaypoints eyeWaypoints;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        eyeWaypoints = GameObject.FindGameObjectWithTag("Eye").GetComponent<EyeWaypoints>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.isDead = true;
            eyeWaypoints.ResetPosition();
        }
    }
}
