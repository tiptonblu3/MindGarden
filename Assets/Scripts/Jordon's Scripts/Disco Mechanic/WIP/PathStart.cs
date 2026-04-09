using UnityEngine;

public class PathStart : MonoBehaviour
{
    public Vector3 originalPosition;
    private bool hasHomeBeenSet = false;

    void Awake() 
    {
        SetHome();
    }

    public void SetHome()
    {
        if (!hasHomeBeenSet)
        {
            originalPosition = transform.position;
            hasHomeBeenSet = true;
            Debug.Log($"{gameObject.name} home set at {originalPosition}");
        }
    }

    public void ResetPlatform() 
    {
        // This moves it back to where it was when the game first started
        transform.position = originalPosition;
    }
}