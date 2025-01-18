using System.Collections.Generic;
using UnityEngine;


public class RoomBase : MonoBehaviour 
{
    public enum RoomType
    {
        NormalBattle,
        Warden,
        Shop,
        Objective,
        Mystery,
        BossBattle,
    }

    public RoomType roomType;


    public virtual void ResetRoom()
    {
        // Debug.Log(roomType.ToString() + " Room reset");
    }

    public virtual void ActivateRoom()
    {

    }

    public virtual void CompleteRoom()
    {
        Debug.Log(roomType.ToString() + " Room completed");
    }
}