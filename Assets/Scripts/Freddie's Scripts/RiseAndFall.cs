using UnityEngine;

public class RiseAndFall : MonoBehaviour
{
    public float RiseSpeed = 5f;
    public float RiseHeight = 0f;
    public float FallDepth;
    public Transform LAT;
    private Rigidbody LARB;
    private bool IsRising = false;



    void Awake()
    {
        LARB = LAT.GetComponent<Rigidbody>();
        FallDepth = LAT.position.y;
    }

    private void OnEnable() 
    {
        IsRising = true;
    }

    private void OnDisable() 
    {
        IsRising = false;
        LAT.position = new Vector3(LAT.position.x, FallDepth, LAT.position.z);
    }

    public void Rise()
    {
        if (LAT == null || LARB == null) return;
        {
            Vector3 RisePos = new Vector3(LAT.position.x, RiseHeight, LAT.position.z);
            Vector3 Rise = Vector3.MoveTowards(LAT.position, RisePos, RiseSpeed * Time.fixedDeltaTime);
            LARB.MovePosition(Rise);
        }

    }
    private void FixedUpdate() 
    {
        if (IsRising)
        {
            Rise();
        }
    }

}
