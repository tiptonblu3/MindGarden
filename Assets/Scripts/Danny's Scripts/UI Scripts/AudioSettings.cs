using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    private static readonly string MasterVolumePref = "MasterVolumePref";
    private static readonly string BGMVolumePref = "BGMVolumePref";
    private static readonly string SoundEffectsVolumePref = "SoundEffectsVolumePref";

    private float MasterVolume;
    private float BGMVolume;
    private float soundEffectsVolume;

    public AudioMixer audioMixer;

    void Awake()
    {
        LoadSettings();
        ApplyVolume();
    }

    private void LoadSettings()
    {
        MasterVolume = PlayerPrefs.GetFloat(MasterVolumePref);
        BGMVolume = PlayerPrefs.GetFloat(BGMVolumePref);
        soundEffectsVolume = PlayerPrefs.GetFloat(SoundEffectsVolumePref);
    }

    private void ApplyVolume()
    {
        SetMasterVolume(MasterVolume);
        SetBGMVolume(BGMVolume);
        SetSFXVolume(soundEffectsVolume);
    }

    // AudioMixers will use int values for decibels insteas of just 0-100% values,
    // so you have to convert the value so the mixer will read it correctly.
    // (Same as AudioManager, but for saving the value)
    private void SetMasterVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    private void SetBGMVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20);
    }

    private void SetSFXVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
}