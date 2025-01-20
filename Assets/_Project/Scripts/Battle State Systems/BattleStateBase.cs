using System.Collections;
using UnityEngine;

public class BattleStateBase : MonoBehaviour 
{
    public bool stateRunning = false;

    public virtual void EnterState()
    {
        Debug.Log("Entering Battle State");
    }

    public virtual void ExitState()
    {
        Debug.Log("Exiting Battle State");
    }

    public virtual void Update()
    {
        if (!stateRunning) return;
    }    

    // States that are more of transitional states will use this
    public virtual IEnumerator RunState()
    {
        yield return null;
    }
}