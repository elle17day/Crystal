using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// https://www.youtube.com/watch?v=IVfyYBJr3Yo&t=779s Wave music

public class AudioSettings : MonoBehaviour
{   // Linking to audio system and menu UI
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider voiceSlider;

    private void Start()
    {   // Update slider values on Start
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            LoadAudioSettings();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSfxVolume();
            SetVoiceVolume();
        }
    }

    public void SetMasterVolume()
    {   // Sets master volume to slider value
        float vol = masterSlider.value;
        audioMixer.SetFloat("Master", vol);
        PlayerPrefs.SetFloat("MasterVolume", vol);
    }

    public void SetMusicVolume()
    {   // Sets music volume to slider value
        float vol = musicSlider.value;
        audioMixer.SetFloat("Music", vol);
        PlayerPrefs.SetFloat("MusicVolume", vol);
    }

    public void SetSfxVolume()
    {   // Sets SFX volume to slider value
        float vol = sfxSlider.value;
        audioMixer.SetFloat("SFX", vol);
        PlayerPrefs.SetFloat("SfxVolume", vol);
    }

    public void SetVoiceVolume()
    {   // Sets voice volume to slider value
        float vol = voiceSlider.value;
        audioMixer.SetFloat("Voice", vol);
        PlayerPrefs.SetFloat("VoiceVolume", vol);
    }

    private void LoadAudioSettings()
    {   // Loads player audio settings from PlayerPrefs
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
        voiceSlider.value = PlayerPrefs.GetFloat("VoiceVolume");
    }
}
