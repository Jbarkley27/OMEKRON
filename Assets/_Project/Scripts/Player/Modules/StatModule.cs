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
    public int speed = 5;

    // Dash
    public float dashCooldown = 5;
    public float dashSpeed = 10;

    // Shield
    public int maxShield = 5;
    public int currentShield = 5;


    

    // Getters
    public float GetDashCooldownTime()
    {
        return dashCooldown;
    }
}