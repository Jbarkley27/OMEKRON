using UnityEngine;

public class ArcProjectileSystem : MonoBehaviour 
{
    public GameObject projectilePrefab; // Prefab to instantiate
    public int projectileCount = 10; // Number of projectiles to spawn
    public float spreadAngle = 360f; // Total angle of the spread (360 for a full circle)
    public int range = 10; // Range of the projectile
    public float force = 10f; // Force to apply to the projectile
    public static ArcProjectileSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Skill Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void SpawnProjectiles()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("Projectile Prefab is not assigned!");
            return;
        }
        
        float angleStep = spreadAngle / Mathf.Max(projectileCount - 1, 1);
        float startingAngle = -spreadAngle / 2; // Center the arc

        for (int i = 0; i < projectileCount; i++)
        {
            // Calculate the rotation for each projectile
            float currentAngle = startingAngle + i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0) * GlobalDataStore.instance.player.rotation;

            // Spawn the projectile
            GameObject projectile = Instantiate(projectilePrefab, GlobalDataStore.instance.player.position, rotation);

            // Ensure the projectile faces outward
            projectile.transform.forward = rotation * Vector3.forward;

            // Use the rigidbody to apply force to the projectile to go straight
            PlayerProjectile rb = projectile.GetComponent<PlayerProjectile>();
            rb.SetupProjectile(projectile.transform.forward, force, range, 10, true);
        }
    }
}