using System.Collections;
using UnityEngine;


public class Blackhole_Skill : SkillBase
{
    public GameObject projectilePrefab;
    public int damage;
    public float dotRate;
    public float lifetime;


    public override IEnumerator ExecuteSkill()
    {
        // show in UI skill was used
        skillUI.UseSkill();
        GameObject projectile = Instantiate(
            projectilePrefab, 
            AimManager.instance.GetWorldCursorPosition(),
            GlobalDataStore.instance.player.transform.rotation);

        projectile.GetComponent<DOTProjectile>().SetupProjectile(damage, lifetime, dotRate);

        yield return new WaitForSeconds(.2f);

        StartCooldown();
    }
}