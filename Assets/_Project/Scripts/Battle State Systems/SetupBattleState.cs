using UnityEngine;
using System.Collections;

public class SetupBattleState : BattleStateBase 
{

    public override void EnterState()
    {
        if (stateRunning)return;

        stateRunning = true;
        Debug.Log("Entering Setup Battle State");  
        StartCoroutine(RunState()); 
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Setup Battle State");
    }

    public override IEnumerator RunState()
    {
        Debug.Log("Running Setup Battle State");
        Debug.Log("Spawning Enemies");
        yield return new WaitForSeconds(2f);
        BattleStateManager.instance.ChangeState(BattleStateManager.BattleState.START);
    }
}