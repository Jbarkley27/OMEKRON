using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour 
{
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public float minSpawnDelayTime = .1f;
    public float maxSpawnDelayTime = 1f;
    public static SpawnManager instance;
    public List<EnemyDNABase> enemies = new List<EnemyDNABase>();
    public bool isSpawning = false;

    public enum EnemyID
    {
        Nucleotide,
        Virus,
        Bacteria,
        Null
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Spawn Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // find all the spawn points with the tag "spawn-point"
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("spawn-point");
        foreach (GameObject spawnPointObject in spawnPointObjects)
        {
            SpawnPoint spawnPoint = spawnPointObject.GetComponent<SpawnPoint>();
            spawnPoints.Add(spawnPoint);
        }
    }

    public float GetSpawnDelayTime()
    {
        return Random.Range(minSpawnDelayTime, maxSpawnDelayTime);
    }

    public IEnumerator StartWave(Wave wave)
    {
        // make sure we're not already spawning
        if (isSpawning)
        {
            yield break;
        }

        isSpawning = true;

        yield return new WaitForSeconds(wave.timeToThisWave);

        foreach(EnemyID enemyID in wave.enemies)
        {
            SpawnNextEnemyFromWave(enemyID);
            yield return new WaitForSeconds(GetSpawnDelayTime());
        }

        isSpawning = false;
    }

    public void SpawnNextEnemyFromWave(EnemyID enemyID)
    {
        ShuffleSpawnPoints();

        SpawnPoint randomSpawnPoint = null;

        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            if(!spawnPoint.isOccupied)
            {
                randomSpawnPoint = spawnPoint;
                break;
            }
        }

        if (randomSpawnPoint == null)
        {
            Debug.LogError("No available spawn points to spawn enemy.");
            return;
        }

        Debug.Log("Spawning enemy: " + enemyID);
        GameObject enemyPrefab = EnemyLibrary.instance.GetEnemyPrefabFromID(enemyID);
        GameObject enemy = Instantiate(enemyPrefab, randomSpawnPoint.transform.position, Quaternion.identity);

        EnemyDNABase enemyDNA = enemy.GetComponent<EnemyDNABase>();
        enemies.Add(enemyDNA);
    }

    public void RemoveEnemy(EnemyDNABase enemy)
    {
        enemies.Remove(enemy);
    }

    public void ShuffleSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnPoint temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Count);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }

    public int GetTotalCurrentPower()
    {
        int totalPower = 0;
        foreach (EnemyDNABase enemy in enemies)
        {
            totalPower += enemy.power;
        }
        return totalPower;
    }
}