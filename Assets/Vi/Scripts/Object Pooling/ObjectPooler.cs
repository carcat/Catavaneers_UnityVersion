using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<ObjectPool> objectPools = new List<ObjectPool>();
    private float activeTime;

    // Make this the one instance managing pooled objects throughout levels
    #region SINGLETON
    private static ObjectPooler instance;
    public static ObjectPooler Instance { get { return instance; } }


    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion


    private void Start()
    {
        PopulateObjectPools();
    }

    /// <summary>
    /// Populate object pools
    /// </summary>
    private void PopulateObjectPools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (ObjectPool pool in objectPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.amount; i++)
            {
                GameObject go = Instantiate(pool.prefab, pool.parent);
                go.SetActive(false);
                objectPool.Enqueue(go);
            }

            poolDictionary.Add(pool.name, objectPool);
        }
    }

    /// <summary>
    /// Get reference to an object in a pool
    /// </summary>
    /// <param name="name"> Name of the pool to get object from </param>
    public GameObject GetGameObject(string name)
    {
        return poolDictionary[name].Dequeue();
    }

    /// <summary>
    /// Spawn object from pool
    /// </summary>
    /// <param name="name"> Name of the pool to pull object from </param>
    /// <param name="position"> Position to spawn at </param>
    /// <param name="rotation"> Rotation to spawn at </param>
    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        GameObject go = poolDictionary[name].Dequeue();

        if (!go)
        {
            Debug.LogWarning("Pool with name \"" + name + "\" does not exist.");
            return null;
        }

        go.SetActive(true);
        go.transform.position = position;
        go.transform.rotation = rotation;

        poolDictionary[name].Enqueue(go);

        activeTime = GetPool(name).activeTime;
        if (activeTime != 0)
            StartCoroutine(SetInactive(go, activeTime));

        return go;
    }

    /// <summary>
    /// Get reference to an object pool
    /// </summary>
    /// <param name="name"> Name of the pool to be returned </param>
    private ObjectPool GetPool(string name)
    {
        foreach (ObjectPool pool in objectPools)
        {
            if (name == pool.name)
            {
                return pool;
            }
        }

        return null;
    }

    /// <summary>
    /// Spawn object from pool with active timer
    /// </summary>
    /// <param name="name"> Name of the pool to pull object from </param>
    /// <param name="position"> Position to spawn at </param>
    /// <param name="rotation"> Rotation to spawn at </param>
    /// <param name="timer"> Time before the spawned object will be deactivated </param>
    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation, float timer)
    {
        GameObject go = poolDictionary[name].Dequeue();

        if (!go)
        {
            Debug.LogWarning("Pool with name \"" + name + "\" does not exist.");
            return null;
        }

        go.SetActive(true);
        go.transform.position = position;
        go.transform.rotation = rotation;

        poolDictionary[name].Enqueue(go);

        StartCoroutine(SetInactive(go, timer));

        return go;
    }

    /// <summary>
    /// Deactivate game object after a certain amount of time and reset its position to original position
    /// </summary>
    /// <param name="gameObject"> The game object to be deactivated </param>
    /// <param name="timer"> Time before the spawned object will be deactivated </param>
    private IEnumerator SetInactive(GameObject gameObject, float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);

        if (gameObject.transform.parent)
        {
            gameObject.transform.position = gameObject.transform.parent.position;
            gameObject.transform.rotation = gameObject.transform.parent.rotation;
        }
    }

    /// <summary>
    /// 1. Update name for easy usage...
    /// 2. If params are not correctly assigned, reassigned to base value
    /// </summary>
    private void UpdatePooledObjectInEditor()
    {
        for (int i = 0; i < objectPools.Count; i++)
        {
            if (objectPools[i].prefab)
                objectPools[i].name = objectPools[i].prefab.name;
            else
                objectPools[i].name = "";

            if (objectPools[i].amount < 0)
                objectPools[i].amount = 0;
        }
    }


    private void OnValidate()
    {
        UpdatePooledObjectInEditor();
    }
}