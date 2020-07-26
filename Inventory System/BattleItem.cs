using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Battle Item", menuName = "Battle Item")]
public class BattleItem : BaseItem
{
    public EnumsScript.Stats[] statsAffected; //the diference with these and a normal consumable item is that these are temporary effects
    public EnumsScript.Condition[] conditionsCured;
    public int hp;
    public int sp;
    public int attack;
    public int defence;
    public int spiritualAttack;
    public int SpiritualDefence;
    public int critic;
}
