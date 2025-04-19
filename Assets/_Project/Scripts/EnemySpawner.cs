using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab; // Prefab for the enemy
    public float spawnInterval = 2f; // Time interval between spawns

    #region MonoBehaviour

    void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnEnemies());
    }

    #endregion

    #region Private Methods

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            // Generate a random X position within the range [-4, 2.87]
            float randomX = Random.Range(-3.5f, 2.87f);
            float fixedY = 9f; // Y position is always 9
            Vector3 spawnPosition = new Vector3(randomX, fixedY, 0f);

            // Instantiate the enemy at the calculated position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    #endregion
}