using UnityEngine;
using TMPro;

public class BillboardEffect : MonoBehaviour
{
    private Transform mainCamTransform;

    private void Start()
    {
        if (Camera.main != null)
        {
            mainCamTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Main camera not found. Make sure to tag your camera as Main Camera");
            this.enabled = false;
        }
    }

    // LateUpdate is to make sure the UI updates when players move if we still do no freeze for pause but with walking
    private void LateUpdate()
    {
        if (mainCamTransform != null) 
        {
            // Makes the UI object face the main camera's current position
            transform.LookAt(mainCamTransform.position);

            transform.RotateAround(transform.position, transform.up, 180f);
        }
    }

}
