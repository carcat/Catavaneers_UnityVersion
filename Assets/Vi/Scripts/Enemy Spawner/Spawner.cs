using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public static int number = 0;
    [HideInInspector] public string name;

    [Header("Enemy Properties")]
    public List<string> enemyNames;

    [Header("Spawn Properties")]
    public int enemyCount;
    public float spawnInterval;
}

public class Spawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public List<Wave> waves = new List<Wave>();
    public float timeBetweenWaves = 0;

    [Header("Spawn Settings")]
    public float spawnRadius = 0;

    private float timeElapsed = 0;
    private float nextWaveTime = 0;
    private float nextSpawnTime = 0;
    private float enemyLeftToSpawn = 0;
    private int randomIndex = 0;
    private Wave currentWave;
    private Vector2 randomPosIn2DCircle;

    private static ObjectPooler objectPooler;
    public static int EnemiesAlive = 0;
    public static bool HasFinishedSpawning = false;
    public static bool canSpawn = false;

    private void Start()
    {
        // get reference to object pooler
        objectPooler = FindObjectOfType<ObjectPooler>();

        // update params to start spawning
        SpawnNextWave();
    }

    private void Update()
    {
        // if still has enemy to spawn and is time to spawn
        if (enemyLeftToSpawn > 0)
        {
            if (timeElapsed > nextSpawnTime)
            {
                randomIndex = Random.Range(0, currentWave.enemyNames.Count);
                Spawn(currentWave.enemyNames[randomIndex], GenerateRandomPosition(spawnRadius));
            }
        }

        // if still has wave left to spawn and is time for next wave
        if (Wave.number < waves.Count)
        {
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
        HasFinishedSpawning = enemyLeftToSpawn <= 0 && Wave.number >= waves.Count;
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
            canSpawn = false;
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
        if (!canSpawn) return;

        Wave.number++;
        print("Wave Number: " + Wave.number);
        currentWave = waves[Wave.number - 1];
        nextWaveTime = timeElapsed + currentWave.enemyCount * currentWave.spawnInterval + timeBetweenWaves;
        enemyLeftToSpawn = currentWave.enemyCount;
        print("Enemy Left To Spawn: " + enemyLeftToSpawn);
    }

    /// <summary>
    /// Spawn a specific enemy from pool at a predefined position
    /// </summary>
    /// <param name="name"> Name of the enemy to pull from pool </param>
    /// <param name="position"> Position to spawn at </param>
    private void Spawn(string name, Vector3 position)
    {
        if (!canSpawn) return;

        objectPooler.SpawnFromPool(name, position, Quaternion.identity);
        nextSpawnTime = timeElapsed + currentWave.spawnInterval;
        enemyLeftToSpawn--;
        EnemiesAlive++;
        print("Enemy Left To Spawn: " + enemyLeftToSpawn);
    }

    /// <summary>
    /// Returns a random position with the same height (y coord) of this object within a radius
    /// </summary>
    /// <param name="radius"> Limit radius that the random generated position will be in </param>
    private Vector3 GenerateRandomPosition(float radius)
    {
        randomPosIn2DCircle = Random.insideUnitCircle * radius;
        return new Vector3(randomPosIn2DCircle.x, transform.position.y, randomPosIn2DCircle.y);
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

            if (waves[i].enemyCount < 0)
                waves[i].enemyCount = 0;

            if (waves[i].spawnInterval < 0)
                waves[i].spawnInterval = 0;
        }
    }

    private void OnValidate()
    {
        UpdateWavesInEditor();
    }
}
