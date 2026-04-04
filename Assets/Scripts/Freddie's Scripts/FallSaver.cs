using UnityEngine;

public class FallSaver : MonoBehaviour
{
    public GameObject Player;
    public GameObject SaverObject;
    public float SaverTrigger = - 10f;
    public float PropelForce = 10f;
    public float PullForce = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // find the Player
       Player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.y < SaverTrigger)
        {
            // Launch the Player in the air than a slight pull to the saver object
            Player.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, PropelForce, 0);
            Player.GetComponent<Rigidbody>().AddForce((SaverObject.transform.position - Player.transform.position) * PullForce, ForceMode.Impulse);
        }    
    }
}
