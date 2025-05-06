using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolObject : Singleton<ProjectilePoolObject>
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject prefab; // Prefab to pool
    [SerializeField] private int initialSize = 50; // Initial pool size

    [SerializeField] private List<GameObject> pool = new List<GameObject>();

    #region MonoBehaviour

    private void Start()
    {
        // Pre-instantiate objects and add them to the pool
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.SetParent(gameObject.transform);
            pool.Add(obj);
        }
    }

    #endregion

    // Get an object from the pool
    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj;

        if (pool.Count > 0)
        {
            obj = pool[0];
            pool.RemoveAt(0);
        }
        else
        {
            obj = Instantiate(prefab);
            obj.transform.SetParent(gameObject.transform);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    // Return an object to the pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }
}