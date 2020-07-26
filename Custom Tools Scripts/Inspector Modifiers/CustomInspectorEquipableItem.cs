using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EquipableItem))]
[CanEditMultipleObjects]
public class CustomInspectorEquipableItem : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        EquipableItem equipableItem = (EquipableItem)target;

        EditorGUILayout.LabelField("Equipable Item Basic Information");
        EditorGUILayout.LabelField("\n");
        EditorGUI.indentLevel += 1;
        equipableItem.itemName = EditorGUILayout.TextField("Name", equipableItem.itemName);
        equipableItem.itemDescription = EditorGUILayout.TextField("Description", equipableItem.itemDescription);
        equipableItem.itemType = (EnumsScript.ItemType)EditorGUILayout.EnumPopup("Type", EnumsScript.ItemType.Equipable); 
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Sprite");
        equipableItem.mySprite = (Sprite)EditorGUILayout.ObjectField(equipableItem.mySprite, typeof(Sprite), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();
        //enable this line for the sprite to show in inspector
        //equipableItem.mySprite = (Sprite)EditorGUILayout.ObjectField("Sprite", equipableItem.mySprite, typeof(Sprite), allowSceneObjects: true);
        equipableItem.itemId = EditorGUILayout.IntField("Id", equipableItem.itemId);
        equipableItem.quantityHeld = EditorGUILayout.IntField("Quantity", equipableItem.quantityHeld);
        equipableItem.available = EditorGUILayout.Toggle("Available", equipableItem.available);
        equipableItem.canBeSelled = EditorGUILayout.Toggle("Can be selled", equipableItem.canBeSelled);
        equipableItem.canBeDiscarted = EditorGUILayout.Toggle("Can be Discarted", equipableItem.canBeDiscarted);
        equipableItem.price = EditorGUILayout.IntField("Price", equipableItem.price);
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.LabelField("\n");

        EditorGUILayout.LabelField("Equipable unique information");
        EditorGUI.indentLevel += 1;
        EditorGUILayout.LabelField("Equipable Characters");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("canEquipId"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("elementResist"));
        EditorGUILayout.LabelField("\n");
        equipableItem.equipedOn = (EnumsScript.EquipedOn)EditorGUILayout.EnumPopup("Equiped On", equipableItem.equipedOn);
        EditorGUILayout.LabelField("Stats affected");
        EditorGUI.indentLevel += 1;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("statsAffected").FindPropertyRelative("Array.size"));//turn visible the stats affected array size
        for(int cont = 0; cont < equipableItem.statsAffected.Length; cont++)
        {
            equipableItem.statsAffected[cont] = (EnumsScript.Stats)EditorGUILayout.EnumPopup("Stat "+(cont+1), equipableItem.statsAffected[cont]);
            EditorGUI.indentLevel += 1;
            if (equipableItem.statsAffected[cont]==EnumsScript.Stats.PHYSICAL_ATTACK)
            {
                equipableItem.attack = EditorGUILayout.IntField("Attack", equipableItem.attack);
            }
            else if (equipableItem.statsAffected[cont] == EnumsScript.Stats.PHYSICAL_DEFENCE)
            {
                equipableItem.defence = EditorGUILayout.IntField("Defence", equipableItem.defence);
            }
            else if (equipableItem.statsAffected[cont] == EnumsScript.Stats.SPIRITUAL_ATTACK)
            {
                equipableItem.spiritualAttack = EditorGUILayout.IntField("Spritual Attack", equipableItem.spiritualAttack);
            }
            else if (equipableItem.statsAffected[cont] == EnumsScript.Stats.SPIRITUAL_DEFENCE)
            {
                equipableItem.spiritualDefence = EditorGUILayout.IntField("Spritual Defence", equipableItem.spiritualDefence);
            }
            else if (equipableItem.statsAffected[cont] == EnumsScript.Stats.CRITIC)
            {
                equipableItem.critic = EditorGUILayout.IntField("Critic", equipableItem.critic);
            }
            else if (equipableItem.statsAffected[cont] == EnumsScript.Stats.HP)
            {
                equipableItem.hp = EditorGUILayout.IntField("Health Points", equipableItem.hp);
            }
            else if (equipableItem.statsAffected[cont] == EnumsScript.Stats.SP)
            {
                equipableItem.sp = EditorGUILayout.IntField("Spiritual Points", equipableItem.sp);
            }
            EditorGUI.indentLevel -= 1;
        }
        EditorGUI.indentLevel -= 1;
        //Apply changes made to serializedObjects, like lists
        serializedObject.ApplyModifiedProperties();
    }
}