using System.Collections.Generic;
using UnityEngine;

public class NormalBattle_Room : RoomBase 
{
    public Wave enemiesThisRoom;

    private void Start() 
    {
        ResetRoom();
    }

    public override void ResetRoom()
    {
        Debug.Log("Resetting Normal Battle Room");
    }

    public override void ActivateRoom()
    {
        Debug.Log("Activating Normal Battle Room");
        StartCoroutine(SpawnManager.instance.StartWave(enemiesThisRoom));
    }

    public override void CompleteRoom()
    {
        Debug.Log("Normal Battle Room completed");
    }


}