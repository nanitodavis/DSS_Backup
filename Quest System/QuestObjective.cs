using System;
using UnityEngine;

[System.Serializable]
public class QuestObjective
{

    //Type of objective
    public EnumsScript.Quest_Objective_Type questType;

    //attributes for Monster Hunt quest type
    #region Hunt Monster Quest
    public int monsterIdToHunt;
    public int quantityToHunt;
    public int quantityHunted;
    #endregion

    //attributes for Talking to specific NPC quest type
    #region Talk To NPC Quest
    public int npcToTalkId;
    public bool talkedToNpc;
    #endregion

    //attributes for Collecting a certain ammount of items quest type
    #region Item Collection Quest
    public int itemToCollectId;
    public int quantityToCollect;
    public int quantityCollected;
    #endregion

    //attributes for killing a boss quest type
    #region Kill Boss quest type
    public int bossIdToDefeat;
    public bool isBossDefeated;
    #endregion

    //attributes por reaching a certain spot in the game quest type
    #region Reach Position quest type
    public float posAtX;
    public float posAtY;
    public float posAtZ;
    public Vector3 positionToGo;
    public string areaNameToGo;
    public float radius;
    public bool positionReached;
    #endregion

    #region Time Limit quest type
    public bool hasTimeLimit;//configure in inspector pending
    public float timeLimit;
    public float actualTime;
    #endregion

    //general attributes
    public bool objectiveCompleted;
    public bool canAchieveGoal = true;

}
