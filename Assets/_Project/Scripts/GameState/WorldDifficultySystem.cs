using UnityEngine;


public class WorldDifficultySystem : MonoBehaviour
{
    public static WorldDifficultySystem instance { get; private set; }


    [Header("World Difficulty")]
    public Difficulty currentDifficulty = Difficulty.BRONZE;
    public enum Difficulty { BRONZE, SILVER, GOLD };
    
    

    [Header("Bronze Difficulty")]
    public int spawnPointAddition = 0;
    public float enemyHealthAddition = 1f;



    [Header("Silver Difficulty")]
    public int spawnPointAdditionSilver = 10;
    public float enemyHealthAdditionSilver = 1.2f;



    [Header("Gold Difficulty")]
    public int spawnPointAdditionGold = 20;
    public float enemyHealthAdditionGold = 1.6f;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found a Screen Shake Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
