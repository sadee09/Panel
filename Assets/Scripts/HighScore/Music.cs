using System;
using UnityEngine;
public class Music : MonoBehaviour
{
    public GameObject musicOn;
    public GameObject musicOff;

    private SoundSettings _soundSettings;

    private void Start()
    {
        LoadSoundSettings();
        UpdateSoundIcon();
    }

    public void ToggleSound()
    {
        _soundSettings.isSoundOn = !_soundSettings.isSoundOn;
        UpdateSoundIcon();
        SaveSoundSeetings();
    }

    private void UpdateSoundIcon()
    {
        musicOn.SetActive(_soundSettings.isSoundOn);
        musicOff.SetActive(!_soundSettings.isSoundOn);
    }
    private void LoadSoundSettings()
    {
        string json = PlayerPrefs.GetString("SoundSettings", "");
        if (!string.IsNullOrEmpty(json))
        {
            _soundSettings = JsonUtility.FromJson<SoundSettings>(json);
        }
        else
        {
            _soundSettings = new SoundSettings();
            _soundSettings.isSoundOn = true;
        }
    }
    
    private void SaveSoundSeetings()
    {
        string json = JsonUtility.ToJson(_soundSettings);
        PlayerPrefs.SetString("SoundSettings", json);
    }
    
}

[Serializable]
public class SoundSettings
{
    public bool isSoundOn;
}
