using System;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>, MMEventListener<EEnemyDie>, MMEventListener<EGameStart>
{
    public List<Achievement> achievements; // List of all achievements

    #region MonoBehaviour

    private void OnEnable()
    {
        this.MMEventStartListening<EEnemyDie>();
        this.MMEventStartListening<EGameStart>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EEnemyDie>();
        this.MMEventStopListening<EGameStart>();
    }

    #endregion
    
    public void IncrementAchievementProgress(AchievementType type, int incrementValue)
    {
        Debug.Log($"AchievementManager: type {type} increment value {incrementValue}");
        foreach (var achievement in achievements)
        {
            if (achievement.type == type)
            {
                int currentValue = PlayerPrefs.GetInt(achievement.name, 0);
                currentValue += incrementValue;

                if (currentValue >= achievement.targetValue)
                {
                    UnlockAchievement(achievement);
                }
                else
                {
                    PlayerPrefs.SetInt(achievement.name, currentValue);
                }

                PlayerPrefs.Save();
            }
        }
    }

    private void UnlockAchievement(Achievement achievement)
    {
        Debug.Log($"AchievementManager: unlocked achievement {achievement.name}");
        if (PlayerPrefs.GetInt(achievement.name + "_Unlocked", 0) == 1)
            return; // Already unlocked

        PlayerPrefs.SetInt(achievement.name + "_Unlocked", 1);
        MMEventManager.TriggerEvent(new EAchievementUnlocked(achievement.type, achievement.name));
    }

    #region Events Listen

    public void OnMMEvent(EEnemyDie eventType)
    {
        IncrementAchievementProgress(AchievementType.EnemyKill, 1);
    }

    public void OnMMEvent(EGameStart eventType)
    {
        IncrementAchievementProgress(AchievementType.GameNum, 1);
    }

    #endregion
}

public enum AchievementType
{
    EnemyKill = 0,
    GameNum = 1
}