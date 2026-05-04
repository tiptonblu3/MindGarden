using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterPipeBehavior : MonoBehaviour
{
    public float rotationGoal; // The target rotation angle for the pipe (can be either 0, 90, 180, or 270)
    public float currentRotation; // The current rotation angle of the pipe
    public float PipeRotateTime = 1.0f; // Time it takes to rotate the pipe (1<X = faster, 0<X<1 = slower, 0 = instant)
    //private Renderer pipeRenderer;
    public float rotationAngle;
    public float correctPosition;
    public bool onXAxis;

    public bool isMoving;

    // From: Danny
    public AudioSource rotationSound;

    private void Start()
    {
        currentRotation = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        if (currentRotation >= 360)
        {
            currentRotation -= 360;
        }
        if (currentRotation == rotationGoal)
        {
            correctPosition = 1;
        }
        else
        {
            correctPosition = 0;
        }
    }


    public void TogglePipe()
    {
        isMoving = true;
        if (onXAxis)
        {
            StartCoroutine(AnimatePipeToggleX());
        }
        else
        {
            StartCoroutine(AnimatePipeToggle());
        }
        if (isMoving)
        {
            rotationSound.Play();
        }
        else if (rotationSound.isPlaying)
        {
            rotationSound.Stop();
        }
        else if (rotationSound == null)
            return;
        Debug.Log("Pipe is now rotating");

    }
    private IEnumerator AnimatePipeToggle()
    {
        Quaternion startRotation = transform.rotation;
        float z = currentRotation + rotationAngle;
        Quaternion endRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, z);

        for (float t = 0; t < 1; t += Time.deltaTime * PipeRotateTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        transform.rotation = endRotation;
        currentRotation = z;
        isMoving = false;
    }
    private IEnumerator AnimatePipeToggleX()
    {
        Quaternion startRotation = transform.rotation;
        float x = currentRotation + rotationAngle;
        Quaternion endRotation = Quaternion.Euler(x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        for (float t = 0; t < 1; t += Time.deltaTime * PipeRotateTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        transform.rotation = endRotation;
        currentRotation = x;
        isMoving = false;
    }
}
