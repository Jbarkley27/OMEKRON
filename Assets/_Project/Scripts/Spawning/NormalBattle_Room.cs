using System.Collections.Generic;
using UnityEngine;

public class NormalBattle_Room : RoomBase 
{
    public List<Wave> waves = new List<Wave>();
    public int currentWave = 0;

    private void Start() 
    {
        ResetRoom();
    }

    private void Update() {
        if(waves[currentWave].ShouldStartNextWave())
        {
            CompleteCurrentWave();
        }
    }

    public override void ResetRoom()
    {
        currentWave = 0;
        Debug.Log("Resetting Normal Battle Room");
    }

    public override void ActivateRoom()
    {
        ResetRoom();
        Debug.Log("Activating Normal Battle Room");
        StartCoroutine(SpawnManager.instance.StartWave(waves[currentWave]));
    }

    public void CompleteCurrentWave()
    {
        if(currentWave >= waves.Count)
        {
            CompleteRoom();
        }
        else
        {
            SpawnManager.instance.StartWave(waves[currentWave]);
        }
    }

    public override void CompleteRoom()
    {
        Debug.Log("Normal Battle Room completed");
        RoomManager.instance.ActiveNextRoom();
    }


}