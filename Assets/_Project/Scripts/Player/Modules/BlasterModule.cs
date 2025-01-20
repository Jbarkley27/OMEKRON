using System.Collections;
using UnityEngine;

public class BlasterModule : MonoBehaviour {


    public BlasterBase equippedBlaster;

    private void Start() 
    {
        equippedBlaster = Instantiate(GlobalDataStore.instance.kitModule.equippedBlaster, gameObject.transform);
    }



    private void Update() 
    {
        // if(InputManager.instance.FiringBlaster)
        // {
        //     if (equippedBlaster.currentAmmo <= equippedBlaster.burstAmount * equippedBlaster.burstsPerShot
        //         || equippedBlaster.isFiring)
        //     {
        //         return;
        //     }


        //     StartCoroutine(ShootProjectileHelper(equippedBlaster, GlobalDataStore.instance.projectileSource, GlobalDataStore.instance.player.transform.forward));
        // }
    }



    public IEnumerator ShootProjectileHelper(BlasterBase blaster, Transform firePoint, Vector3 direction)
    {
        blaster.SetIsFiring(true);

        for (int i = 0; i < blaster.burstAmount; i++)
        {
            for (int j = 0; j < blaster.burstsPerShot; j++)
            {
                GameObject projectile = Instantiate(
                    blaster.projectile, 
                    firePoint.position,
                    GlobalDataStore.instance.player.transform.rotation);

                projectile.GetComponent<PlayerProjectile>().SetupProjectile(ApplySpread(direction, blaster.spread), blaster.projectileSpeed, blaster.damage, blaster.range);

                blaster.UseAmmo();

                yield return new WaitForSeconds(blaster.burstsPerShotFireRate);
            }

            yield return new WaitForSeconds(blaster.fireRate);
        }
        
        blaster.SetIsFiring(false);
    }


    public Vector3 ApplySpread(Vector3 direction, float spread)
    {
        Vector2 accuracy = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread));
        Vector3 directionWithSpread = direction + new Vector3(accuracy.y, 0, 0);

        return directionWithSpread;
    }
}