using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Checks if the user has played the game before,
    // and if not, sets default values for volume.
    // Also saves the user's volume choice for other scenes.

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MasterVolumePref = "MasterVolumePref";
    private static readonly string BGMVolumePref = "BGMVolumePref";
    private static readonly string SoundEffectsVolumePref = "SoundEffectsVolumePref";

    private int firstPlayInt;

    public Slider MasterVolumeSlider;
    public Slider BGMVolumeSlider;
    public Slider soundEffectsSlider;

    private float MasterVolume;
    private float BGMVolume;
    private float soundEffectsVolume;

    public AudioMixer audioMixer;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            // default volume values on first play
            MasterVolume = .75f;
            BGMVolume = 0.25f;
            soundEffectsVolume = 0.75f;

            // Set sliders
            MasterVolumeSlider.value = MasterVolume;
            BGMVolumeSlider.value = BGMVolume;
            soundEffectsSlider.value = soundEffectsVolume;

            // Apply to mixer (!!!!! took me forever)
            SetMasterVolume(MasterVolume);
            SetBGMVolume(BGMVolume);
            SetSFXVolume(soundEffectsVolume);

            // Saves the volume and takes the user out of FirstPlay.
            PlayerPrefs.SetFloat("MasterVolumePref", MasterVolume);
            PlayerPrefs.SetFloat("BGMVolumePref", BGMVolume);
            PlayerPrefs.SetFloat("SoundEffectsVolumePref", soundEffectsVolume);
            PlayerPrefs.SetInt(FirstPlay, 1);
        }
        else
        {
            // Will load the saved volume value -
            MasterVolume = PlayerPrefs.GetFloat(MasterVolumePref);
            BGMVolume = PlayerPrefs.GetFloat(BGMVolumePref);
            soundEffectsVolume = PlayerPrefs.GetFloat(SoundEffectsVolumePref);

            // then saves the value to the sliders -
            MasterVolumeSlider.value = MasterVolume;
            BGMVolumeSlider.value = BGMVolume;
            soundEffectsSlider.value = soundEffectsVolume;

            // and applies it to the AudioMixer
            SetMasterVolume(MasterVolume);
            SetBGMVolume(BGMVolume);
            SetSFXVolume(soundEffectsVolume);
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MasterVolumePref, MasterVolumeSlider.value);
        PlayerPrefs.SetFloat(BGMVolumePref, BGMVolumeSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsVolumePref, soundEffectsSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateSound()
    {
        SetMasterVolume(MasterVolumeSlider.value);
        SetBGMVolume(BGMVolumeSlider.value);
        SetSFXVolume(soundEffectsSlider.value);
        SaveSoundSettings();
    }

    // AudioMixers will use int values for decibels insteas of just 0-100% values,
    // so you have to convert the value so the mixer will read it correctly.
    public void SetMasterVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    public void SetBGMVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }

    private void OnApplicationFocus(bool focus)
    {
        // Saves audio settings if the player closes the game
        if (!focus)
        {
            SaveSoundSettings();
        }
    }

}

