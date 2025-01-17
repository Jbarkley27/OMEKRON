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
        
        this.damage = damage;
        this.range = range;
        this.direction = direction;
        this.force = force;

        gameObject.transform.rotation = GlobalDataStore.instance.player.transform.rotation;
        gameObject.GetComponent<Rigidbody>().velocity = this.direction * force;
        isSetup = true;

        Destroy(gameObject, range);
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
