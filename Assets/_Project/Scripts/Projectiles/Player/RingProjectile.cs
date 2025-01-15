using UnityEngine;
using DG.Tweening;

public class RingProjectile: MonoBehaviour
{
    public int damage;
    public float lifetime;
    public bool isSetup = false;
    public float spinRate;


    public void SetupProjectile(int damage, float lifetime)
    {
        this.damage = damage;
        this.lifetime = lifetime;

        isSetup = true;

        Invoke("DestroyProjectile", lifetime);
        gameObject.transform.DOScale(new Vector3(
            gameObject.transform.localScale.x * 4,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z * 4
        ), lifetime).SetEase(Ease.Linear);
    }

    public void DestroyProjectile()
    {
        // do some stuff
        Destroy(gameObject);
    }

    private void Update() {
        if (gameObject == null || !isSetup) return;
        
    }
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "enemy-visual")
        {
            Debug.Log("Hit enemy");
        }
    }

}