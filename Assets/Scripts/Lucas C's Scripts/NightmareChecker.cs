using System.Collections;
using UnityEngine;

public class NightmareChecker : MonoBehaviour
{
    public GameObject dreamObjects;//objects leaving the scene
    public GameObject nightmareObjects;//objects entering the scene
    public bool nightmareActive;
    public CheckPoints checkpointScript;

    private void Start()
    {
        if (nightmareActive == true)
        {
            ToggleNightmare();
        }
    }
    void Update()
    {
        if (checkpointScript.CurrentCheckPointIndex == 3 && nightmareActive == false)
        {
            nightmareActive = true;
        }
        if (nightmareActive == true)
        {
            ToggleNightmare();
        }
    }

    public void ToggleNightmare()
    {
        Debug.Log("Nightmare layout activated");
        StartCoroutine(SwitchStatesDream());
        StartCoroutine(SwitchStatesNightmare());
    }
    private IEnumerator SwitchStatesDream()
    {
        Vector3 startPosition = dreamObjects.transform.position;
        Vector3 endPosition = new Vector3(0, -20, 0);
        for (float t = 0; t < 1; t += Time.deltaTime * 1f)
        {
            dreamObjects.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        dreamObjects.transform.position = endPosition;
    }
    private IEnumerator SwitchStatesNightmare()
    {
        Vector3 startPosition = nightmareObjects.transform.position;
        float y = -1.38f;
        Vector3 endPosition = new Vector3(0, y, 0);
        for (float t = 0; t < 1; t += Time.deltaTime * 1f)
        {
            nightmareObjects.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        nightmareObjects.transform.position = endPosition;

    }

}
