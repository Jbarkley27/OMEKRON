using UnityEngine;

public class DashUI: SkillUIBase
{
    private void Start() 
    {
        isDashSkill = true;
    }


    public override void SetSkillUsageToReady()
    {
        canvasGroup.alpha = 1;
        // play sound

        GlobalDataStore.instance.playerController.isDashing = false;
        Debug.Log("Dash Ready");
    }


    public override void SetupSkillUI(SkillBase assignedSkill)
    {
        base.SetupSkillUI(assignedSkill);
        GlobalDataStore.instance.playerController.isDashing = false;
        cooldownSlider.maxValue = GlobalDataStore.instance.playerController.dashCooldown;
        cooldownSlider.value = GlobalDataStore.instance.playerController.dashCooldown;
    }
}