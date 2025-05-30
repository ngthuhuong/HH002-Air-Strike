using System;
using UnityEngine;
using System.Collections.Generic;
using MoreMountains.Tools;

public class AudioManager : Singleton<AudioManager>, MMEventListener<EStopSound>, MMEventListener<EPlaySound>
{
    public enum SoundName
    {
        BGM_MainTheme = 0,
        SFX_Active_Shield = 1,
        SFX_Collect_Coin = 2,
        SFX_Victory = 3,
       
        SFX_Button_Click = 5,
        SFX_Lose = 6,
        SFX_Shoot = 7,
        SFX_Explosion_Soft = 8,
        SFX_Heal = 9
        // Add more sound names as needed
    }
    
    public enum AudioType
    {
        BGM,
        SFX
    }

    [System.Serializable]
    public class Sound
    {
        public SoundName name;
        public AudioType type; // BGM or SFX
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop = false;
    }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private int sfxPoolSize = 7; // Number of SFX AudioSources
    [SerializeField] private List<AudioSource> sfxSources;

    [Header("Sounds")]
    [SerializeField] private List<Sound> sounds;

    private Dictionary<SoundName, Sound> soundDictionary;

    #region MonoBehaviour

    private void Awake()
    {
        soundDictionary = new Dictionary<SoundName, Sound>();
        foreach (var sound in sounds)
        {
            soundDictionary[sound.name] = sound;
        }
        
        bgmSource = gameObject.AddComponent<AudioSource>();

        // Initialize SFX AudioSource pool
        sfxSources = new List<AudioSource>();
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSources.Add(sfxSource);
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening<EStopSound>();
        this.MMEventStartListening<EPlaySound>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EStopSound>();
        this.MMEventStopListening<EPlaySound>();
    }

    #endregion

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
    
    private void StopSFX(AudioManager.SoundName name)
    {
        foreach (var source in sfxSources)
        {
            if (source.clip != null && source.clip.name == name.ToString())
            {
                source.Stop();
                break;
            }
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

    #region Events Listen

    public void OnMMEvent(EStopSound eventType)
    {
        if (soundDictionary.ContainsKey(eventType.SoundName))
        {
            Sound sound = soundDictionary[eventType.SoundName];
            if (sound.type == AudioType.BGM)
            {
                StopBGM();
            }
            else if (sound.type == AudioType.SFX)
            {
                StopSFX(eventType.SoundName);
            }
        }
    }

    public void OnMMEvent(EPlaySound eventType)
    {
        if (soundDictionary.ContainsKey(eventType.SoundName))
        {
            Sound sound = soundDictionary[eventType.SoundName];
            if (sound.type == AudioType.BGM)
            {
                PlayBGM(eventType.SoundName);
            }
            else if (sound.type == AudioType.SFX)
            {
                PlaySFX(eventType.SoundName);
            }
        }
    }

    #endregion
}
