using UnityEngine;

public class EnemyFormationPosition: MonoBehaviour
{
    public bool isOccupied = false;
    public Vector3 spawnPosition;
    public Vector3 finalPosition;

    private void Start() {
        spawnPosition = transform.GetChild(0).position;
        finalPosition = transform.position;
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }

    public bool GetOccupied()
    {
        return isOccupied;
    }


}