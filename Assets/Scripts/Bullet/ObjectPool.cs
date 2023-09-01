using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 20;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPos;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            // Rotate the bullet's initial orientation by 90 degrees in the z-direction
            Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
            GameObject obj = Instantiate(bulletPrefab, bulletPos.position, rotation);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        for(int i = 0; i< pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // If no inactive objects are found, create a new one and add it to the pool
        Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
        GameObject newObj = Instantiate(bulletPrefab, bulletPos.position, rotation);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }
}
