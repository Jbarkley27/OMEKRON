using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    public List<EnemyFormationPosition> formationPositions = new List<EnemyFormationPosition>();
    public EnemyFormationPosition frontPosition;
    public EnemyFormationPosition backPosition;
    public static EnemyFormation instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Spawn Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public EnemyFormationPosition GetNextAvailablePosition()
    {
        ShuffleFormation();
        foreach (var position in formationPositions)
        {
            if (!position.GetOccupied())
            {
                return position;
            }
        }

        return null;
    }

    public void ShuffleFormation()
    {
        for (int i = 0; i < formationPositions.Count; i++)
        {
            var temp = formationPositions[i];
            int randomIndex = Random.Range(i, formationPositions.Count);
            formationPositions[i] = formationPositions[randomIndex];
            formationPositions[randomIndex] = temp;
        }
    }
}
