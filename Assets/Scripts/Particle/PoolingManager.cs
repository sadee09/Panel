using System;
using UnityEngine;
using System.Collections.Generic;

public class PoolingManager : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public int size;
        public GameObject prefab;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    
    public static PoolingManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetFromPool(string mytag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(mytag))
        {
            Debug.Log("It doesnt contain tag" + mytag);
            return null;
        }
        GameObject objectToSpawn = poolDictionary[mytag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        
        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        poolDictionary[mytag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}

