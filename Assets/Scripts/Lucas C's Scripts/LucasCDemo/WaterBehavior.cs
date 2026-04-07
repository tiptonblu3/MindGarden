using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    public float waterOpacity = 0.5f; //can be adjusted to whatever needed.
    public float underwaterSpeedMultiplier = 0.5f; //can be adjusted to whatever needed.
    private GameObject playerController;
    private float originalPlayerSpeed;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = waterOpacity;
        renderer.material.color = color;
        //playerController = GameObject.Find("Player").GetComponent<Player_Movement>();
        //originalPlayerSpeed = playerController.speed;
    }

    void Update()
    {
        //if (playerController.isUnderwater){playerController.speed = playerController.speed * underwaterSpeedMultiplier;}
        //else{playerController.speed = originalPlayerSpeed;}
    }
}
