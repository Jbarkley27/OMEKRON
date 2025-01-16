using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int damage;
    public float range;
    public Vector3 direction;
    public float force;
    public bool isSetup = false;

    public void SetupProjectile(Vector3 direction, float force, int damage, float range, bool simpleMode = false)
    {
        if(simpleMode)
        {
            gameObject.GetComponent<Rigidbody>().velocity = direction * force;
            this.damage = damage;
            this.range = range;
            isSetup = true;
            Destroy(gameObject, range);
            return;
        }

        // this is to add more complexity to the projectile if needed
        this.damage = damage;
        this.range = range;
        this.direction = direction;
        this.force = force;

        gameObject.transform.rotation = GlobalDataStore.instance.player.transform.rotation;
        gameObject.GetComponent<Rigidbody>().velocity = this.direction * force;
        isSetup = true;

        Destroy(gameObject, range);
    }

    private void Update() {
        if (gameObject == null || !isSetup) return;
    }
    


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "enemy-visual")
        {
            Debug.Log("Hit enemy");
            Destroy(gameObject);
        }
    }


}
