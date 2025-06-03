using System;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

public class RewardPoolCoin : Singleton<RewardPoolCoin>, IResetable, MMEventListener<EGameRestart>
{
    [System.Serializable]
    public class Reward
    {
        public RewardType rewardType; // Name of the reward
        public GameObject prefab; // Prefab for the reward
        public int initialSize;   // Initial pool size for this reward
    }
    
    public enum RewardType
    {
        Coin,
        Gem,
        PowerUp_Heal,
        PowerUp_SpeedBoost,
        PowerUp_Shield
    }

    [Header("Reward Pool Settings")]
    [SerializeField] private List<Reward> rewards; // List of reward types

    private Dictionary<RewardType, List<GameObject>> rewardPools = new Dictionary<RewardType, List<GameObject>>();

    #region MonoBehaviour

    private void OnEnable()
    {
        MMEventManager.RegisterAllCurrentEvents(this);
    }

    private void OnDisable()
    {
        MMEventManager.UnregisterAllCurrentEvents(this);
    }

    private void Start()
    {
        // Initialize pools for each reward type
        foreach (var reward in rewards)
        {
            List<GameObject> pool = new List<GameObject>();

            for (int i = 0; i < reward.initialSize; i++)
            {
                GameObject obj = Instantiate(reward.prefab, transform);
                obj.SetActive(false);
                pool.Add(obj);
            }

            rewardPools.Add(reward.rewardType, pool);
        }
    }

    #endregion

    // Get an object from the pool
    public GameObject GetObject(RewardType rewardType, Vector3 position, Quaternion rotation)
    {
        if (!rewardPools.ContainsKey(rewardType))
        {
            Debug.LogWarning($"Reward pool with type {rewardType} doesn't exist.");
            return null;
        }

        var pool = rewardPools[rewardType];
        GameObject obj;

        if (pool.Count > 0)
        {
            obj = pool[0];
            pool.RemoveAt(0);
        }
        else
        {
            // Find the reward prefab to instantiate a new one
            var reward = rewards.Find(r => r.rewardType == rewardType);
            if (reward == null)
            {
                Debug.LogWarning($"Reward prefab with type {rewardType} not found.");
                return null;
            }

            obj = Instantiate(reward.prefab, transform);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }
    
    public GameObject GetRandomReward(Vector3 position, Quaternion rotation)
    {
        if (rewardPools.Count == 0)
        {
            Debug.LogWarning("No rewards available in the pool.");
            return null;
        }

        // Create a weighted list of reward types
        List<RewardType> weightedRewardTypes = new List<RewardType>();
        foreach (var rewardType in rewardPools.Keys)
        {
            if (rewardType == RewardType.Coin)
            {
                // Add the coin reward type twice to double its probability
                weightedRewardTypes.Add(rewardType);
                weightedRewardTypes.Add(rewardType);
            }
            else
            {
                weightedRewardTypes.Add(rewardType);
            }
        }

        // Select a random reward type from the weighted list
        var randomRewardType = weightedRewardTypes[Random.Range(0, weightedRewardTypes.Count)];
        return GetObject(randomRewardType, position, rotation);
    }

    // Return an object to the pool
    public void ReturnObject(RewardType rewardType, GameObject obj)
    {
        if (!rewardPools.ContainsKey(rewardType))
        {
            Debug.LogWarning($"Reward pool with type {rewardType} doesn't exist.");
            return;
        }

        if (rewardPools[rewardType].Contains(obj))
        {
            obj.SetActive(false);
            rewardPools[rewardType].Add(obj);
        }
        else
        {
            Debug.LogWarning($"Object is already in the pool for reward type {rewardType}.");
        }
    }

    #region IResetable

    public bool isActivated { get; set; }
    public void ResetState()
    {
        Debug.Log($"Reset State of RewardPoolCoin");
        // Iterate through each reward type in the pool
        foreach (var rewardType in rewardPools.Keys.ToList())
        {
            // Get the list of objects for the current reward type
            var pool = rewardPools[rewardType];

            // Find all active objects in the pool
            var activeObjects = pool.Where(obj => obj.activeSelf).ToList();

            // Deactivate and return each active object to the pool
            foreach (var obj in activeObjects)
            {
                ReturnObject(rewardType, obj);
            }
        }
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

    #region Events Listen

    public void OnMMEvent(EGameRestart eventType)
    {
        ResetState();
    }

    #endregion
}