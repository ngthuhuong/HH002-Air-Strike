using UnityEngine;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    public enum SoundName
    {
        BGM_MainTheme = 0,
        SFX_Active_Shield = 1,
        SFX_Collect_Coin = 2,
        SFX_Victory = 3,
       
        SFX_Button_Click = 5,
        SFX_Lose = 6
        // Add more sound names as needed
    }

    [System.Serializable]
    public class Sound
    {
        public SoundName name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop = false;
    }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private int sfxPoolSize = 5; // Number of SFX AudioSources
    [SerializeField] private List<AudioSource> sfxSources;

    [Header("Sounds")]
    [SerializeField] private List<Sound> sounds;

    private Dictionary<SoundName, Sound> soundDictionary;

    private void Awake()
    {
        soundDictionary = new Dictionary<SoundName, Sound>();
        foreach (var sound in sounds)
        {
            soundDictionary[sound.name] = sound;
        }

        // Initialize SFX AudioSource pool
        sfxSources = new List<AudioSource>();
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSources.Add(sfxSource);
        }
    }

    public void PlayBGM(SoundName name)
    {
        if (!soundDictionary.ContainsKey(name))
        {
            Debug.LogWarning($"BGM '{name}' not found!");
            return;
        }

        Sound sound = soundDictionary[name];
        bgmSource.clip = sound.clip;
        bgmSource.volume = sound.volume;
        bgmSource.pitch = sound.pitch;
        bgmSource.loop = sound.loop;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(SoundName name)
    {
        if (!soundDictionary.ContainsKey(name))
        {
            Debug.LogWarning($"SFX '{name}' not found!");
            return;
        }

        Sound sound = soundDictionary[name];
        AudioSource availableSource = GetAvailableSFXSource();
        if (availableSource != null)
        {
            availableSource.clip = sound.clip;
            availableSource.volume = sound.volume;
            availableSource.pitch = sound.pitch;
            availableSource.loop = sound.loop;
            availableSource.Play();
        }
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (var source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        Debug.LogWarning("No available SFX AudioSource. Consider increasing the pool size.");
        return null;
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var source in sfxSources)
        {
            source.volume = Mathf.Clamp01(volume);
        }
    }
}
