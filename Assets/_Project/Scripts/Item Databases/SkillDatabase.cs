using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    public enum SkillID {FIREBALL, BLACKHOLE, ARCSTRIKE, QUAKE};

    public static SkillDatabase instance;

    // [Header("Skill Prefabs")]
    public Fireball_Skill fireballPrefab;
    public Blackhole_Skill blackholePrefab;
    public ArcStrike_Skill arcStrikePrefab;
    public Quake_Skill quakePrefab;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Skill Database object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public SkillBase GetSkillFromID(SkillID skillID)
    {
        SkillBase newSkill;

        switch (skillID)
        {
            case SkillID.FIREBALL:
                newSkill = Instantiate(fireballPrefab);
                break;
            case SkillID.BLACKHOLE:
                newSkill = Instantiate(blackholePrefab);
                break;
            case SkillID.ARCSTRIKE:
                newSkill = Instantiate(arcStrikePrefab);
                break;
            case SkillID.QUAKE:
                newSkill = Instantiate(quakePrefab);
                break;
            default:
                newSkill = null;
                break;
        }

        return newSkill;
    }
}