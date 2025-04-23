using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<LevelData> allLevels; // List of all levels

    

    public void SpawnCurrentLevel()
    {
        if (DataManager.Instance.CurrentLevelId >= allLevels.Count)
        {
            Debug.LogError("Current level ID exceeds the number of available levels.");
            return;
        }
        LevelData currentLevelData = allLevels[DataManager.Instance.CurrentLevelId];
        StartCoroutine(SpawnEnemies(currentLevelData));
    }

    private IEnumerator SpawnEnemies(LevelData levelData)
    {
        foreach (var spawnData in levelData.enemySpawns)
        {
            yield return new WaitForSeconds(spawnData.spawnTime);
            Instantiate(spawnData.enemyPrefab, spawnData.spawnPosition, Quaternion.identity);
        }
    }

}