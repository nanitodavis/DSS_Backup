using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterProfile))]
public class CustomInspectorCharacter : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //sample code for making a button
        if(GUILayout.Button("Test Button"))
        {

        }

        //if the target of the inspector has a component of type TestCharacter, then the code is applied
        if (target.GetType() == typeof(CharacterProfile))
        {
            //creates a TestCharacter named "basechar" and assign the actual target of same type to it using a cast
            CharacterProfile basechar = (CharacterProfile)target;
            //takes the values assigned to the character stats and pass then throught the setters method
            basechar.baseInformation.HealthPointsProperty = basechar.baseInformation.healthPoints;
            basechar.baseInformation.SpiritualPointsProperty = basechar.baseInformation.spiritualPoints;
            basechar.baseInformation.PhysicalAttackProperty = basechar.baseInformation.physicalAttack;
            basechar.baseInformation.PhysicalDefenseProperty = basechar.baseInformation.physicalDefense;
            basechar.baseInformation.SpiritualAttackProperty = basechar.baseInformation.spiritualAttack;
            basechar.baseInformation.SpiritualDefenseProperty = basechar.baseInformation.spiritualDefense;
            basechar.baseInformation.CriticProperty = basechar.baseInformation.critic;
        }
    }
}
