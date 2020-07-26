[System.Serializable]
public class BaseSkill
{
    public int skillId;
    public string skillName;
    public string skillDescription;
    public EnumsScript.Skill_Type type;
    public int[] skillDependace;
    public int levelRequired;
    public bool canBeLEarned;
    public int consumedSkillPoints;
}
