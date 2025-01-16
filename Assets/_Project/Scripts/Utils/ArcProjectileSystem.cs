using UnityEngine;

public class ArcProjectileSystem : MonoBehaviour 
{
    public GameObject projectilePrefab; // Prefab to instantiate
    public int projectileCount = 10; // Number of projectiles to spawn
    public float spreadAngle = 360f; // Total angle of the spread (360 for a full circle)
    public int range = 10; // Range of the projectile
    public float force = 10f; // Force to apply to the projectile
    public static ArcProjectileSystem instance;


    public struct ProjectileData
    {
        public GameObject projectilePrefab;
        public int projectileCount;
        public float spreadAngle;
        public float force;
        public int range;
        public int damage;
        public bool simpleMode;


        public ProjectileData(GameObject projectilePrefab, int projectileCount, float spreadAngle, float force, int range, int damage, bool simpleMode)
        {
            this.projectilePrefab = projectilePrefab;
            this.projectileCount = projectileCount;
            this.spreadAngle = spreadAngle;
            this.force = force;
            this.range = range;
            this.damage = damage;
            this.simpleMode = simpleMode;
        }
    }

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
    
    public void SpawnProjectiles(ProjectileData data)
    {
        if (projectilePrefab == null 
            || data.projectilePrefab == null
            || data.projectileCount <= 0)
        {
            Debug.LogWarning("Projectile Prefab is not assigned!");
            return;
        }

        // grab the 
        
        float angleStep = data.spreadAngle / Mathf.Max(data.projectileCount - 1, 1);
        float startingAngle = -spreadAngle / 2; // Center the arc

        for (int i = 0; i < data.projectileCount; i++)
        {
            // Calculate the rotation for each projectile
            float currentAngle = startingAngle + i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0) * GlobalDataStore.instance.player.rotation;

            // Spawn the projectile
            GameObject projectile = Instantiate(data.projectilePrefab, GlobalDataStore.instance.player.position, rotation);

            // Ensure the projectile faces outward
            projectile.transform.forward = rotation * Vector3.forward;

            // Use the rigidbody to apply force to the projectile to go straight
            PlayerProjectile rb = projectile.GetComponent<PlayerProjectile>();
            rb.SetupProjectile(projectile.transform.forward, data.force, data.damage, data.range, data.simpleMode);
        }
    }
}