using UnityEngine;

public class ArcProjectileSystem : MonoBehaviour 
{
    public GameObject projectilePrefab; // Prefab to instantiate
    public int projectileCount = 10; // Number of projectiles to spawn
    public float spreadAngle = 360f; // Total angle of the spread (360 for a full circle)

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
            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0) * transform.rotation;

            // Spawn the projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation);

            // Ensure the projectile faces outward
            projectile.transform.forward = rotation * Vector3.forward;
        }
    }
}