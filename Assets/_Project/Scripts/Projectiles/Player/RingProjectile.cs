using UnityEngine;
using DG.Tweening;

public class RingProjectile: MonoBehaviour
{
    public int damage;
    public float lifetime;
    public bool isSetup = false;


    public void SetupProjectile(int damage, float lifetime)
    {
        this.damage = damage;
        this.lifetime = lifetime;


        isSetup = true;


        gameObject.transform.DOScale(new Vector3(
            gameObject.transform.localScale.x * 20f,
            gameObject.transform.localScale.y * 1.2f,
            gameObject.transform.localScale.z * 20f
        ), lifetime)
        .SetEase(Ease.Linear)
        .OnComplete(() => Destroy(gameObject));
    }
    




    // COLLISION EVENTS ----------------------------------------------
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "enemy-visual")
        {
            Debug.Log("Hit enemy");
        }
    }

}