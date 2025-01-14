using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillUIBase : MonoBehaviour 
{
    public Slider cooldownSlider;
    public Image skillIcon;
    public CanvasGroup canvasGroup;
    public bool isCooldownSkill = true;


    private void Start() {
        canvasGroup.alpha = 1;
    }

    public void SetupSkillUI(Sprite icon)
    {
        skillIcon.sprite = icon;
    }

    public void UseSkill()
    {
        if (cooldownSlider)
        {
            cooldownSlider.value = 0;
            canvasGroup.alpha = 0.4f;
        }
        // play sound here
    }

    public void BeginCooldown(float cooldown)
    {
        StartCoroutine(Cooldown(cooldown));
    }

    private IEnumerator Cooldown(float cooldownTime)
    {
        float elapsedTime = 0f;
        float startValue = cooldownSlider.value; // Current value of the slider
        float targetValue = cooldownSlider.maxValue; // Max value of the slider

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time
            cooldownSlider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / cooldownTime); // Smoothly interpolate the slider value
            yield return null; // Wait until the next frame
        }

        // Ensure the slider is completely filled at the end
        cooldownSlider.value = targetValue;

        // Set the skill usage to ready
        SetSkillUsageToReady();
    }

    public virtual void SetSkillUsageToReady()
    {
        
    }

}