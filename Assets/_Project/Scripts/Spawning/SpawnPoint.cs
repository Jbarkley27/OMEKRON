using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour 
{
    public bool isOccupied;

    public void SpawnEnemy()
    {
        isOccupied = true;
    }

    public IEnumerator SpawnEnemyHelper()
    {
        isOccupied = true;

        // short delay so that the enemy don't spawn at the same time
        yield return new WaitForSeconds(3);


        isOccupied = false;
    }
}