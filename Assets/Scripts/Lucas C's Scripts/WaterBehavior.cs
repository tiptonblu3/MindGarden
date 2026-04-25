using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    public float waterOpacity = 0.5f; //can be adjusted to whatever needed.
    private Player_Movement playerController;
    private float originalPlayerSpeed;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = waterOpacity;
        renderer.material.color = color;
        playerController = GameObject.FindWithTag("Player").GetComponent<Player_Movement>();
        originalPlayerSpeed = playerController.speed;
    }



    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.CanJump = false;
            playerController.speed =4.5f;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.CanJump = true;
            playerController.speed = originalPlayerSpeed;
        }
    }
}
