using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class NightmareChecker : MonoBehaviour
{
    public GameObject dreamObjects;//objects leaving the scene
    public GameObject nightmareObjects;//objects entering the scene
    public bool nightmareActive;
    public CheckPoints checkpointScript;

    public AudioSource waterWallAudio;
    public GameObject DayNotify;
    public GameObject NightNotify;

    private void Start()
    {
        if (waterWallAudio != null && !nightmareActive)
            waterWallAudio.Stop();

        if (nightmareActive)
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
        if (waterWallAudio != null && !waterWallAudio.isPlaying)
        {
            waterWallAudio.Play();
        }
    }
    private IEnumerator SwitchStatesDream()
    {
        NightNotify.SetActive(false);
        DayNotify.SetActive(true);
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
        DayNotify.SetActive(false);
        NightNotify.SetActive(true);
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
