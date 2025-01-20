using UnityEngine;

public class BattleStateManager : MonoBehaviour 
{
    public enum BattleState { SETUP, START, PLAYERTURN, PLAYERENDTURN, ENEMYTURN, ENEMYENDTURN, CONCLUSION};
    public BattleState currentState;
    public static BattleStateManager instance;

    [Header("Battle States")]
    public SetupBattleState setupBattleState;
    public StartBattleState startBattleState;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found a Screen Shake Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start() {
        currentState = BattleState.SETUP;
    }

    private void Update() {
        switch (currentState)
        {
            case BattleState.SETUP:
                // Setup the battle
                setupBattleState.EnterState();
                break;
            case BattleState.START:
                // Start the battle
                startBattleState.EnterState();
                break;
            case BattleState.PLAYERTURN:
                // Player's turn
                break;
            case BattleState.PLAYERENDTURN:
                // Player's turn ended
                break;
            case BattleState.ENEMYTURN:
                // Enemy's turn
                break;
            case BattleState.ENEMYENDTURN:
                // Enemy's turn ended
                break;
            case BattleState.CONCLUSION:
                // Battle concluded
                break;
            default:
                break;
        }
    }

    public void ChangeState(BattleState newState)
    {
        switch (currentState)
        {
            case BattleState.SETUP:
                setupBattleState.ExitState();
                break;
            case BattleState.START:
                startBattleState.ExitState();
                break;
            case BattleState.PLAYERTURN:
                break;
            case BattleState.PLAYERENDTURN:
                break;
            case BattleState.ENEMYTURN:
                break;
            case BattleState.ENEMYENDTURN:
                break;
            case BattleState.CONCLUSION:
                break;
            default:
                break;
        }

        currentState = newState;
    }
}