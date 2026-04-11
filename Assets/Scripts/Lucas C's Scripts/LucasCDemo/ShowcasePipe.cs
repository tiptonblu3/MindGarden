using UnityEngine;

public class ShowcasePipe : MonoBehaviour
{
    private WaterPipeBehavior pipeBehavior;
    void Start()
    {
        pipeBehavior = GameObject.Find("TEMPSmallPipe").GetComponent<WaterPipeBehavior>();
    }
    public void OnClick()
    {
        pipeBehavior.TogglePipe();
    }
}
