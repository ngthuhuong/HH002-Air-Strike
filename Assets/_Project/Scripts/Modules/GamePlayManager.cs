using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
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
    #region MonoBehaviour

    #endregion

}