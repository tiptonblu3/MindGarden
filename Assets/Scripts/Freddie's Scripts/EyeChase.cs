using UnityEngine;

public class EyeChase : MonoBehaviour
{
    public GameObject player;
    public float EyeSpeed = 0.3f;
    public bool Chase = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        LookAtPlayer();
        if (Chase)
        {
            ChasePlayer();
        }
    }
        

    public void ChasePlayer()
    {
        if (player != null)
        {
            transform.position += transform.forward * Time.fixedDeltaTime * EyeSpeed; // Adjust speed as needed
        }
    }
    
    public void LookAtPlayer()
    {
        if (player != null)
        {
            transform.LookAt(player.transform);
        }
    }
}
