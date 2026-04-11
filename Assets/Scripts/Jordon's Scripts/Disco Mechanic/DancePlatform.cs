using UnityEngine;

public class DancePlatform : MonoBehaviour
{
    public bool GameStart = false;
    public Vector3 targetPos = new Vector3(0, 0, 0);
public void Update()
    {
        if (GameStart)
        {
            transform.position = targetPos;
        }
    }



private void Start()
    {
        
    }
















}