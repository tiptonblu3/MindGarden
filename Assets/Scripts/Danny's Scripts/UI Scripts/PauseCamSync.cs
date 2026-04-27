using UnityEngine;
using Unity.Cinemachine;

public class PauseCamSync : MonoBehaviour
{
    [SerializeField] private CinemachineCamera pauseMenuCam;
    [SerializeField] private CinemachineCamera freeLookCam;

    [SerializeField] public Transform followPoint;       
    [SerializeField] public Transform targetGroupTransform;

    private bool isPaused;
    private Quaternion lockedRotation;

    public void onPause()
    {
        isPaused = true;
        lockedRotation = Camera.main.transform.rotation;
        pauseMenuCam.LookAt = targetGroupTransform;
        pauseMenuCam.ForceCameraPosition(Camera.main.transform.position, Camera.main.transform.rotation);
    }

    public void onResume()
    {
        isPaused = false;
        pauseMenuCam.LookAt = followPoint;
    }

    void LateUpdate()
    {
        if (isPaused)
        {
            pauseMenuCam.transform.rotation = lockedRotation;
        }
    }
}