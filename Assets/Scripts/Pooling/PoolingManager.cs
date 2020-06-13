using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{

    [SerializeField]
    private List<Pool> pools = null;
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Start()
    {
        foreach (Pool pool in pools)
        {
            if (pool.tag == null || pool.desiredPrefab == null)
            {
                Debug.LogWarning("Pool " + pool.tag + " does not meet the requirements");
                continue;
            }
            GameObject parentForPooledObjects = new GameObject(pool.tag + "Parent");
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject obj;
            for (int i = 0; i < pool.size; i++)
            {
                obj = Instantiate(pool.desiredPrefab, parentForPooledObjects.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(tag) == false)
        {
            Debug.LogWarning("Pool doesnt exist: " + tag);
            return null;

        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    public GameObject SpawnFromPool(string tag)
    {
        return SpawnFromPool(tag, Vector3.zero, Quaternion.identity);
    }

    public void SpawnFromPool(string tag, Vector3 position, Quaternion rotation, int count)
    {
        if (poolDictionary.ContainsKey(tag) == false)
        {
            Debug.LogWarning("Pool doesnt exist: " + tag);
            return;

        }
        for (int i = 0; i < count; i++)
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);
            poolDictionary[tag].Enqueue(objectToSpawn);

        }
    }
    private void Update()
    {

    }
}
