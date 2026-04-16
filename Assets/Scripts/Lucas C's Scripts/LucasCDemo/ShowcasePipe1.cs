using UnityEngine;

public class ShowcasePipe1 : MonoBehaviour
{
    private WaterPipeBehavior pipeBehavior;
    void Start()
    {
        pipeBehavior = GameObject.Find("TEMPSmallPipe1").GetComponent<WaterPipeBehavior>();
    }
    public void OnClick()
    {
        pipeBehavior.TogglePipe();
    }
}
