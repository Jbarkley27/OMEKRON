using System.Collections.Generic;
using UnityEngine;

public class DOTProjectile : MonoBehaviour
{
    public int damage;
    public float lifetime;
    public float dotRate;
    public bool isSetup = false;
    public float spinRate;
    public List<HealthNode> enemiesHit = new List<HealthNode>();


    public void SetupProjectile(int damage, float lifetime, float dotRate)
    {
        this.damage = damage;
        this.lifetime = lifetime;
        this.dotRate = dotRate;

        isSetup = true;

        Invoke("DestroyProjectile", lifetime);
        InvokeRepeating("ApplyDamage", .1f, dotRate);
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void Update() {
        if (!isSetup) return;
        transform.Rotate(Vector3.up, spinRate * Time.deltaTime);
    }


    public void ApplyDamage()
    {
        foreach (HealthNode enemy in enemiesHit)
        {
            if (enemy == null) continue;
            enemy.TakeDamage(damage);
        }
    }
    







    // COLLISION EVENTS
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "enemy-visual")
        {
            HealthNode enemy = collider.gameObject.transform.parent.GetComponent<HealthNode>();
            if (enemy != null && !enemiesHit.Contains(enemy))
            {
                enemiesHit.Add(enemy);
            }
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "enemy-visual")
        {
            HealthNode enemy = collider.gameObject.transform.parent.GetComponent<HealthNode>();
            if (enemy != null && enemiesHit.Contains(enemy))
            {
                enemiesHit.Remove(enemy);
            }
        }
    }


    
}
