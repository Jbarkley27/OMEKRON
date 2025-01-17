using UnityEngine;
using DamageNumbersPro;


// TODO: Come back and incorporate colors based on damage type
public class PopUpTextSystem : MonoBehaviour
{
    public static PopUpTextSystem instance;
    public DamageNumber numberPrefab;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an PopUpTextSystem object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreateDamagePopup(Transform sourceTransform, string text)
    {
        DamageNumber damageNumber = numberPrefab.Spawn(sourceTransform.position, text, sourceTransform);
    }
}
