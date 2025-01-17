using UnityEngine;

public class HealthNode : MonoBehaviour 
{
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;


    [Header("Shield")]
    public int maxShield;
    public int currentShield;


    public Flashable flashable;



    private void Start() 
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        // make the enemy flash white
        flashable.Flash(MaterialFlashSystem.instance.hitFlashMaterial_Base, .1f);

        // show the damage number popup
        PopUpTextSystem.instance.CreateDamagePopup(transform, damage.ToString());

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