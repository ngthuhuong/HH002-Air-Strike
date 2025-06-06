using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIHUD : GUIBase
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI shieldCoolingText;
    [SerializeField] private TextMeshProUGUI speedCoolingText;

    [Header("Buttons")] 
    [SerializeField] private Button shopButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button profileButton;
    
    
    #region MonoBehaviour

    private void OnEnable()
    {
        shopButton.onClick.AddListener(OnClickShop);
        settingsButton.onClick.AddListener(OnClickSettings);
        profileButton.onClick.AddListener(OnClickProfile);
    }

    private void OnClickProfile()
    {
        GUIManager.Instance.GUIProfile.Show();
    }


    private void OnDisable()
    {
        shopButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        profileButton.onClick.RemoveAllListeners();
    }

    #endregion

    #region Button Events

    private void OnClickShop()
    {
        GUIManager.Instance.GUIShop.Show();
    }
    
    private void OnClickSettings()
    {
        GUIManager.Instance.GUISettings.Show();
    }

    #endregion
    #region Public Methods

    public void UpdateUI()
    {
        scoreText.text = DataManager.Instance.CurrentScore.ToString();
        hpText.text = $"HP: {GamePlayManager.Instance.Player.CurrentHealth.ToString()}";
        coinText.text = DataManager.Instance.CurrentCoin.ToString();
    }

    #endregion

    public void AppyBoosterCooldown(BoosterType type, float duration)
    {
        if (type == BoosterType.Speed)
        {
            StartCoroutine(BoosterCooldownCoroutine(type, duration, speedCoolingText));
        }
        else if (type == BoosterType.Shield)
        {
            StartCoroutine(BoosterCooldownCoroutine(type, duration, shieldCoolingText));
        }
        
        
    }

    private IEnumerator BoosterCooldownCoroutine(BoosterType type, float duration, TextMeshProUGUI targetText)
    {
        float remainingTime = duration;

        
        while (remainingTime > 0)
        {
            targetText.text = $"{type.ToString()}: {remainingTime:F1}s";
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        targetText.text = $"{type.ToString()}: 0";
    }
}
