using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class SkillUIBase : MonoBehaviour 
{
    public Slider cooldownSlider;
    public Image skillIcon;
    public CanvasGroup canvasGroup;
    public SkillBase skill;
    public bool isEmpty = false;
    public bool isDashSkill = false;

    private void Start() 
    {
        
    }

    private void Update() {

        if(isDashSkill) return;
        isEmpty = skill == null;

        if(isEmpty)
        {
            skillIcon.sprite = null;
            canvasGroup.alpha = .2f;
        }
    }

    public virtual void SetupSkillUI(SkillBase assignedSkill)
    {
        if(isDashSkill) return;
        skill = assignedSkill;
        skillIcon.sprite = assignedSkill.Icon;
        cooldownSlider.maxValue = assignedSkill.CooldownTime;
        cooldownSlider.value = assignedSkill.CooldownTime;
        canvasGroup.alpha = 1;
        skill.skillUI = this;
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

    private IEnumerator Cooldown(float cooldownTime = -1)
    {
        // this is in the cases where we need the player to force a quicker cooldown
        if(cooldownTime == -1)
        {
            cooldownTime = 1f;
        }

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
        if(skill != null)
        {
            canvasGroup.alpha = 1;
            skill.EndCooldown();
        }
    }

}