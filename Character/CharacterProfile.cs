using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterProfile : MonoBehaviour
{

    public BaseCharacterCharacteristic baseInformation;
    public RawStats equipmentStats;
    public RawStats battleStats;

    public float[] battleItemTime;
    public BaseItem[] battleItem;

    public int actualHealthPoints;
    public int actualSpiritualPoints;
    public bool isDead;
    public bool activePlayer;

    public Sprite smallInventoryNormalSprite;
    public Sprite smallInventoryStatusSprite;

    public EquipableItem headEquipment;
    public EquipableItem chestEquipment;
    public EquipableItem armEquipment;
    public EquipableItem mainWeaponEquipment;
    public EquipableItem offHandEquipment;
    public EquipableItem footEquipment;
    public EquipableItem accesoryOneEquipment;
    public EquipableItem accesoryTwoEquipment;

    public bool activeInParty;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public EquipableItem EquipItem(EquipableItem itemToEquip)
    {
        EquipableItem itemToReturn = null;
        if (itemToEquip.equipedOn == EnumsScript.EquipedOn.HEAD)
        {
            itemToReturn = headEquipment;
            headEquipment = itemToEquip;
        }
        else if (itemToEquip.equipedOn == EnumsScript.EquipedOn.CHEST)
        {
            itemToReturn = chestEquipment;
            chestEquipment = itemToEquip;
        }
        else if (itemToEquip.equipedOn == EnumsScript.EquipedOn.ARMS)
        {
            itemToReturn = armEquipment;
            armEquipment = itemToEquip;
        }
        else if (itemToEquip.equipedOn == EnumsScript.EquipedOn.MAIN_WEAPON)
        {
            itemToReturn = mainWeaponEquipment;
            mainWeaponEquipment = itemToEquip;
        }
        else if (itemToEquip.equipedOn == EnumsScript.EquipedOn.OFF_HAND)
        {
            itemToReturn = offHandEquipment;
            offHandEquipment = itemToEquip;
        }
        else if (itemToEquip.equipedOn == EnumsScript.EquipedOn.FOOT)
        {
            itemToReturn = footEquipment;
            footEquipment = itemToEquip;
        }
        else if (itemToEquip.equipedOn == EnumsScript.EquipedOn.ACCESSORY1)
        {
            itemToReturn = accesoryOneEquipment;
            accesoryOneEquipment = itemToEquip;
        }
        else if (itemToEquip.equipedOn == EnumsScript.EquipedOn.ACCESSORY2)
        {
            itemToReturn = accesoryTwoEquipment;
            accesoryTwoEquipment = itemToEquip;
        }

        return itemToReturn;
    }

    public void ConsumeItem(ConsumableItem itemToConsume)
    {
        bool flag = false;
        // if the item can cure a condition then cure it
        if (itemToConsume.canCureCondition && baseInformation.condition != EnumsScript.Condition.NONE)
        {
            for (int cont = 0; cont < itemToConsume.conditionsCured.Length; cont++)
            {
                if (itemToConsume.conditionsCured[cont]==baseInformation.condition)
                {
                    if(baseInformation.condition == EnumsScript.Condition.KO)
                    {
                        //To Check
                        baseInformation.condition = EnumsScript.Condition.NONE;
                        isDead = false;
                        flag = true;
                    }
                    else
                    {
                        baseInformation.condition = EnumsScript.Condition.NONE;
                        flag = true;
                    }
                }
            }
        }

        //HP
        if (actualHealthPoints < (baseInformation.healthPoints + equipmentStats.healthPoints + battleStats.healthPoints)&&itemToConsume.hpToRestore>0) {
            //healt points calculation
            if (itemToConsume.hpPercentage && !isDead)
            {
                actualHealthPoints += (int)((baseInformation.healthPoints + equipmentStats.healthPoints) * (float)(itemToConsume.hpToRestore / 100));
            }
            else if (!isDead)
            {
                actualHealthPoints += itemToConsume.hpToRestore;
            }
            flag = true;
        }

        //SP
        if (actualSpiritualPoints < (baseInformation.spiritualPoints + equipmentStats.spiritualPoints + battleStats.spiritualPoints) && itemToConsume.spToRestore > 0)
        {
            //spiritual points calculation
            if (itemToConsume.spPercentage && !isDead)
            {
                actualSpiritualPoints += (int)((baseInformation.spiritualPoints + equipmentStats.spiritualPoints) * (float)(itemToConsume.spToRestore / 100));
            }
            else if (!isDead)
            {
                actualSpiritualPoints += itemToConsume.spToRestore;
            }
            flag = true;
        }

        //if hp or sp surpass the max 
        if (actualHealthPoints > (baseInformation.healthPoints + equipmentStats.healthPoints + battleStats.healthPoints))
        {
            actualHealthPoints = baseInformation.healthPoints + equipmentStats.healthPoints + battleStats.healthPoints;
        }
        if (actualSpiritualPoints > (baseInformation.spiritualPoints + equipmentStats.spiritualPoints + battleStats.spiritualPoints))
        {
            actualSpiritualPoints = baseInformation.spiritualPoints + equipmentStats.spiritualPoints + battleStats.spiritualPoints;
        }
        if (flag)
        {
            itemToConsume.quantityHeld -= 1;
        }
    }

    public void ConsumeBattleItem(BattleItem itemToConsume)
    {
        //To-Do apply battle item effects
        itemToConsume.quantityHeld -= 1;
    }

    public void ClearBattleItemEffect()
    {
        //To-Do
    }

    public void ApplyItem(BaseItem itemToUse)
    {
        if (itemToUse.itemType == EnumsScript.ItemType.Consumable)
        {
            ConsumeItem((ConsumableItem)itemToUse);
        }
        else if (itemToUse.itemType == EnumsScript.ItemType.Equipable)
        {
            EquipItem((EquipableItem)itemToUse);
        }
        else if (itemToUse.itemType == EnumsScript.ItemType.Battle)
        {
            ConsumeBattleItem((BattleItem)itemToUse);
        }
    }
}