using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>, MMEventListener<EEnemyDie>
{
    [Header("Game Components")]
    [SerializeField] private PlayerController player; // Reference to the player
    public PlayerController Player
    {
        get => player;
        set => player = value;
    }

    [SerializeField] private EnemySpawner enemySpawner; // Reference to the enemy spawner
    public EnemySpawner EnemySpawner
    {
        get => enemySpawner;
        set => enemySpawner = value;
    }
    
    public GameObject enemyProjectileContainer;

    public Transform centerPoint;

    [Header("Level Details")]
    [SerializeField] private List<LevelData> allLevels;

    [Header("Flags")]
    [SerializeField] private bool isLevelComplete = false;
    public bool IsLevelComplete
    {
        get => isLevelComplete;
        set => isLevelComplete = value;
    }

    private int remainingEnemies; // Track the number of enemies in the current level

    #region MonoBehaviour

    private void Start()
    {
        StartCoroutine(SpawnAllLevels());
        MMEventManager.TriggerEvent(new EGameStart());
        MMEventManager.TriggerEvent(new EPlaySound(AudioManager.SoundName.BGM_MainTheme));
    }

    private void OnEnable()
    {
        this.MMEventStartListening<EEnemyDie>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<EEnemyDie>();
    }

    #endregion

    #region Public Methods

    public void SpawnCurrentLevel()
    {
        if (DataManager.Instance.CurrentLevelId >= allLevels.Count)
        {
            Debug.LogError("Current level ID exceeds the number of available levels.");
            return;
        }
        LevelData currentLevelData = allLevels[DataManager.Instance.CurrentLevelId];
        remainingEnemies = currentLevelData.enemySpawns.Count;
        StartCoroutine(SpawnEnemies(currentLevelData));
    }

    public void OnEnemySpawned()
    {
        remainingEnemies--;
    }

    public void OnEnemyDefeated()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            isLevelComplete = true; // Mark the level as complete
        }
    }

    #endregion

    #region Private Methods

    private IEnumerator SpawnAllLevels()
    {
        for (int i = 0; i < allLevels.Count; i++)
        {
            DataManager.Instance.CurrentLevelId = i; // Set the current level ID
            SpawnCurrentLevel();

            // Wait until the current level is complete
            yield return new WaitUntil(() => isLevelComplete);

            isLevelComplete = false; // Reset the condition for the next level
        }

        Debug.Log("All levels completed!");
    }

    private IEnumerator SpawnEnemies(LevelData levelData)
    {
        remainingEnemies = 0; // Reset enemy count for the new level

        foreach (var spawnData in levelData.enemySpawns)
        {
            yield return new WaitForSeconds(spawnData.spawnTime);
            EnemyController enemy = Instantiate(spawnData.enemyPrefab, spawnData.spawnPosition, Quaternion.identity).GetComponent<EnemyController>();
            // enemy.centerPoint = centerPoint;

            //OnEnemySpawned(); // Increment enemy count
        }
    }

    #endregion

    public void OnMMEvent(EEnemyDie eventType)
    {
        OnEnemyDefeated();
    }
}