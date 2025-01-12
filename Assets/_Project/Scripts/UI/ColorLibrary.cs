using UnityEngine;

public class ColorLibrary: MonoBehaviour
{
    public static ColorLibrary instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Color Library object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // HELPERS -----------------------------------------------------------------
    public void ChangeImageColor(GameObject image, ColorCodes colorCode)
    {
        if (image.GetComponent<UnityEngine.UI.Image>() == null)
        {
            Debug.LogError("No Image component found on " + image.name);
            return;
        }

        image.GetComponent<UnityEngine.UI.Image>().color = GetColor(colorCode);
    }

    public Color GetColor(ColorCodes colorCode)
    {
        switch (colorCode)
        {
            case ColorCodes.SCORCH_PRIMARY_UI:
                return SCORCH_PRIMARY_UI;
            case ColorCodes.CORRUPTION_PRIMARY_UI:
                return CORRUPTION_PRIMARY_UI;
            case ColorCodes.JOLT_PRIMARY_UI:
                return JOLT_PRIMARY_UI;
            case ColorCodes.VOID_PRIMARY_UI:
                return VOID_PRIMARY_UI;
            default:
                return Color.white;
        }
    }

    // Rarity ---------------------------------------------------------------------------
    public enum RarityColorCodes {COMMON, RARE, EXOTIC};

    public Color GetRarityColor(RarityColorCodes colorCode)
    {
        switch (colorCode)
        {
            case RarityColorCodes.COMMON:
                return COMMON;
            case RarityColorCodes.RARE:
                return RARE;
            case RarityColorCodes.EXOTIC:
                return EXOTIC;
            default:
                return Color.white;
        }
    }

    public Color COMMON;
    public Color RARE;
    public Color EXOTIC;

    // Skill | Augment  -----------------------------------------------------------------
    public enum ColorCodes {SCORCH_PRIMARY_UI, CORRUPTION_PRIMARY_UI, JOLT_PRIMARY_UI, VOID_PRIMARY_UI}

    public Color GetSkillAugmentColor(ColorCodes colorCode)
    {
        switch (colorCode)
        {
            case ColorCodes.SCORCH_PRIMARY_UI:
                return SCORCH_PRIMARY_UI;
            case ColorCodes.CORRUPTION_PRIMARY_UI:
                return CORRUPTION_PRIMARY_UI;
            case ColorCodes.JOLT_PRIMARY_UI:
                return JOLT_PRIMARY_UI;
            case ColorCodes.VOID_PRIMARY_UI:
                return VOID_PRIMARY_UI;
            default:
                return Color.white;
        }
    }

    public Color SCORCH_PRIMARY_UI;
    public Color CORRUPTION_PRIMARY_UI;
    public Color JOLT_PRIMARY_UI;
    public Color VOID_PRIMARY_UI;

}