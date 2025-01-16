using UnityEngine;
using System.Collections;

public class ArcStrike_Skill: SkillBase
{
    public GameObject projectilePrefab;
    public int damage;
    public int range;
    public int numProjectiles;
    public float angle;
    public float force;

    
    public override IEnumerator ExecuteSkill()
    {
        skillUI.UseSkill();
        
        ArcProjectileSystem.instance.SpawnProjectiles(
            new ArcProjectileSystem.ProjectileData(
                projectilePrefab, 
                numProjectiles, 
                angle,
                force,
                range,
                damage,
                true
            )
        );

        yield return new WaitForSeconds(1);

        if (skillUI)
        {
            skillUI.BeginCooldown(CooldownTime);
        }
    }
}