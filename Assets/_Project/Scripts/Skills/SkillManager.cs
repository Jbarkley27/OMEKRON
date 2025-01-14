using UnityEngine;

public class SkillManager : MonoBehaviour 
{
    public static SkillManager instance { get; private set; }
    public enum SkillType {Scorch, Corruption, Jolt, Void};
    public bool SkillInUse = false;


    [Header("Equipped Skills")]
    public Skill skill_1;
    public Skill skill_2;
    public Skill skill_3;

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

    public bool CanUseSkill()
    {
        return !SkillInUse;
    }
    
    public void UseSkill(int index)
    {
        if (SkillInUse)
            return;

        switch (index)
        {
            case 1:
                skill_1.UseSkill();
                break;
            case 2:
                skill_2.UseSkill();
                break;
            case 3:
                skill_3.UseSkill();
                break;
        }
    }
}