using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : Singleton<PoolingManager>
{
    public enum PoolTag
    {
        PlayerProjectile,
        EnemyProjectile,
        Coin
    }

    [System.Serializable]
    public class Pool
    {
        public PoolTag tag;
        public GameObject prefab;
        public int size;
    }

    [Header("Pools")]
    [SerializeField] private List<Pool> pools;

    private Dictionary<PoolTag, List<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<PoolTag, List<GameObject>>();

        foreach (var pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetObject(PoolTag tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        var objectPool = poolDictionary[tag];

        if (objectPool.Count == 0)
        {
            Debug.LogWarning($"No available objects in pool with tag {tag}.");
            return null;
        }

        GameObject obj = objectPool[0];
        objectPool.RemoveAt(0);

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;
    }

    public void ReturnObject(PoolTag tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return;
        }

        obj.SetActive(false);
        poolDictionary[tag].Add(obj);
    }
}