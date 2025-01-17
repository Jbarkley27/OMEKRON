using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthNode : MonoBehaviour 
{
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public Slider healthSlider;


    [Header("Shield")]
    public int maxShield;
    public int currentShield;
    public Slider shieldSlider;


    [Header("Utils")]
    public Flashable flashable;
    public EnemyDNABase enemyDNABase;




    private void Start() 
    {
       // Shield
       if(enemyDNABase.HasShield())
       {
            maxShield = enemyDNABase.maxShield;
            currentShield = maxShield;
            shieldSlider.maxValue = maxShield;
            shieldSlider.value = currentShield;
        }
        else
        {
            Debug.Log("Enemy does not have a shield");
            shieldSlider.gameObject.SetActive(false);
        }

        // Health
        maxHealth = enemyDNABase.maxHealth;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }


    private void Update() 
    {
        healthSlider.value = currentHealth;
        shieldSlider.value = currentShield;

        if(currentHealth <= 0)
        {
            enemyDNABase.Die();
        }
    }


    public void TakeDamage(int damage)
    {
        // make the enemy flash white
        flashable.Flash(MaterialFlashSystem.instance.hitFlashMaterial_Base, .1f);

        // show the damage number popup
        PopUpTextSystem.instance.CreateDamagePopup(transform, damage.ToString());

        // take from shield first
        if (currentShield > 0 && enemyDNABase.HasShield())
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