using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler SharedIntance;

    private void Awake()
    {
        SharedIntance = this;
    }
    #endregion
    
    public List<Pool> pools;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            var objectPool = new Queue<GameObject>();
            for (var i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string objectTag, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(objectTag))
        {
            Debug.LogWarning("Pool with tag " + objectTag + " does not exist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[objectTag].Dequeue();
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        
        poolDictionary[objectTag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
