using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    [Serializable]
    public class EnemySpawnData
    {
        public GameObject enemyPrefab; // The enemy prefab to spawn
        public Vector2 spawnPosition; // The position to spawn the enemy
        public float spawnTime; // The time (in seconds) to spawn the enemy
    }

    public List<EnemySpawnData> enemySpawns = new List<EnemySpawnData>();
}