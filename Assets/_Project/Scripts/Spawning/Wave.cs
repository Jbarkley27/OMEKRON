using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "New Wave")]
public class Wave : ScriptableObject 
{
    public List<SpawnManager.EnemyID> enemies = new List<SpawnManager.EnemyID>();  
    public int index;

    private void Start() {
        index = 0;
    }

    private bool IsWaveOver()
    {
        return index >= enemies.Count;
    }

}