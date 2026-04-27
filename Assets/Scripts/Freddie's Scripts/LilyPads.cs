using UnityEngine;

public class LilyPads : MonoBehaviour
{
    GameObject Player;
    public bool IsOnLilyPad = false;
    public bool WeakLilyPad = false;
    
    public float Speed = 1f;
    public float SinkDepth;
    private float RiseHeight;
    public Transform LPT;
    private Rigidbody LPRB;


     void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        LPRB = LPT.GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RiseHeight = LPT.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LilyPadSink();
    }

    public void LilyPadSink()
    {
        if (WeakLilyPad && IsOnLilyPad)
        {
            Vector3 SinkPos = new Vector3(LPT.position.x, SinkDepth, LPT.position.z);
            Vector3 Sink = Vector3.MoveTowards(LPT.position, SinkPos, Speed * Time.fixedDeltaTime);
            LPRB.MovePosition(Sink);
        }
        else
        {
            Vector3 RisePos = new Vector3(LPT.position.x, RiseHeight, LPT.position.z);
            Vector3 Rise = Vector3.MoveTowards(LPT.position, RisePos, Speed * Time.fixedDeltaTime);
            LPRB.MovePosition(Rise);
        }
    }
    public void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsOnLilyPad = true;
        }
    }
     public void OnCollisionExit(Collision other) 
     {
        if (other.gameObject.CompareTag("Player"))
        {
            IsOnLilyPad = false;
        }
    }
}
