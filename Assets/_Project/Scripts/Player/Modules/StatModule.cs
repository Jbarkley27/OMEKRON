using UnityEngine;

public class StatModule : MonoBehaviour 
{
    public enum StatType {HEALTH, DAMAGE, SKILL_COOLDOWN, SPEED, DASH_COOLDOWN};

    [Header("Player Stats")]

    // Health
    public int maxHealth = 100;
    public int currentHealth;

    // Skill Cooldown
    public int skillCooldown = 5;

    // Speed
    public int speed = 1;

    // Dash
    public float dashCooldown = 5;
    public float dashSpeed = 10;

    // Shield
    public int maxShield = 5;
    public int currentShield = 5;

    private void Start() 
    {
        // currentHealth = maxHealth;
        // currentShield = maxShield;
    }


    // Dash -----------------------------------------------------------------------------
    public float GetDashCooldownTime()
    {
        return dashCooldown;
    }


    // Speed -----------------------------------------------------------------------------
    public int GetSpeed()
    {
        return speed;
    }


    // Health -----------------------------------------------------------------------------
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