using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;



/**
 * The Battle Manager is responsible for managing the progression of the player through the game.
 * It keeps track of the current radius(level, distance from Isol) the player is in and the rooms that the player has to go through.
 * It also keeps track of the current room the player is in and will handle activation of those rooms
 * The Battle Manager is a singleton class, so there should only be one instance of it in the game.
 *
 * A Room can be a normal battle, a warden, a shop, an objective, a mystery, or a boss battle.
 * Normal Battle Rooms have: Waves of enemies that the player has to defeat before moving on to the next wave.
 *
 *
 *
 */
public class RoomManager : MonoBehaviour 
{
    public enum Radius {ALPHA, BETA, GAMMA, OMEGA};
    public static RoomManager instance;
    public Radius currentRadius;
    public bool playerBeatBoss = false;

    [Header("Room Prefabs")]
    public List<RoomBase> omegaRoomsGO = new List<RoomBase>();
    public List<RoomBase> gammaRoomsGO = new List<RoomBase>();
    public List<RoomBase> betaRoomsGO = new List<RoomBase>();
    public List<RoomBase> alphaRoomsGO = new List<RoomBase>();

    [Header("Internal Lists")]
    public List<RoomBase> omegaRooms = new List<RoomBase>();
    public List<RoomBase> gammaRooms = new List<RoomBase>();
    public List<RoomBase> betaRooms = new List<RoomBase>();
    public List<RoomBase> alphaRooms = new List<RoomBase>();
    public int currentRoomIndex = -1;
    public bool waitForUserToStartNextRadius = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found a Battle Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);


        // instantiate all the rooms
        Debug.Log("Creating all the rooms...");
        omegaRooms.Clear();
        foreach (RoomBase room in omegaRoomsGO)
        {
            RoomBase newRoom = Instantiate(room, transform);
            omegaRooms.Add(newRoom);
        }

        gammaRooms.Clear();
        foreach (RoomBase room in gammaRoomsGO)
        {
            RoomBase newRoom = Instantiate(room, transform);
            gammaRooms.Add(newRoom);
        }

        betaRooms.Clear();
        foreach (RoomBase room in betaRoomsGO)
        {
            RoomBase newRoom = Instantiate(room, transform);
            betaRooms.Add(newRoom);
        }

        alphaRooms.Clear();
        foreach (RoomBase room in alphaRoomsGO)
        {
            RoomBase newRoom = Instantiate(room, transform);
            alphaRooms.Add(newRoom);
        }
    }

    public void Start()
    {
        // Start the first room for OMEGA radius
        ResetRoomManager();
    }

    public void SetupNextRadius()
    {
        switch (currentRadius)
        {
            case Radius.ALPHA:
                currentRadius = Radius.BETA;
                break;
            case Radius.BETA:
                currentRadius = Radius.GAMMA;
                break;
            case Radius.GAMMA:
                currentRadius = Radius.OMEGA;
                break;
            case Radius.OMEGA:
                playerBeatBoss = true;
                break;
        }
    }

    public List<RoomBase> GetRoomsForCurrentRadius()
    {
        switch (currentRadius)
        {
            case Radius.ALPHA:
                return alphaRooms;
            case Radius.BETA:
                return betaRooms;
            case Radius.GAMMA:
                return gammaRooms;
            case Radius.OMEGA:
                return omegaRooms;
            default:
                return null;
        }
    }

    public void ResetRoomManager()
    {
        // Reset Environment Manager
        Debug.Log("Resetting Room Manager");
        EnvironmentSystem.instance.ResetEnvironment();
        StartCoroutine(EnvironmentSystem.instance.SetupEnvironment());


        // Reset Battle Manager specific variables
        currentRadius = Radius.OMEGA;
        playerBeatBoss = false;
        currentRoomIndex = -1;

        // reset all rooms
        foreach (RoomBase room in alphaRooms)
        {
            room.ResetRoom();
        }
        foreach (RoomBase room in betaRooms)
        {
            room.ResetRoom();
        }
        foreach (RoomBase room in gammaRooms)
        {
            room.ResetRoom();
        }
        foreach (RoomBase room in omegaRooms)
        {
            room.ResetRoom();
        }

        // Start the first room for OMEGA radius
        ActiveNextRoom();
    }

    public bool IsEndOfRoomForCurrentRadius()
    {
        return currentRoomIndex >= GetRoomsForCurrentRadius().Count;
    }

    public void ActiveNextRoom()
    {

        if (IsEndOfRoomForCurrentRadius())
        {
            Debug.Log("End of current Radius " + currentRadius.ToString());
            StartCoroutine(TransitionToNextRadius());
            return;
        }


        CurrentRoomComplete();

        Debug.Log("Activating next room in radius: " + currentRadius.ToString());
        GetRoomsForCurrentRadius()[currentRoomIndex].ActivateRoom();
    }

    public void CurrentRoomComplete()
    {
        if(currentRoomIndex >= 0) Debug.Log("Current Room " + GetRoomsForCurrentRadius()[currentRoomIndex].roomType.ToString() + " completed");
        currentRoomIndex++;
    }

    public IEnumerator TransitionToNextRadius()
    {

        currentRoomIndex = -1;
        SetupNextRadius();

        if (playerBeatBoss)
        {
            Debug.Log("Player beat the boss, game over");
            yield break;
        }

        Debug.Log("Transitioning to next radius: " + currentRadius.ToString());
        Debug.Log("Waiting for user to start next radius...");

        // Wait for player input before starting the next radius
        yield return new WaitUntil(() => waitForUserToStartNextRadius);

        ActiveNextRoom();
    }
}