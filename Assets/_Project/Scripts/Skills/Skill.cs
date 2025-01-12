using System.Collections;
using UnityEngine;

public class Skill: MonoBehaviour
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public float CooldownTime;
    public SkillManager.SkillType skillType;
    public bool CanUse;




    public virtual void UseSkill()
    {
        if (!CanUse) return;
        SkillManager.instance.SkillInUse = true;
    }

    public virtual IEnumerator ExecuteSkill()
    {
        // Play error sound
        // Do something with the UI
        yield return new WaitForSeconds(1);
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