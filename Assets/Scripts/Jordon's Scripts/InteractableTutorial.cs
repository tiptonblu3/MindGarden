using UnityEngine;

public class InteractableTutorial : MonoBehaviour, IInteractable
{ // This is a script thats a very basic button that changes color when you interact with it. 
// Feel free to use this as a base for your own interactable objects, or to expand on it with your own ideas!
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