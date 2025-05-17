using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIMenu : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button startButton;
    [SerializeField] private Button claimDailyRewardButton;
    
    [Header("Daily Reward")]
    [SerializeField] private TextMeshProUGUI todayDate;
    [SerializeField] private int dailyRewardAmount = 100;

    #region MonoBehaviour

    private void OnEnable()
    {
        startButton.onClick.AddListener(OnClickStart);
        claimDailyRewardButton.onClick.AddListener(OnClickClaimDailyReward);

        UpdateUI();
    }

    

    private void OnDisable()
    {
        startButton.onClick.RemoveAllListeners();
        claimDailyRewardButton.onClick.RemoveAllListeners();
    }

    #endregion

    #region Button Events

    private void OnClickClaimDailyReward()
    {
        if (CanClaimDailyReward())
        {
            // Grant the reward
            int currentCoins = PlayerPrefs.GetInt(PrefString.CURRENT_COIN, 0);
            PlayerPrefs.SetInt(PrefString.CURRENT_COIN, currentCoins + dailyRewardAmount);

            // Update the last claimed date
            PlayerPrefs.SetString(PrefString.LAST_CLAIM_DATE, DateTime.Now.ToString("yyyy-MM-dd"));
            PlayerPrefs.Save();

            Debug.Log($"Daily reward claimed! You received {dailyRewardAmount} coins.");
        }
        else
        {
            Debug.Log("Daily reward already claimed for today.");
        }
    }

    private void OnClickStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    #endregion

    #region Private Methods
    private void UpdateUI()
    {
        todayDate.text = $"Today: {DateTime.Now.ToString("MM/dd/yyyy")}";
    }

    #endregion

    #region Public Methods

    public bool CanClaimDailyReward()
    {
        // Get the last claimed date from PlayerPrefs
        string lastClaimDate = PlayerPrefs.GetString(PrefString.LAST_CLAIM_DATE, string.Empty);

        if (string.IsNullOrEmpty(lastClaimDate))
        {
            // No record of last claim, reward is available
            return true;
        }

        // Parse the saved date
        DateTime lastClaimDateTime = DateTime.Parse(lastClaimDate);

        // Check if the current date is different from the last claimed date
        return DateTime.Now.Date > lastClaimDateTime.Date;
    }

    #endregion
}
