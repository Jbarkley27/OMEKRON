using UnityEngine;

public class StatModule : MonoBehaviour 
{
    public enum StatType {HEALTH, DAMAGE, SKILL_COOLDOWN, SPEED, DASH_COOLDOWN};

    [Header("Player Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int skillCooldown = 5;
    public int speed = 5;
    public int dashCooldown = 5;
    public int maxShieldCharges = 5;
    public int currentShieldCharges;
}