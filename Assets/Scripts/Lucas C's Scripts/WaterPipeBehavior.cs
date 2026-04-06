using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterPipeBehavior : MonoBehaviour
{
    public float rotationGoal; // The target rotation angle for the pipe (can be either 0, 90, 180, or 270)
    public float currentRotation; // The current rotation angle of the pipe
    public float PipeRotateTime; // Time it takes to rotate the pipe (1<X = faster, 0<X<1 = slower, 0 = instant)
    private Renderer pipeRenderer;

    void Start()
    {
        currentRotation = transform.eulerAngles.z;
        pipeRenderer = GetComponent<Renderer>();
        TogglePipe();
    }

    void Update()
    {
        if (Mathf.Approximately(currentRotation, rotationGoal))
        {
            pipeRenderer.material.color = Color.green;
        }
        else
        {
            pipeRenderer.material.color = Color.red;
        }

        if (currentRotation >= 360)
        {
            currentRotation -= 360;
        }
    }


    public void TogglePipe()
    {
        StartCoroutine(AnimatePipeToggle());
        Debug.Log("Pipe is now rotating");

    }
    private IEnumerator AnimatePipeToggle()
    {
        Quaternion startRotation = transform.rotation;
        float z = currentRotation + 90f;
        Quaternion endRotation = Quaternion.Euler(0, 0, z);

        for (float t = 0; t < 1; t += Time.deltaTime * PipeRotateTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        transform.rotation = endRotation;
        currentRotation = z;
    }
}
