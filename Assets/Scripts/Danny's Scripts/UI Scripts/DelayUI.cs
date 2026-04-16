using System.Collections;
using UnityEngine;

public class DelayUI : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu, pauseMenuBubble1, pauseMenuBubble2;

    private void Start()
    {
        StartCoroutine(ShowUIAfterDelay(0.3f));
    }

    IEnumerator ShowUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(delay);

    }

}
