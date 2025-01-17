using System.Collections;
using UnityEngine;

public class BlasterBase : MonoBehaviour 
{
    public string blasterName;
    [Tooltip("Time between each round")]
    public float fireRate;
    [Tooltip("How many rounds are in a burst (4 Round Burst)")]
    public int burstAmount;
    [Tooltip("How many projectiles are in each round")]
    public int burstsPerShot;
    public float spread;
    public float projectileSpeed;
    [Tooltip("How long between each projectile")]
    public float burstsPerShotFireRate;
    public float range;
    public int damage;
    public GameObject projectile;


    [Header("Ammo")]
    public int maxAmmo;
    public int currentAmmo;
    public float rechargeRate;



    [Header("State")]
    public bool isFiring = false;
    public bool isRecharging = false;
    public Coroutine cooldownCoroutine;


    private void Start() {
        currentAmmo = maxAmmo;
    }



    public void Update()
    {
        if (CanCooldown())
        {
            cooldownCoroutine = StartCoroutine(Cooldown());
        }
    }


    public void SetIsFiring(bool value)
    {
        isFiring = value;
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
            cooldownCoroutine = null;
        }
    }


    public void RechargeAmmo()
    {
        currentAmmo++;
        Mathf.Clamp(currentAmmo, 0, maxAmmo);
    }


    public void UseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
        }
    }


    public bool CanCooldown() {return !isFiring && cooldownCoroutine == null;}


    public IEnumerator Cooldown()
    {
        // small delay before we start recharging
        yield return new WaitForSeconds(1f);

        isRecharging = true;

        // start recharging ammo
        for(int i = currentAmmo; i < maxAmmo; i++)
        {
            // check if were firing each time
            if (isFiring)
            {
                isRecharging = false;
                yield break;
            }

            RechargeAmmo();
            yield return new WaitForSeconds(rechargeRate);
        }

        isRecharging = false;
    }
}