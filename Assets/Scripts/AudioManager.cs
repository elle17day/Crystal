using UnityEngine;
public class AudioManager : MonoBehaviour
{
    // Create sound manager instance
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // References to Audio Sources
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource voiceSource;


    public void PlaySFX(AudioClip clip)
    {   // Function to play a sound effect
        effectsSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {   // Function to play/change music
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayVoice(AudioClip clip)
    {   // Function to play voices in game
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}