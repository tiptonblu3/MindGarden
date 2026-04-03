using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string MasterVolumePref = "MasterVolumePref";
    private static readonly string BGMVolumePref = "BGMVolumePref";
    private static readonly string SoundEffectsVolumePref = "SoundEffectsVolumePref";
    private float MasterVolume;
    private float BGMVolume;
    private float soundEffectsVolume;
    public AudioSource BGMSource;
    public AudioSource[] soundEffectsAudio;

    void Awake()
    {
        LoadSettings();
        ApplyVolume();
    }

    private void LoadSettings()
    {
        // Will load the saved volume value.
        MasterVolume = PlayerPrefs.GetFloat(MasterVolumePref);
        BGMVolume = PlayerPrefs.GetFloat(BGMVolumePref);
        soundEffectsVolume = PlayerPrefs.GetFloat(SoundEffectsVolumePref);

    }

    private void ApplyVolume()
    {
        // Apply master volume scaling to everything else
        float finalBGM = MasterVolume * BGMVolume;
        float finalSFX = MasterVolume * soundEffectsVolume;

        // Apply new values
        BGMSource.volume = finalBGM;

        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = finalSFX;
        }
    }
}
