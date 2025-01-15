using UnityEngine;

public class HealthNode : MonoBehaviour 
{
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;


    [Header("Shield")]
    public int maxShield;
    public int currentShield;



    private void Start() 
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        // take from shield first
        if (currentShield > 0)
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHealth += currentShield;
                currentShield = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }
    }
}