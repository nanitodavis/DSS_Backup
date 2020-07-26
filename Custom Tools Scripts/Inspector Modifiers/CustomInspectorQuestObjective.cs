using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MainQuestChapter))]
[CanEditMultipleObjects]
public class CustomInspectorQuestObjective : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        MainQuestChapter qo = (MainQuestChapter)target;

        //basic quest chapter information
        EditorGUILayout.LabelField("Chapter Basic Attributes");
        qo.chapterId = EditorGUILayout.IntField("Chapter ID", qo.chapterId);
        qo.chapterName = EditorGUILayout.TextField("Chapter Name", qo.chapterName);
        qo.chapterDescription = EditorGUILayout.TextField("Chapter Description", qo.chapterDescription);
        qo.chapterSypnosis = EditorGUILayout.TextField("Chapter Sypnosis", qo.chapterSypnosis);


        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("Actual Objective Type requires the list to be populated");
        if(serializedObject.FindProperty("listOfObjectives").arraySize <= 0)
        {
            qo.actualObjective.questType = (EnumsScript.Quest_Objective_Type)EditorGUILayout.EnumPopup("Actual Objective Type", EnumsScript.Quest_Objective_Type.NONE);
        }
        else
        {
            qo.actualObjective.questType = (EnumsScript.Quest_Objective_Type)EditorGUILayout.EnumPopup("Actual Objective Type", qo.actualObjective.questType);
        }

        //only shows the properties needed depending on the quest objective type
        EditorGUI.indentLevel += 1;
        if (qo.actualObjective.questType == EnumsScript.Quest_Objective_Type.COLLECT_ITEM)
        {
            qo.actualObjective.itemToCollectId = EditorGUILayout.IntField("Item ID", qo.actualObjective.itemToCollectId);
            qo.actualObjective.quantityToCollect = EditorGUILayout.IntField("Quantity Required", qo.actualObjective.quantityToCollect);
            qo.actualObjective.hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.actualObjective.hasTimeLimit);
        }
        else if (qo.actualObjective.questType == EnumsScript.Quest_Objective_Type.HUNT_MONSTER)
        {
            qo.actualObjective.monsterIdToHunt = EditorGUILayout.IntField("Monster ID", qo.actualObjective.monsterIdToHunt);
            qo.actualObjective.quantityToHunt = EditorGUILayout.IntField("Quantity To Hunt", qo.actualObjective.quantityToHunt);
            qo.actualObjective.hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.actualObjective.hasTimeLimit);
        }
        else if (qo.actualObjective.questType == EnumsScript.Quest_Objective_Type.KILL_BOSS)
        {
            qo.actualObjective.bossIdToDefeat = EditorGUILayout.IntField("Boss ID", qo.actualObjective.bossIdToDefeat);
            qo.actualObjective.hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.actualObjective.hasTimeLimit);
        }
        else if (qo.actualObjective.questType == EnumsScript.Quest_Objective_Type.REACH_PLACE)
        {
            qo.actualObjective.positionToGo = EditorGUILayout.Vector3Field("Destination", qo.actualObjective.positionToGo);
            qo.actualObjective.radius = EditorGUILayout.FloatField("Detection Area", qo.actualObjective.radius);
            qo.actualObjective.areaNameToGo = EditorGUILayout.TextField("Area Name", qo.actualObjective.areaNameToGo);
            qo.actualObjective.hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.actualObjective.hasTimeLimit);
        }
        else if (qo.actualObjective.questType == EnumsScript.Quest_Objective_Type.TALK_TO_NPC)
        {
            qo.actualObjective.npcToTalkId = EditorGUILayout.IntField("NPC ID", qo.actualObjective.npcToTalkId);
            qo.actualObjective.hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.actualObjective.hasTimeLimit);
        }

        if (qo.actualObjective.hasTimeLimit)
        {
            qo.actualObjective.timeLimit = EditorGUILayout.FloatField("Time Limit", qo.actualObjective.timeLimit);
        }
        EditorGUI.indentLevel -= 1;

        //list of objectives represented in editor
        EditorGUILayout.LabelField("\n");
        if (serializedObject.FindProperty("listOfObjectives").arraySize>0) {
            qo.ActualObjectiveIndexProperty = EditorGUILayout.IntField("Objective List Index", qo.actualObjectiveIndex);
        }

        //EditorGUILayout.PropertyField(serializedObject.FindProperty("listOfObjectives"));
        EditorGUILayout.LabelField("List Of Objectives");
        //for showing the size of the list so it can be modified
        if (serializedObject.FindProperty("listOfObjectives").isExpanded) {
            EditorGUI.indentLevel += 1;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("listOfObjectives").FindPropertyRelative("Array.size"));
            //Apply changes made to serializedObjects, like our list above
            serializedObject.ApplyModifiedProperties();
            //for modifying the list of objectives according to their respective types
            EditorGUI.indentLevel += 1;
            if (qo.listOfObjectives != null)
                for (int cont = 0; cont < qo.listOfObjectives.Length; cont++)
                {
                    qo.listOfObjectives[cont].questType = (EnumsScript.Quest_Objective_Type)EditorGUILayout.EnumPopup("Objective Type", qo.listOfObjectives[cont].questType);
                    EditorGUI.indentLevel += 1;
                    if (qo.listOfObjectives[cont].questType == EnumsScript.Quest_Objective_Type.COLLECT_ITEM)
                    {
                        qo.listOfObjectives[cont].itemToCollectId = EditorGUILayout.IntField("Item ID", qo.listOfObjectives[cont].itemToCollectId);
                        qo.listOfObjectives[cont].quantityToCollect = EditorGUILayout.IntField("Quantity Required", qo.listOfObjectives[cont].quantityToCollect);
                        qo.listOfObjectives[cont].hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.listOfObjectives[cont].hasTimeLimit);
                    }
                    else if (qo.listOfObjectives[cont].questType == EnumsScript.Quest_Objective_Type.HUNT_MONSTER)
                    {
                        qo.listOfObjectives[cont].monsterIdToHunt = EditorGUILayout.IntField("Monster ID", qo.listOfObjectives[cont].monsterIdToHunt);
                        qo.listOfObjectives[cont].quantityToHunt = EditorGUILayout.IntField("Quantity To Hunt", qo.listOfObjectives[cont].quantityToHunt);
                        qo.listOfObjectives[cont].hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.listOfObjectives[cont].hasTimeLimit);
                    }
                    else if (qo.listOfObjectives[cont].questType == EnumsScript.Quest_Objective_Type.KILL_BOSS)
                    {
                        qo.listOfObjectives[cont].bossIdToDefeat = EditorGUILayout.IntField("Boss ID", qo.listOfObjectives[cont].bossIdToDefeat);
                        qo.listOfObjectives[cont].hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.listOfObjectives[cont].hasTimeLimit);
                    }
                    else if (qo.listOfObjectives[cont].questType == EnumsScript.Quest_Objective_Type.REACH_PLACE)
                    {
                        qo.listOfObjectives[cont].positionToGo = EditorGUILayout.Vector3Field("Destination", qo.listOfObjectives[cont].positionToGo);
                        qo.actualObjective.radius = EditorGUILayout.FloatField("Detection Area", qo.actualObjective.radius);
                        qo.actualObjective.areaNameToGo = EditorGUILayout.TextField("Area Name", qo.actualObjective.areaNameToGo);
                        qo.listOfObjectives[cont].hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.listOfObjectives[cont].hasTimeLimit);
                    }
                    else if (qo.listOfObjectives[cont].questType == EnumsScript.Quest_Objective_Type.TALK_TO_NPC)
                    {
                        qo.listOfObjectives[cont].npcToTalkId = EditorGUILayout.IntField("NPC ID", qo.listOfObjectives[cont].npcToTalkId);
                        qo.listOfObjectives[cont].hasTimeLimit = EditorGUILayout.Toggle("Has time limit", qo.listOfObjectives[cont].hasTimeLimit);
                    }

                    if (qo.listOfObjectives[cont].hasTimeLimit)
                    {
                        qo.listOfObjectives[cont].timeLimit = EditorGUILayout.FloatField("Time Limit", qo.listOfObjectives[cont].timeLimit);
                    }
                    EditorGUI.indentLevel -= 1;
                }
            EditorGUI.indentLevel -= 2;
        }
    }
}