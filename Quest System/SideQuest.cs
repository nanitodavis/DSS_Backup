using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Side Quest", menuName = "Side Quest")]
public class SideQuest : ScriptableObject
{
    public BaseQuest basicQuestInformation;
    public QuestObjective actualObjective;
    public int actualObjectiveIndex;
    public QuestObjective[] listOfObjectives;
    public Reward[] questReward;

    public QuestObjective ReturnNextObjective()
    {
        return null;
    }

    public int ActualObjectiveIndexProperty
    {
        get
        {
            return actualObjectiveIndex;
        }
        set
        {
            actualObjectiveIndex = value;
            if (actualObjectiveIndex >= listOfObjectives.Length && listOfObjectives.Length > 0)
            {
                actualObjectiveIndex = listOfObjectives.Length - 1;
            }
            else if (actualObjectiveIndex < 0)
            {
                actualObjectiveIndex = 0;
            }
            ApplyActualObjective();
        }
    }
    public void ApplyActualObjective()
    {
        //check if the actual index is on a applayable objective
        if (listOfObjectives.Length > 0)
        {
            actualObjective = listOfObjectives[actualObjectiveIndex];
        }
        else
        {
            //if note, we have no objective
            actualObjective.questType = EnumsScript.Quest_Objective_Type.NONE;
        }
    }

    public void MoveToNextObjective()
    {
        //check if theres a next objective in the list
        if (actualObjectiveIndex < listOfObjectives.Length - 1 && !basicQuestInformation.questIsCompleted)
        {
            //if there is, then applied it as the actual objective
            actualObjectiveIndex++;
            ApplyActualObjective();
        }
    }

    public void CheckIfChapterIsCompleted()
    {
        for (int cont = 0; cont < listOfObjectives.Length; cont++)
        {
            if (!listOfObjectives[cont].objectiveCompleted)
            {
                return;
            }
            else if (cont == listOfObjectives.Length - 1 && listOfObjectives[cont].objectiveCompleted)
            {
                basicQuestInformation.questIsCompleted = true;
            }
        }
    }
}
