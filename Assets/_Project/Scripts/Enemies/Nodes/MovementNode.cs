using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MovementNode : MonoBehaviour 
{
    public EnemyDNABase enemy;
    public Rigidbody rb;

    private void Awake() 
    {
        enemy = GetComponent<EnemyDNABase>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
 
    }

    private void FixedUpdate() 
    {
        RotateTowards(GlobalDataStore.instance.player.position - transform.position);
    }
    
    private void Update() {

    }




     // UTIL FUNCTIONS --------------------------------------------
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