using UnityEngine;

public class EnemyLibrary : MonoBehaviour 
{
    public GameObject nucleotideEnemyPrefab;
    public GameObject virusEnemyPrefab;
    public GameObject bacteriaEnemyPrefab;

    public static EnemyLibrary instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Enemy Library object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public GameObject GetEnemyPrefabFromID(SpawnManager.EnemyID enemyID)
    {
        switch (enemyID)
        {
            case SpawnManager.EnemyID.Nucleotide:
                return nucleotideEnemyPrefab;
            case SpawnManager.EnemyID.Virus:
                return virusEnemyPrefab;
            case SpawnManager.EnemyID.Bacteria:
                return bacteriaEnemyPrefab;
            default:
                return null;
        }
    }   
}