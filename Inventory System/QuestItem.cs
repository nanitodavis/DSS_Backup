using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Item", menuName = "Quest Item")]
public class QuestItem : BaseItem
{
    public string questDescription;
    public bool delivered;
    public BaseQuest questReference;
}
