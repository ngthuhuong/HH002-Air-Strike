using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GamePlayManager : Singleton<GamePlayManager>, MMEventListener<EEnemyDie>, MMEventListener<EGameRestart>, IResetable
{
    [Header("Game Components")]
    [SerializeField] private PlayerController player; // Reference to the player
    public PlayerController Player
    {
        get => player;
        set => player = value;
    }

    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private Transform enemyContainer; 
    
    public Transform enemyProjectileContainer;

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

    [SerializeField] private int remainingEnemies; // Track the number of enemies in the current level

    #region MonoBehaviour

    private void Start()
    {
        StartCoroutine(SpawnAllLevels());
        MMEventManager.TriggerEvent(new EGameStart());
        MMEventManager.TriggerEvent(new EPlaySound(AudioManager.SoundName.BGM_MainTheme));
    }

    private void OnEnable()
    {
        MMEventManager.RegisterAllCurrentEvents(this);
    }

    private void OnDisable()
    {
        MMEventManager.UnregisterAllCurrentEvents(this);
    }

    #endregion

    #region Public Methods

    public IEnumerator SpawnCurrentLevel()
    {
        if (DataManager.Instance.CurrentLevelId >= allLevels.Count)
        {
            Debug.LogError("Current level ID exceeds the number of available levels.");
            yield return null;
        }
        LevelData currentLevelData = allLevels[DataManager.Instance.CurrentLevelId];
        remainingEnemies = currentLevelData.enemySpawns.Count;
        

        foreach (var spawnData in currentLevelData.enemySpawns)
        {
            yield return new WaitForSeconds(spawnData.spawnTime);
            Instantiate(spawnData.enemyPrefab, spawnData.spawnPosition, Quaternion.identity, enemyContainer).GetComponent<EnemyController>();
        }
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
            yield return SpawnCurrentLevel();

            // Wait until the current level is complete
            yield return new WaitUntil(() => isLevelComplete);

            isLevelComplete = false; // Reset the condition for the next level
        }

        Debug.Log("All levels completed!");
    }
    
    public void OnMMEvent(EEnemyDie eventType)
    {
        Debug.Log($"GamePlayManager received EEnemyDie event");
        OnEnemyDefeated();
    }

    public void OnMMEvent(EGameRestart eventType)
    {
        ResetState();
    }

    #endregion

    #region IResetable

    public bool isActivated { get; set; }
    public void ResetState()
    {
       Player.ResetState();
       
       // Destroy all enemies
       foreach (Transform child in enemyContainer.transform)
       {
           Destroy(child.gameObject);
       }
       
       //Destroy all enemy projectiles
         foreach (Transform child in enemyProjectileContainer.transform)
         {
              Destroy(child.gameObject);
         }
       
       remainingEnemies = 0;
       isLevelComplete = false;
    }

    public void StartState()
    {
        throw new NotImplementedException();
    }

    public void EndState()
    {
        throw new NotImplementedException();
    }
    
    #endregion
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(GamePlayManager))]
public class GamePlayManagerInspector : Editor
{
    private GamePlayManager gamePlayManager;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        gamePlayManager = (GamePlayManager)target;
        GUILayout.Space(10);
        GUILayout.Label("Current Events Listening", EditorStyles.boldLabel);

        List<string> events = MMEventManager.GetCurrentEvents(gamePlayManager);
        if (events.Count > 0)
        {
            foreach (string eventName in events)
            {
                GUILayout.Label($"- {eventName.Substring(37)}");
            }
        }
        else
        {
            GUILayout.Label("No events currently being listened to.");
        }

        Repaint(); // Ensure the Inspector updates in real-time
    }
}
#endif