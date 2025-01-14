using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitUI : MonoBehaviour
{
    [Header("Kit")]
    public GameObject kitUIRoot;


    public void TurnOnKitUI()
    {
        kitUIRoot.SetActive(!kitUIRoot.activeSelf);
    }
}
