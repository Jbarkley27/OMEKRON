using System.Collections;
using UnityEngine;

/**
 * REMINDER
 * Skills need to call the correct functions to represent the skill in the UI
 */
public class SkillBase: MonoBehaviour
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public float CooldownTime;
    public SkillManager.SkillType skillType;
    public bool CanUse;
    public SkillDatabase.SkillID skillID;
    public SkillUIBase skillUI;


    public virtual void UseSkill()
    {
        if (!CanUse) return;
        SkillManager.instance.SkillInUse = true;
        StartCoroutine(ExecuteSkill());
    }

    public virtual IEnumerator ExecuteSkill()
    {
        // Play error sound
        // Do something with the UI
        yield return new WaitForSeconds(.1f);
    }

    public virtual void StartCooldown()
    {
        CanUse = false;
        Invoke("EndCooldown", CooldownTime);
    }

    public virtual void EndCooldown()
    {
        CanUse = true;
    }



}