using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;

[System.Serializable]
public struct EnemyToSpawn
{
    [Header("Enemy Properties")]
    public string name;
    
    [Header("Spawn Properties")]
    public int count;
    public SpawnPoint spawnPoint; 
}

[System.Serializable]
public class Wave
{
    public static int number = 0;
    [HideInInspector] public string name;

    public List<EnemyToSpawn> enemiesToSpawn;
    public float spawnInterval;

    private int enemyCount;
    public int EnemyCount { get { return enemyCount; } }

    /// <summary>
    /// Initialize wave variables
    /// </summary>
    public void Init()
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            enemyCount += enemiesToSpawn[i].count;
        }
    }

    /// <summary>
    /// Set parameters neccessary to every spawn points in wave settings
    /// </summary>
    public void SetSpawnParams()
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            enemiesToSpawn[i].spawnPoint.SetSpawnParams(enemiesToSpawn, spawnInterval);
        }
    }
}

public class SpawnManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public List<Wave> waves = new List<Wave>();
    public float timeBetweenWaves = 0;

    [Header("Debug Settings")]
    public bool debug = false;

    private float timeElapsed = 0;
    private float nextWaveTime = 0;
    private Wave currentWave;

    private static ObjectPooler objectPooler;

    public static float EnemyLeftToSpawn = 0;
    public static int EnemiesAlive = 0;
    public static bool HasFinishedSpawning = false;
    public static bool CanSpawn = false;
    public static bool m_Debug = false;

    private void Start()
    {
        // get reference to object pooler
        objectPooler = FindObjectOfType<ObjectPooler>();

        // update params to start spawning
        SpawnNextWave();
    }

    private void Update()
    {
        // if cannot spawn, do nothing
        if (!CanSpawn) return;

        // if still has wave left to spawn
        if (Wave.number < waves.Count)
        {
            // ... and is time for next wave
            if (timeElapsed > nextWaveTime)
            {
                SpawnNextWave();
            }
        }

        UpdateSpawnParams();
    }

    /// <summary>
    /// Check if has spawned all enemies
    /// </summary>
    private bool HasSpawnedAllEnemies()
    {
        HasFinishedSpawning = EnemyLeftToSpawn <= 0 && Wave.number >= waves.Count;
        return HasFinishedSpawning;
    }

    /// <summary>
    /// Update time elapsed and can spawn switch
    /// </summary>
    private void UpdateSpawnParams()
    {
        // if has spawned all enemies, set can spawn to false
        if (HasSpawnedAllEnemies())
        {
            CanSpawn = false;
        }
        // otherwise update time elapsed
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    /// <summary>
    /// Update params for next wave
    /// </summary>
    private void SpawnNextWave()
    {
        if (!CanSpawn) return;

        Wave.number++;
        print("Wave Number: " + Wave.number);
        currentWave = waves[Wave.number - 1];
        currentWave.Init();
        currentWave.SetSpawnParams();
        nextWaveTime = timeElapsed + currentWave.EnemyCount * currentWave.spawnInterval + timeBetweenWaves;
        EnemyLeftToSpawn = currentWave.EnemyCount;
        print("Enemy Left To Spawn: " + EnemyLeftToSpawn);
    }

    /// <summary>
    /// 1. Update name for easy usage...
    /// 2. If params are not correctly assigned, reassigned to base value
    /// </summary>
    private void UpdateWavesInEditor()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            if (waves[i].name == "")
                waves[i].name = "Wave " + (i + 1);

            if (waves[i].spawnInterval < 0)
                waves[i].spawnInterval = 0;

            for (int j = 0; j < waves[i].enemiesToSpawn.Count; j++)
            {
                if (waves[i].enemiesToSpawn[j].count < 0)
                    Debug.LogError("Enemy count should be a positive number. Error at "
                        + waves[i].enemiesToSpawn[j].name
                        + " in Spawn Manager/Wave Settings/Enemy Properties");
            }
        }

        m_Debug = debug;
    }

    private void OnValidate()
    {
        UpdateWavesInEditor();
    }
}
