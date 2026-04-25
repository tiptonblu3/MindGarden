using System;
using System.Collections;
using UnityEngine;

public class UpdateCheckpoint : MonoBehaviour
{
    // Variables & References
    #region

    public int checkpointIndex;
    private bool hasTriggered = false;
    public CheckPointReturner checkPointReturner;

    [Header("DeathBox Settings")]
    public Transform deathBox;
    public float targetYScale;
    public float growDuration = 2f;

    #endregion

    // OnTriggerEnter
    #region

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            checkPointReturner.SetCheckpoint(checkpointIndex);
            Debug.Log("Triggered Checkpoint: " + checkpointIndex);

            if (deathBox != null)
            {
                StartCoroutine(GrowDeathBox());
            }
        }
    }

    #endregion

    // GrowDeathBox
    #region

    // Makes the Death Fog grow over time
    IEnumerator GrowDeathBox()
    {
        Vector3 startScale = deathBox.localScale;
        Vector3 targetScale = new Vector3(startScale.x, targetYScale, startScale.z);

        float elapsed = 0f;

        while (elapsed < growDuration)
        {
            float t = elapsed / growDuration;
            deathBox.localScale = Vector3.Lerp(startScale, targetScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        deathBox.localScale = targetScale;
    }

    #endregion
}
