using UnityEngine;

public class DashUI: SkillUIBase
{
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
    }
}