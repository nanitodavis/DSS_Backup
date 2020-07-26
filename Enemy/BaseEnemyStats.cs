[System.Serializable]
public class BaseEnemyStats
{

    //General Attributes
    public string name;
    public string desc;
    public int level;
    public EnumsScript.Element affinityElement;
    public int totalGrantedExperience;

    //Stats
    public int healthPoints;
    public int spiritualPoints;
    public int physicalAttack;

    public int physicalDefense;
    public int spiritualAttack;
    public int spiritualDefense;
    public float critic;

    //Special attributes
    public int superArmorHits;

}
