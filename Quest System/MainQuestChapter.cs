using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Main Quest Chapter", menuName = "Main Quest Chapter")]
public class MainQuestChapter : ScriptableObject
{
    public int chapterId;
    public string chapterName;
    public string chapterDescription;
    public string chapterSypnosis;
    public QuestObjective actualObjective;
    public int actualObjectiveIndex;
    public QuestObjective[] listOfObjectives;
    public bool chapterIsActive;
    public bool chapterCompleted;

    //if available, return the next objective in the list, if not, returns null
    public QuestObjective ReturnNextObjective()
    {
        //check if there's another objective available after the actual objective
        if (listOfObjectives.Length-1 > actualObjectiveIndex)
        {
            //if the next objective exist, it returns it
            return listOfObjectives[actualObjectiveIndex + 1];
        }
        //if not, returns null
        return null;
    }

    //Get And Set property for the actualObjectiveIndex attribute
    public int ActualObjectiveIndexProperty
    {
        get
        {
            return actualObjectiveIndex;
        }
        set
        {
            actualObjectiveIndex = value;
            if (actualObjectiveIndex >= listOfObjectives.Length&&listOfObjectives.Length>0)
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
        if (actualObjectiveIndex < listOfObjectives.Length - 1&&!chapterCompleted)
        {
            //if there is, then applied it as the actual objective
            actualObjectiveIndex++;
            ApplyActualObjective();
        }
    }

    public void CheckIfChapterIsCompleted()
    {
        for(int cont = 0; cont < listOfObjectives.Length; cont++)
        {
            if (!listOfObjectives[cont].objectiveCompleted)
            {
                return;
            }else if (cont == listOfObjectives.Length - 1 && listOfObjectives[cont].objectiveCompleted)
            {
                chapterCompleted = true;
            }
        }
    }
}