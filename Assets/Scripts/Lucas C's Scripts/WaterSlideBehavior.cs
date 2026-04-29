using UnityEngine;

public class WaterSlideBehavior : MonoBehaviour
{
    private Player_Movement playerController;
    private float originalPlayerSpeed;

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<Player_Movement>();
        originalPlayerSpeed = playerController.speed;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.speed = originalPlayerSpeed * 2;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.speed = originalPlayerSpeed;
        }
    }
}
