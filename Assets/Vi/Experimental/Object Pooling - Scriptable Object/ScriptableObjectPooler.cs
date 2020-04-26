using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Experimental
{
    [System.Serializable]
    public class Organizer
    {
        public PoolObject poolObject;
        public Transform parent;
    }

    public class ScriptableObjectPooler : MonoBehaviour
    {
        public Dictionary<string, Queue<GameObject>> PoolDictionary;
        public List<Organizer> objectPools = new List<Organizer>();
        private float activeTime;


        #region SINGLETON
        private static ScriptableObjectPooler instance;
        public static ScriptableObjectPooler Instance { get { return instance; } }


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


        private void PopulateObjectPools()
        {
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Organizer pool in objectPools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.poolObject.amount; i++)
                {
                    GameObject go = Instantiate(pool.poolObject.prefab, pool.parent);
                    go.SetActive(false);
                    objectPool.Enqueue(go);
                }

                PoolDictionary.Add(pool.poolObject.objectName, objectPool);
            }
        }


        public GameObject GetGameObject(string poolName)
        {
            return PoolDictionary[poolName].Dequeue();
        }


        public GameObject SpawnFromPool(string poolName, Vector3 position, Quaternion rotation)
        {
            GameObject go = PoolDictionary[poolName].Dequeue();

            if (!go)
            {
                Debug.LogWarning("Pool with name \"" + poolName + "\" does not exist.");
                return null;
            }

            go.SetActive(true);
            go.transform.position = position;
            go.transform.rotation = rotation;

            PoolDictionary[poolName].Enqueue(go);

            activeTime = GetPool(poolName).activeTime;
            if (activeTime != 0)
                StartCoroutine(SetInactive(go, activeTime));

            return go;
        }


        private PoolObject GetPool(string poolName)
        {
            foreach (Organizer pool in objectPools)
            {
                if (poolName == pool.poolObject.objectName)
                {
                    return pool.poolObject;
                }
            }

            return null;
        }


        public GameObject SpawnFromPool(string poolName, Vector3 position, Quaternion rotation, float activeTime)
        {
            GameObject go = PoolDictionary[poolName].Dequeue();

            if (!go)
            {
                Debug.LogWarning("Pool with name \"" + poolName + "\" does not exist.");
                return null;
            }

            go.SetActive(true);
            go.transform.position = position;
            go.transform.rotation = rotation;

            PoolDictionary[poolName].Enqueue(go);

            StartCoroutine(SetInactive(go, activeTime));

            return go;
        }


        private IEnumerator SetInactive(GameObject spawnedObject, float timer)
        {
            yield return new WaitForSeconds(timer);
            spawnedObject.SetActive(false);

            if (spawnedObject.transform.parent)
            {
                spawnedObject.transform.position = spawnedObject.transform.parent.position;
                spawnedObject.transform.rotation = spawnedObject.transform.parent.rotation;
            }
        }
    }
}