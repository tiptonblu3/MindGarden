using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public PlatformManager platmanscript;
    private void OnTriggerEnter(Collider other)
    {
        platmanscript.ResetAllPlatforms();

    }
}