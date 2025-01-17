using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthShieldUI : MonoBehaviour
{
    [Header("Health")]
    public TMP_Text healthText;
    public Slider healthSlider;


    [Header("Shield")]
    public TMP_Text shieldText;
    public Slider shieldSlider;



    
    void Update()
    {
        healthSlider.maxValue = GlobalDataStore.instance.statModule.maxHealth;
        healthSlider.value = GlobalDataStore.instance.statModule.currentHealth;
        healthText.text = GlobalDataStore.instance.statModule.currentHealth + " / " + GlobalDataStore.instance.statModule.maxHealth;

        shieldSlider.maxValue = GlobalDataStore.instance.statModule.maxShield;
        shieldSlider.value = GlobalDataStore.instance.statModule.currentShield;
        shieldText.text = GlobalDataStore.instance.statModule.currentShield + " / " + GlobalDataStore.instance.statModule.maxShield;
    }
}
