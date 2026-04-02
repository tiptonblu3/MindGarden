using UnityEngine;

public class TestButton : MonoBehaviour, IInteractable
{
    int check = 0;
    void Awake()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    public void Interact()
    {
        if (check == 0)
        {
            Debug.Log("Button Pressed! Opening door...");
            GetComponent<Renderer>().material.color = Color.green;
            check++;
        }
        else
        {
            Debug.Log("Button Pressed! Closing door...");
            GetComponent<Renderer>().material.color = Color.red;
            check = 0;
        }
        
    }
}