using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;


public class DataManager : Singleton<DataManager>, MMEventListener<EEarnScore>, MMEventListener<EGameOver>,MMEventListener<EEnemyDie>, MMEventListener<EEarnResource>, MMEventListener<EAchievementUnlocked>, MMEventListener<EGameStart>
{
    [SerializeField] private int highScore;
    
    public int HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            MMEventManager.TriggerEvent(new EDataChanged());    
        }
    }
    
    [SerializeField] private int currentScore;

    public int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = value;
            MMEventManager.TriggerEvent(new EDataChanged());    
        }
    }

    [Header("Leveling")] 
    [SerializeField] private int currentLevelId;
    public int CurrentLevelId
    {
        get => currentLevelId;
        set
        {
            currentLevelId = value;
            MMEventManager.TriggerEvent(new EDataChanged());    
        }
    }

    [SerializeField] private int currentCoin;
    public int CurrentCoin 
    {
        get => PlayerPrefs.GetInt(PrefString.CURRENT_COIN, 0);
        set
        {
            PlayerPrefs.SetInt(PrefString.CURRENT_COIN, value);
            MMEventManager.TriggerEvent(new EDataChanged());    
        }
    }
    
    public int GamesPlayed
    {
        get => PlayerPrefs.GetInt(PrefString.GAMES_PLAYED, 0);
        set
        {
            PlayerPrefs.SetInt(PrefString.GAMES_PLAYED, value);
            MMEventManager.TriggerEvent(new EDataChanged());    
        }
    }
    
    #region MonoBehaviour

    private void OnEnable()
    {
        this.MMEventStartListening<EEarnScore>();
        this.MMEventStartListening<EGameOver>();
        this.MMEventStartListening<EEnemyDie>();
        this.MMEventStartListening<EEarnResource>();
        this.MMEventStartListening<EAchievementUnlocked>();
        this.MMEventStartListening<EGameStart>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EEarnScore>();
        this.MMEventStopListening<EGameOver>();
        this.MMEventStopListening<EEnemyDie>();
        this.MMEventStopListening<EEarnResource>();
        this.MMEventStopListening<EAchievementUnlocked>();
        this.MMEventStopListening<EGameStart>();
    }

    #endregion

    #region Events Listen

    public void OnMMEvent(EEarnScore eventType)
    {
        Debug.Log($"DataManager receive event {eventType}");
        CurrentScore += 1;
    }

    public void OnMMEvent(EGameOver eventType)
    {
        // Cap nhat lai high score
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (HighScore < CurrentScore)
        {
            PlayerPrefs.SetInt("HighScore", CurrentScore);
            PlayerPrefs.Save();
            HighScore = CurrentScore;
        }
    }
    
    public void OnMMEvent(EEnemyDie eventType)
    {
        CurrentScore += 1;
        
    }

    public void OnMMEvent(EEarnResource eventType)
    {
        CurrentCoin += eventType.amount;
    }

    public void OnMMEvent(EAchievementUnlocked eventType)
    {
        foreach (Achievement achievement in AchievementManager.Instance.achievements)
        {
            if (achievement.name == eventType.achievementName)
            {
                if (achievement.rewardType == RewardPoolCoin.RewardType.Coin)
                {
                    CurrentCoin += achievement.rewardValue;
                }
            }
        }
    }

    public void OnMMEvent(EGameStart eventType)
    {
        GamesPlayed += 1;
    }

    #endregion

    
}

public class PrefString
{
    public const string CURRENT_COIN = "CURRENT_COIN";
    public const string GAMES_PLAYED = "GAMES_PLAYED";
}