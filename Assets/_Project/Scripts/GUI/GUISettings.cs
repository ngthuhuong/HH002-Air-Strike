using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUISettings : GUIBase
{
    [Header("Buttons")] 
    [SerializeField] private Button closeButton;
    [SerializeField] private Button backToHomeButton;
    
    [Header("Sliders")]
    [SerializeField] private Slider volumeSlider;

    #region MonoBehaviour

    private void Start()
    {
        // Initialize the slider value with the current audio volume
        volumeSlider.value = AudioListener.volume;

        // Add a listener to handle volume changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
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
    
    private void OnVolumeChanged(float arg0)
    {
        throw new NotImplementedException();
    }

    #endregion
}
