using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GUIProfile : GUIBase
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerCoinText;
    [SerializeField] private Image playerAvatarImage;
    [SerializeField] private Button closeProfileButton;

    [Header("Player Data")]
    private PlayerData playerData; // ScriptableObject or data source for player info

    private void OnEnable()
    {
        playerData = DataManager.Instance.PlayerData;
        UpdateProfileUI();
        closeProfileButton.onClick.AddListener(OnClickClose);
        
        Time.timeScale = 0f;
    }

    private void OnClickClose()
    {
        Hide();
    }

    private void OnDisable()
    {
        closeProfileButton.onClick.RemoveAllListeners();
        Time.timeScale = 1f;
    }

    public void UpdateProfileUI()
    {
        if (playerData != null)
        {
            playerNameText.text = playerData.PlayerName;
            playerCoinText.text = $"Coins: {DataManager.Instance.CurrentCoin}";
            playerAvatarImage.sprite = playerData.Avatar;
        }
    }
}