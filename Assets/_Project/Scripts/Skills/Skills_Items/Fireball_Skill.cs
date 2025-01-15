using System.Collections;
using UnityEngine;


/**
 * Cannon skill that shoots a projectile
 */
public class Fireball_Skill : SkillBase
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public int damage;
    public float range;


    public override IEnumerator ExecuteSkill()
    {
        // show in UI skill was used
        skillUI.UseSkill();
        GameObject projectile = Instantiate(
            projectilePrefab, 
            GlobalDataStore.instance.projectileSource.position,
            GlobalDataStore.instance.player.transform.rotation);

        projectile.GetComponent<PlayerProjectile>().SetupProjectile(GlobalDataStore.instance.player.transform.forward, projectileSpeed, damage, range);

        yield return new WaitForSeconds(.2f);

        StartCooldown();
    }
}