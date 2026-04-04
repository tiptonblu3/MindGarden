using UnityEngine;

public class QuitManager : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quit button pressed\nGame exiting...");
        Application.Quit();
        // stops playback in editor when called
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}