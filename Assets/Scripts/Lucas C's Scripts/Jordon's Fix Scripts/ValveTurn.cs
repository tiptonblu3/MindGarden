using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ValveTurn : MonoBehaviour, IInteractable
{
    
    public void Interact()
    {
        Debug.Log("Valve Activated");
        ToggleValve();

    }

    public bool valveActive = false; //can be adjusted to whatever needed.
    public float valveSwitchTime = 1; // Time it takes to switch the valve state (1<X = faster, 0<X<1 = slower, 0 = instant)
    public GameObject waterObject; // used to show it's functionality, made to be removed as a referebce

    void Start()
    {
        ToggleValve();
    }
    public void ToggleValve() // Toggle the valve state
    {
        valveActive = !valveActive;
        StartCoroutine(AnimateValveToggle());
        Debug.Log("Valve is now " + (valveActive ? "active" : "inactive"));
    }

    private IEnumerator AnimateValveToggle()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = valveActive ? Quaternion.Euler(0, 0, 180) : Quaternion.Euler(0, 0, 0);

        for (float t = 0; t < 1; t += Time.deltaTime * valveSwitchTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        Functionality();
    }

    private void Functionality()
    {
        //used for demonstrating it's on and off states, made to be removed as a reference
        if (valveActive)
        {
            waterObject.SetActive(true);
        }
        else
        {
            waterObject.SetActive(false);
        }
    }
}


