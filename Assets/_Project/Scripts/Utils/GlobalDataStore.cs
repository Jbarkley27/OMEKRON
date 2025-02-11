using UnityEngine;

public class GlobalDataStore : MonoBehaviour 
{
    public static GlobalDataStore instance { get; private set; }

    [Header("Player")]
    public Transform player;
    public Transform projectileSource;


    [Header("Parents")]
    public Transform skillParent;


    [Header("Managers")]
    public GameObject manager;

    [Header("Player Modules")]
    public StatModule statModule;
    public PlayerController playerController;
    public KitModule kitModule;
    public BlasterModule blasterModule;


    [Header("UI")]
    public DashUI dashUI;
    public KitUI kitUI;
    public BlasterUI blasterUI;
    

    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Global Data Store object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}