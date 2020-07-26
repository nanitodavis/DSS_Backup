using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestScript : MonoBehaviour
{
    //script attributes
    public MainQuestChapter[] chapterList;
    public MainQuestChapter actualChapter;
    public int actualChapterIndex;
    public bool mainStoryFinished;

    private void Start()
    {
        if (actualChapter == null)
            ApplyActualChapter();
    }

    //Get and Set method for the chapter index property
    public int ActualChapterIndexProperty
    {
        get
        {
            return actualChapterIndex;
        }
        set
        {
            actualChapterIndex = value;
            if (actualChapterIndex >= chapterList.Length && chapterList.Length > 0)
            {
                actualChapterIndex = chapterList.Length - 1;
            }
            else if (actualChapterIndex < 0)
            {
                actualChapterIndex = 0;
            }
            ApplyActualChapter();
        }
    }

    public void ApplyActualChapter()
    {
        if (chapterList.Length > 0)
        {
            actualChapter = chapterList[actualChapterIndex];
        }
    }

    // Called for activating the next objective in the chapter
    public void MoveToNextObjective()
    {
        if (!actualChapter.chapterCompleted)
        {
            if (actualChapter.actualObjective.objectiveCompleted)
            {
                actualChapter.MoveToNextObjective();
            }
        }
        else
        {
            NextChapter();
        }
    }

    //Move to next chapter, if all chapters are completed, the main story is completed
    public void NextChapter()
    {
        if (actualChapter.chapterCompleted)
        {
            actualChapterIndex++;
            if (actualChapterIndex < chapterList.Length)
            {
                actualChapter = chapterList[actualChapterIndex];
            }
            else
            {
                mainStoryFinished = true;
            }
        }
    }

    //Check information to see if the quest is completed
    public void AttempToCompleteActualObjective(EnumsScript.Quest_Objective_Type questType, int id, int quantity, string areaName, Vector3 position)
    {
        if (!actualChapter.actualObjective.objectiveCompleted)
        {
            switch (questType)
            {
                case EnumsScript.Quest_Objective_Type.HUNT_MONSTER:
                    if (actualChapter.actualObjective.monsterIdToHunt == id)
                        actualChapter.actualObjective.quantityHunted += quantity;
                    if (actualChapter.actualObjective.quantityToHunt == actualChapter.actualObjective.quantityHunted)
                    {
                        actualChapter.actualObjective.objectiveCompleted = true;
                    }
                    break;

                case EnumsScript.Quest_Objective_Type.COLLECT_ITEM:
                    if (actualChapter.actualObjective.itemToCollectId == id)
                        actualChapter.actualObjective.quantityCollected+= quantity;
                    if(actualChapter.actualObjective.quantityToCollect == actualChapter.actualObjective.quantityCollected)
                        actualChapter.actualObjective.objectiveCompleted = true;
                    break;

                case EnumsScript.Quest_Objective_Type.KILL_BOSS:
                    if (actualChapter.actualObjective.bossIdToDefeat == id)
                    {
                        actualChapter.actualObjective.isBossDefeated = true;
                        actualChapter.actualObjective.objectiveCompleted = true;
                    }
                    break;

                case EnumsScript.Quest_Objective_Type.REACH_PLACE:
                    //fix position check variable with the radius
                    if (Vector3.Distance(actualChapter.actualObjective.positionToGo, position) < actualChapter.actualObjective.radius && actualChapter.actualObjective.areaNameToGo == areaName)
                    {
                        actualChapter.actualObjective.positionReached = true;
                        actualChapter.actualObjective.objectiveCompleted = true;
                    }
                    break;

                case EnumsScript.Quest_Objective_Type.TALK_TO_NPC:
                    if (actualChapter.actualObjective.npcToTalkId == id)
                    {
                        actualChapter.actualObjective.talkedToNpc = true;
                        actualChapter.actualObjective.objectiveCompleted = true;
                    }
                    break;

                case EnumsScript.Quest_Objective_Type.NONE:
                    break;

            }
        }
    }

    //Check if position is correct
    public void AttempToCompleteActualObjective(EnumsScript.Quest_Objective_Type questType, Vector3 position, string areaName)
    {
        if (!actualChapter.actualObjective.objectiveCompleted && questType == EnumsScript.Quest_Objective_Type.REACH_PLACE)
        {
            if (Vector3.Distance(actualChapter.actualObjective.positionToGo, position) < actualChapter.actualObjective.radius && actualChapter.actualObjective.areaNameToGo == areaName)
            {
                actualChapter.actualObjective.positionReached = true;
                actualChapter.actualObjective.objectiveCompleted = true;
            }
        }
    }

    public void AttempToCompleteActualObjective(EnumsScript.Quest_Objective_Type questType, int id, int quantity)
    {
        if (!actualChapter.actualObjective.objectiveCompleted)
        {
            switch (questType)
            {
                case EnumsScript.Quest_Objective_Type.HUNT_MONSTER:
                    if (actualChapter.actualObjective.monsterIdToHunt == id)
                        actualChapter.actualObjective.quantityHunted += quantity;
                    if (actualChapter.actualObjective.quantityToHunt == actualChapter.actualObjective.quantityHunted)
                    {
                        actualChapter.actualObjective.objectiveCompleted = true;
                    }
                    break;

                case EnumsScript.Quest_Objective_Type.COLLECT_ITEM:
                    if (actualChapter.actualObjective.itemToCollectId == id)
                        actualChapter.actualObjective.quantityCollected += quantity;
                    if (actualChapter.actualObjective.quantityToCollect == actualChapter.actualObjective.quantityCollected)
                        actualChapter.actualObjective.objectiveCompleted = true;
                    break;

                case EnumsScript.Quest_Objective_Type.KILL_BOSS:
                    if (actualChapter.actualObjective.bossIdToDefeat == id)
                    {
                        actualChapter.actualObjective.isBossDefeated = true;
                        actualChapter.actualObjective.objectiveCompleted = true;
                    }
                    break;

                case EnumsScript.Quest_Objective_Type.TALK_TO_NPC:
                    if (actualChapter.actualObjective.npcToTalkId == id)
                    {
                        actualChapter.actualObjective.talkedToNpc = true;
                        actualChapter.actualObjective.objectiveCompleted = true;
                    }
                    break;

                case EnumsScript.Quest_Objective_Type.NONE:
                    break;

            }
        }
    }

    public bool CheckActualObjective()
    {
        return actualChapter.actualObjective.objectiveCompleted;
    }
}
