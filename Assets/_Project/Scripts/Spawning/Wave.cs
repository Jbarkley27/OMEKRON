using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "New Wave")]
public class Wave : ScriptableObject 
{
    public List<SpawnManager.EnemyID> enemies = new List<SpawnManager.EnemyID>();  
    public float timeToThisWave = 5f;

    [Tooltip("The power of the current wave, used to determine what is the lowest power before we start to spawn the next wave.")]
    public int nextWavePowerThreshold = 10;
    public int index;

    private void Start() {
        index = 0;
    }

    private bool IsWaveOver()
    {
        return index >= enemies.Count;
    }



    public bool ShouldStartNextWave()
    {
        if(SpawnManager.instance.GetTotalCurrentPower() >= nextWavePowerThreshold)
        {
            return true;
        }

        return false;
    }
}