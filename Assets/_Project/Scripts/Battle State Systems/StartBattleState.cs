using UnityEngine;
using System.Collections;

public class StartBattleState : BattleStateBase
{
    
    public override void EnterState()
    {
        if (stateRunning)return;

        stateRunning = true;
        Debug.Log("Battle Starting");
        StartCoroutine(RunState());
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Start Battle State");
    }

    public override IEnumerator RunState()
    {
        Debug.Log("Running Start Battle State");
        yield return new WaitForSeconds(2f);
        Debug.Log("Draw Cards");
        Debug.Log("Battle Started");
        // BattleStateManager.instance.ChangeState(BattleStateManager.BattleState.START);
    }

}