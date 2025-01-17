using UnityEngine;


public class WorldDifficultySystem : MonoBehaviour
{
    public static WorldDifficultySystem instance { get; private set; }


    [Header("World Difficulty")]
    public int currentLevel;

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
