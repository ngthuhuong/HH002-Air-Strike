using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    private const float SpawnMinX = -3.5f; // Minimum X position for spawning
    private const float SpawnMaxX = 2.87f; // Maximum X position for spawning
    private const float SpawnFixedY = 9f; // Fixed Y position for spawning
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
            float randomX = Random.Range(SpawnMinX, SpawnMaxX);
            float fixedY = SpawnFixedY;
            Vector2 spawnPosition = new Vector2(randomX, fixedY);

            // Instantiate the enemy at the calculated position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    #endregion
}