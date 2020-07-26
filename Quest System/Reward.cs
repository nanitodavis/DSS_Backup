using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    public EnumsScript.Reward_Type rewardType;
    public ConsumableItem item;
    public EquipableItem equipment;
    public int goldReward;
    public int experienceReward;
    public bool claimed;
}
