using UnityEngine;
using DG.Tweening;

public class SpawnNode : MonoBehaviour
{
    public void NavigateEnemyToFinalSpawnPosition(Vector3 finalPosition)
    {
        // Move the enemy to the final position
        transform.DOMove(finalPosition, Random.Range(.8f, 1.5f)).SetEase(Ease.Linear);
    }
}