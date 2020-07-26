using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code for creating this scriptable object the editor
[CreateAssetMenu(fileName ="New Consumable Item", menuName = "Consumable Item")]
public class ConsumableItem : BaseItem
{
    //public BaseItem baseItemInfo; //all the basic information of an item
    public bool canCureCondition;
    public EnumsScript.Condition[] conditionsCured;
    public bool hpPercentage;
    public bool spPercentage;
    public int hpToRestore;
    public int spToRestore;
}
