using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// WE NEEDED TO CREATE A NEW CLASS FOR THE DASH SKILL BECAUSE IT HAS A DIFFERENT UI FUNCTIONALITY FROM THE OTHER SKILLS
public class DashUI: MonoBehaviour
{

    public Slider cooldownSlider;
    public Image skillIcon;
    public CanvasGroup canvasGroup;

    private void Start() 
    {
      SetSkillUsageToReady();
    }


    public void SetSkillUsageToReady()
    {
        canvasGroup.alpha = 1;
        
        
        cooldownSlider.maxValue = GlobalDataStore.instance.statModule.dashCooldown;
        cooldownSlider.value = GlobalDataStore.instance.statModule.dashCooldown;

        GlobalDataStore.instance.playerController.isDashing = false;
        Debug.Log("Dash Ready");
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
}