using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardPoolCoin : Singleton<RewardPoolCoin>
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

        obj.SetActive(false);
        rewardPools[rewardType].Add(obj);
    }
}