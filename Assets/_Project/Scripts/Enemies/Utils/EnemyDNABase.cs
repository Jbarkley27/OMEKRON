using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;


public class EnemyDNABase : MonoBehaviour 
{
    public enum Grade
    {
        C, B, A, S
    }

    [Header("Enemy Stats")]
    public string enemyName;
    public string description;
    public Grade grade;
    public int maxHealth;
    public int currentHealth;
    public int maxShield;
    public int currentShield;
    public int damage;
    public int attackSpeed;
    public int power;

    [Header("Agent Settings")]
    public NavMeshAgent agent;
    public float moveSpeedMax;
    public float moveSpeedMin;

    public float attackRangeMax;
    public float attackRangeMin;

    public float accelerationMax;
    public float accelerationMin;

    public float sizeMax;
    public float sizeMin;

    public float rotationSpeedMax;
    public float rotationSpeedMin;
    public float currentRotationSpeed;

    [Header("Nodes")]
    public SpawnNode spawnNode;


    // These will be used to determine the enemy's stats based on their grade, which will determined by the current game level
    [Header("Enemy Multipliers")]
    public List<int> damageMultipliers = new(4);
    public List<int> healthMultipliers = new(4);
    public List<int> shieldMultipliers = new(4);
    public List<int> speedMultipliers = new(4);
    public List<int> attackSpeedMultipliers = new(4);


    public bool IsDead = false;



    private void Awake() {
        SetStats();
        spawnNode = GetComponent<SpawnNode>();
    }

    public bool HasShield()
    {
        return maxShield > 0;
    }

    public void SetStats()
    {
        // Set the stats based on the grade
        switch (grade)
        {
            case Grade.C:
                damage = damageMultipliers[0];
                maxHealth = healthMultipliers[0];
                maxShield = shieldMultipliers[0];
                attackSpeed = attackSpeedMultipliers[0];
                break;
            case Grade.B:
                damage = damageMultipliers[1];
                maxHealth = healthMultipliers[1];
                maxShield = shieldMultipliers[1];
                attackSpeed = attackSpeedMultipliers[1];
                break;
            case Grade.A:
                damage = damageMultipliers[2];
                maxHealth = healthMultipliers[2];
                maxShield = shieldMultipliers[2];
                attackSpeed = attackSpeedMultipliers[2];
                break;
            case Grade.S:
                damage = damageMultipliers[3];
                maxHealth = healthMultipliers[3];
                maxShield = shieldMultipliers[3];
                attackSpeed = attackSpeedMultipliers[3];
                break;
        }

        currentHealth = maxHealth;
        currentShield = maxShield;

        // Set the rotation
        currentRotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
    }

    public void Die()
    {
        if (IsDead)
        {
            return;
        }
        IsDead = true;
        StartCoroutine(ExecuteDeath());
    }

    public IEnumerator ExecuteDeath()
    {
        // Play death animation
        // Play death sound
        // Play death particles
        // Wait for a few seconds
        // Destroy the game object
        yield return new WaitForSeconds(.1f);
        SpawnManager.instance.enemies.Remove(this);
        Destroy(gameObject);
    }
    
}