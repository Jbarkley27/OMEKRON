using UnityEngine;

public class SkillManager : MonoBehaviour 
{
    public static SkillManager instance { get; private set; }
    public enum SkillType {Scorch, Corruption, Jolt, Void};
    public bool SkillInUse = false;


    [Header("Skill UI")]
    public SkillUIBase skillUI_1;
    public SkillUIBase skillUI_2;
    public SkillUIBase skillUI_3;


    [Header("Equipped Skills")]
    public SkillBase skill_1;
    public SkillBase skill_2;
    public SkillBase skill_3;



    [Header("Starting Skills")]
    public SkillDatabase.SkillID startingSkill_1;
    public SkillDatabase.SkillID startingSkill_2;
    public SkillDatabase.SkillID startingSkill_3;


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


        // instantiate each skill
        skill_1 = SkillDatabase.instance.GetSkillFromID(startingSkill_1);
        skill_2 = SkillDatabase.instance.GetSkillFromID(startingSkill_2);
        skill_3 = SkillDatabase.instance.GetSkillFromID(startingSkill_3);

        // set the skill UI
        skillUI_1.SetupSkillUI(skill_1);
        skillUI_2.SetupSkillUI(skill_2);
        skillUI_3.SetupSkillUI(skill_3);
    }

    public void Start()
    {
        // instantiate each skill
        // skill_1 = SkillDatabase.instance.GetSkillFromID(startingSkill_1);
        // skill_2 = SkillDatabase.instance.GetSkillFromID(startingSkill_2);
        // skill_3 = SkillDatabase.instance.GetSkillFromID(startingSkill_3);

        // // set the skill UI
        // skillUI_1.SetupSkillUI(skill_1);
        // skillUI_2.SetupSkillUI(skill_2);
        // skillUI_3.SetupSkillUI(skill_3);
    }

    public bool CanUseSkill()
    {
        return !SkillInUse;
    }

    public void SkillComplete()
    {
        SkillInUse = false;
    }   
    
    public void UseSkill(int index)
    {
        if (SkillInUse)
            return;


        // make sure the skill is not null
        if (index == 1 && skill_1 == null) return;
        if (index == 2 && skill_2 == null) return;
        if (index == 3 && skill_3 == null) return;


        // make sure the skill is not on cooldown
        if (index == 1 && !skill_1.CanUse) return;
        if (index == 2 && !skill_2.CanUse) return;
        if (index == 3 && !skill_3.CanUse) return;


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

        SkillInUse = true;
    }
}