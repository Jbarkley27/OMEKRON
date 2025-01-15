using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class BlasterUI: MonoBehaviour
{
    public TMP_Text ammoText;
    public Slider ammoSlider;


    public void Update()
    {
        ammoText.text = GlobalDataStore.instance.blasterModule.equippedBlaster.currentAmmo.ToString();
        ammoSlider.maxValue = GlobalDataStore.instance.blasterModule.equippedBlaster.maxAmmo;
        ammoSlider.value = GlobalDataStore.instance.blasterModule.equippedBlaster.currentAmmo;
    }


}