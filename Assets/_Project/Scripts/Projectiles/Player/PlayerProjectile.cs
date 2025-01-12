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

    public void SetupProjectile(Vector3 direction, float force, int damage, float range)
    {
        this.damage = damage;
        this.range = range;
        this.direction = GlobalDataStore.instance.player.transform.position - direction;
        this.force = force;

        gameObject.transform.rotation = GlobalDataStore.instance.player.transform.rotation;

        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * force;

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
