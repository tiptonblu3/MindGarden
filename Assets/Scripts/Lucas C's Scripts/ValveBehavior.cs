using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ValveBehavior : MonoBehaviour
{
    public bool valveActive = false; //can be adjusted to whatever needed.
    public float valveSwitchTime; // Time it takes to switch the valve state (1<X = faster, 0<X<1 = slower, 0 = instant)
    public GameObject waterObject; // used to show it's functionality, made to be removed as a referebce
    private bool interactivityCheck; //used to check if the player is in range of the valve
    public float correctPosition;

    private void Update()
    {
        if (interactivityCheck && InputSystem.actions["Interact"].triggered)
        {
            ToggleValve();
        }
        if (valveActive==true)
        {
            correctPosition = 1;
        }
        else
        {
            correctPosition = 0;
        }
    }
    public void ToggleValve() // Toggle the valve state
    {
        valveActive = !valveActive;
        StartCoroutine(AnimateValveToggle ());
        Debug.Log("Valve is now " + (valveActive ? "active" : "inactive"));
    }

    private IEnumerator AnimateValveToggle()
    {
        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = valveActive ? Quaternion.Euler(0, 0, 180+15) : Quaternion.Euler(0, 0, 0+15);

        for (float t = 0; t < 1; t += Time.deltaTime * valveSwitchTime)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        transform.rotation = endRotation;
        Functionality();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) interactivityCheck = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) interactivityCheck = false;
    }

    private void Functionality()
    {
        //used for demonstrating it's on and off states, made to be removed as a reference
        if (valveActive)
        {
            waterObject.SetActive(false);
        }
        else
        {
            waterObject.SetActive(true);
        }
    }
}
