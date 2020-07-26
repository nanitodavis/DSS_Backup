[System.Serializable]
public class BaseQuest
{
    public int questId;
    public string questName;
    public string questDescription;
    public EnumsScript.Quest_Type questType;
    public bool questIsActive;
    public bool questIsCompleted;
}
