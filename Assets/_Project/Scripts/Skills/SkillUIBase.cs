using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SkillUIBase : MonoBehaviour 
{
    public Slider cooldownSlider;
    public Image skillIcon;
    public CanvasGroup canvasGroup;
    public SkillBase skill;
    public bool isDashSkill = false;

    private void Start() 
    {
        
    }

    private void Update() 
    {
        if(skill == null)
        {
            skillIcon.sprite = null;
            canvasGroup.alpha = .2f;
        }
    }


    // INITIALLY SETS UP THE UI
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





    // THIS IS FOR OTHER CLASSES TO CALL AS SOON AS THEY USE THE SKILL TO VISUALLY SHOW THE COOLDOWN
    public void UseSkill()
    {
        if (cooldownSlider)
        {
            cooldownSlider.value = 0;
            canvasGroup.alpha = 0.4f;
        }
        // play sound here
    }








    // COOLDOWN --------------------------------------------------------------------
    public void BeginCooldown(float cooldown)
    {
        StartCoroutine(Cooldown(cooldown));
    }




    // This Coroutine does the actual cooldown
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
        canvasGroup.alpha = 1;
        skill.EndCooldown();
    }

}