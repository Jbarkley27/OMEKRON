using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFlashSystem : MonoBehaviour
{
    public Material hitFlashMaterial_Base;
    public static MaterialFlashSystem instance;

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
}
