using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    [System.Serializable]
    public class Pool
    {
        public string Key;
        public GameObject Prefab;
        public int size;
    }

    public Pool[] Pools;
    public Dictionary<string, Queue<GameObject>> poolsDict { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public void Initialize()
    {
        poolsDict = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolsDict.Add(pool.Key, objectPool);
        }
    }

    public GameObject SpawnFromPool(string key, Vector3 position, Quaternion rotation)
    {
        if (!poolsDict.ContainsKey(key))
        {
            Debug.LogWarning("Pool with tag " + key + " doesn`t exist");
            return null;
        }

        GameObject objToSpawn = poolsDict[key].Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolsDict[key].Enqueue(objToSpawn);

        return objToSpawn;
    }
}