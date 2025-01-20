using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour 
{
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

        yield return new WaitForSeconds(.1f);

        foreach(EnemyID enemyID in wave.enemies)
        {
            SpawnNextEnemyFromWave(enemyID);
            yield return new WaitForSeconds(GetSpawnDelayTime());
        }

        isSpawning = false;
    }

    public void SpawnNextEnemyFromWave(EnemyID enemyID)
    {
        EnemyFormationPosition randomSpawnPoint = EnemyFormation.instance.GetNextAvailablePosition();
        if (randomSpawnPoint == null)
        {
            Debug.LogError("No available spawn points");
            return;
        }

        // now that we know we have a spawn point, spawn the enemy and set point to occupied
        randomSpawnPoint.SetOccupied(true);


        Debug.Log("Spawning enemy: " + enemyID);
        GameObject enemyPrefab = EnemyLibrary.instance.GetEnemyPrefabFromID(enemyID);
        GameObject enemy = Instantiate(enemyPrefab, randomSpawnPoint.spawnPosition, Quaternion.identity);
        EnemyDNABase enemyDNA = enemy.GetComponent<EnemyDNABase>();
        
        enemyDNA.spawnNode.NavigateEnemyToFinalSpawnPosition(randomSpawnPoint.finalPosition);
        enemies.Add(enemyDNA);
    }

    public void RemoveEnemy(EnemyDNABase enemy)
    {
        enemies.Remove(enemy);
    }

}