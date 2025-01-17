using UnityEngine;
using System.Collections;

public class Quake_Skill : SkillBase
{
    public GameObject quakePrefab;
    public float lifetime;
    public int damage;


    public override IEnumerator ExecuteSkill()
    {
        skillUI.UseSkill();
        SpawnQuake();
        yield return new WaitForSeconds(1);
        StartCooldown();
    }

    public void SpawnQuake()
    {
        GameObject quake = Instantiate(quakePrefab, GlobalDataStore.instance.player.transform.position, Quaternion.identity);
        quake.GetComponent<RingProjectile>().SetupProjectile(damage, lifetime);

        if (skillUI)
        {
            skillUI.BeginCooldown(CooldownTime);
        }
    }
}