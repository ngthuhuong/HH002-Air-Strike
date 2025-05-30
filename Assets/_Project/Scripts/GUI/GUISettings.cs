using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUISettings : GUIBase
{
    [Header("Buttons")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button backToHomeButton;

    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private AudioMixer audioMixer;
    private const string MusicVolumeParam = "Music";
    private const string SFXVolumeParam = "SFX";

    #region MonoBehaviour

    private void Start()
    {
        // Initialize sliders with current mixer values
        float musicVolume, sfxVolume;
        audioMixer = AudioManager.Instance.audioMixer;
        audioMixer.GetFloat(MusicVolumeParam, out musicVolume);
        audioMixer.GetFloat(SFXVolumeParam, out sfxVolume);

        musicSlider.value = Mathf.Pow(10, musicVolume / 20); // Convert dB to linear
        sfxSlider.value = Mathf.Pow(10, sfxVolume / 20);

        // Add listeners for slider changes
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnClickClose);
        backToHomeButton.onClick.AddListener(OnClickBackToHome);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
        backToHomeButton.onClick.RemoveAllListeners();
        Time.timeScale = 1f;
    }

    #endregion

    #region Buttons

    private void OnClickBackToHome()
    {
        SceneManager.LoadScene(0);
        Hide();
    }

    private void OnClickClose()
    {
        Hide();
    }

    private void OnMusicVolumeChanged(float value)
    {
        float volume = Mathf.Log10(value) * 20; // Convert linear to dB
        audioMixer.SetFloat(MusicVolumeParam, volume);
    }

    private void OnSFXVolumeChanged(float value)
    {
        float volume = Mathf.Log10(value) * 20; // Convert linear to dB
        audioMixer.SetFloat(SFXVolumeParam, volume);
    }

    #endregion
}