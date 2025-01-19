using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MovementNode : MonoBehaviour 
{
    public NavMeshAgent mNavMeshAgent;
    public EnemyDNABase enemy;
    public Rigidbody rb;
    public bool CalculatingPath = false;
    [Range(0, 5f)] public float calculationSpeed = .5f;

    public bool InRange = false;

    private void Awake() 
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<EnemyDNABase>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        SetupNavMeshAgent();
    }

    private void FixedUpdate() 
    {
        RotateTowards(GlobalDataStore.instance.player.position - transform.position);
    }
    
    private void Update() {
        StartCalculatingPath();
        InRange = IsWithinRange();
    }


    public void SetupNavMeshAgent()
    {
        mNavMeshAgent.speed = Random.Range(enemy.moveSpeedMin, enemy.moveSpeedMax);
        mNavMeshAgent.acceleration = Random.Range(enemy.accelerationMin, enemy.accelerationMax);
        mNavMeshAgent.stoppingDistance = Random.Range(enemy.attackRangeMin, enemy.attackRangeMax);
        mNavMeshAgent.radius = Random.Range(enemy.sizeMin, enemy.sizeMax);
        mNavMeshAgent.angularSpeed = Random.Range(enemy.rotationSpeedMin, enemy.rotationSpeedMax);
    }
    


    // PATH CALCULATION FUNCTIONS --------------------------------------------
    public void StartCalculatingPath()
    {
        if (CalculatingPath)
        {
            return;
        }

        StartCoroutine(CalculatePath());
    }

    public IEnumerator CalculatePath()
    {
        CalculatingPath = true;
        yield return new WaitForSeconds(calculationSpeed);

        // Get the player's position
        Vector3 playerPos = GlobalDataStore.instance.player.position;

        // begin calculating the path
        if (!IsWithinRange())
        {
            // Calculate the direction from the enemy to the player
            // Vector3 direction = (playerPos - transform.position).normalized;

            // Set the new target position for the agent to move towards
            mNavMeshAgent.SetDestination(playerPos);

            // Set the stopping distance to 0 so that the enemy can move to the player
            // currentStoppingDistance = 0f;
        }
        else
        {
            // If the player is within range (less than moveDistance), move directly to the player
            // mNavMeshAgent.SetDestination(playerPos);

            Debug.Log("Attack");

            // Set the stopping distance to the stopping distance so that the enemy will stop moving towards the player
            // currentStoppingDistance = stoppingDistance;
        }

        CalculatingPath = false;

    }




     // UTIL FUNCTIONS --------------------------------------------

    public bool IsWithinRange()
    {
        return Vector3.Distance(transform.position, GlobalDataStore.instance.player.position) < mNavMeshAgent.stoppingDistance;
    }

    public void RotateTowards(Vector3 targetDirection)
    {

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Smoothly interpolate between current and target rotation
        Quaternion smoothedRotation = Quaternion.Slerp(
            rb.rotation,             // Current rotation
            targetRotation,          // Target rotation
            enemy.currentRotationSpeed * Time.deltaTime // Interpolation factor
        );

        // Apply the smooth rotation to the Rigidbody
        rb.MoveRotation(smoothedRotation);
    }
}