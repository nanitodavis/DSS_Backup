using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipable Item", menuName = "Equipable Item")]
public class EquipableItem : BaseItem
{
    public EnumsScript.EquipedOn equipedOn;
    public int[] canEquipId;
    public EnumsScript.Stats[] statsAffected;
    public EnumsScript.Element[] elementResist;
    public int timesEquiped;
    public int hp { get; set; }
    public int sp { get; set; }
    public int attack { get; set; }
    public int defence { get; set; }
    public int spiritualAttack { get; set; }
    public int spiritualDefence { get; set; }
    public int critic { get; set; }
}
