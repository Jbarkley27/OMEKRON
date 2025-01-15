using UnityEngine;
using System.Collections;

public class ArcStrike_Skill: SkillBase
{
    public GameObject projectilePrefab;
    public int damage;
    public float lifetime;
    
    public override IEnumerator ExecuteSkill()
    {
        skillUI.UseSkill();
        ArcProjectileSystem.instance.SpawnProjectiles();
        yield return new WaitForSeconds(.2f);
        StartCooldown();
    }
}