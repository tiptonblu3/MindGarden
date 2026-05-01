using UnityEngine;

public class Lvl3EyeKill : MonoBehaviour
{
    public EyeWaypoints eyeWaypoints;
    public Player_Movement player;
    void Awake()
    {
        eyeWaypoints = GetComponent<EyeWaypoints>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.isDead = true;
            eyeWaypoints.ResetPosition();
        }
    }
}
