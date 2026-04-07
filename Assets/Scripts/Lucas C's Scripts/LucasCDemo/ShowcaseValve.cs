using UnityEngine;

public class ShowcaseValve : MonoBehaviour
{
    private ValveBehavior valveBehavior;
    void Start()
    {
        valveBehavior = GameObject.Find("TEMPValve").GetComponent<ValveBehavior>();
    }
    public void OnClick()
    {
        valveBehavior.ToggleValve();
    }
}
