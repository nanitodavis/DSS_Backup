using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool canViewInventory;

    //item lists
    public List<BattleItem> battleItems;
    public List<ConsumableItem> consumableItems;
    public List<EquipableItem> equipableItems;
    public List<KeyItem> keyItems;
    public List<QuestItem> questItems;
    private List<ScriptableObject> unknownItems;
    private List<BaseItem> allItems;

    public int gold;

    // Update is called once per frame
    void Start()
    {
        allItems = GetAllItems();
    }

    //methods for adding specific types of items to the inventory
    public void StoreItemInInventory(ConsumableItem item)
    {
        for(int cont = 0; cont < consumableItems.Count; cont++)
        {
            if (item.itemId == consumableItems[cont].itemId)
            {
                consumableItems[cont].quantityHeld += item.quantityHeld;
            }else if(cont==consumableItems.Count-1&&item.itemId != consumableItems[cont].itemId)
            {
                consumableItems.Add(item);
            }
        }
        allItems.Add(item);
    }
    public void StoreItemInInventory(EquipableItem item)
    {
        for (int cont = 0; cont < equipableItems.Count; cont++)
        {
            if (item.itemId == equipableItems[cont].itemId)
            {
                equipableItems[cont].quantityHeld += item.quantityHeld;
            }
            else if (cont == equipableItems.Count - 1 && item.itemId != equipableItems[cont].itemId)
            {
                equipableItems.Add(item);
            }
        }
        allItems.Add(item);
    }
    public void StoreItemInInventory(BattleItem item)
    {
        for (int cont = 0; cont < battleItems.Count; cont++)
        {
            if (item.itemId == battleItems[cont].itemId)
            {
                battleItems[cont].quantityHeld += item.quantityHeld;
            }
            else if (cont == battleItems.Count - 1 && item.itemId != battleItems[cont].itemId)
            {
                battleItems.Add(item);
            }
        }
        allItems.Add(item);
    }

    // method for adding any type of item to the inventory
    public void StoreItemInInventory(BaseItem itemToAdd)
    {
        if (itemToAdd.GetType() == typeof(ConsumableItem))
        {
            ConsumableItem item = (ConsumableItem)itemToAdd;
            for (int cont = 0; cont < consumableItems.Count; cont++)
            {
                if (item.itemId == consumableItems[cont].itemId)
                {
                    consumableItems[cont].quantityHeld += item.quantityHeld;
                }
                else if (cont == consumableItems.Count - 1 && item.itemId != consumableItems[cont].itemId)
                {
                    consumableItems.Add(item);
                }
            }
        }
        else if (itemToAdd.GetType() == typeof(EquipableItem))
        {
            EquipableItem item = (EquipableItem)itemToAdd;
            for (int cont = 0; cont < equipableItems.Count; cont++)
            {
                if (item.itemId == equipableItems[cont].itemId)
                {
                    equipableItems[cont].quantityHeld += item.quantityHeld;
                }
                else if (cont == equipableItems.Count - 1 && item.itemId != equipableItems[cont].itemId)
                {
                    equipableItems.Add(item);
                }
            }
        }
        else if (itemToAdd.GetType() == typeof(BattleItem))
        {
            BattleItem item = (BattleItem)itemToAdd;
            for (int cont = 0; cont < battleItems.Count; cont++)
            {
                if (item.itemId == battleItems[cont].itemId)
                {
                    battleItems[cont].quantityHeld += item.quantityHeld;
                }
                else if (cont == battleItems.Count - 1 && item.itemId != battleItems[cont].itemId)
                {
                    battleItems.Add(item);
                }
            }
        }
        else if (itemToAdd.GetType() == typeof(KeyItem))
        {
            KeyItem item = (KeyItem)itemToAdd;
            for (int cont = 0; cont < keyItems.Count; cont++)
            {
                if (item.itemId == keyItems[cont].itemId)
                {
                    keyItems[cont].quantityHeld += item.quantityHeld;
                }
                else if (cont == keyItems.Count - 1 && item.itemId != keyItems[cont].itemId)
                {
                    keyItems.Add(item);
                }
            }
        }
        else if (itemToAdd.GetType() == typeof(QuestItem))
        {
            QuestItem item = (QuestItem)itemToAdd;
            for (int cont = 0; cont < questItems.Count; cont++)
            {
                if (item.itemId == questItems[cont].itemId)
                {
                    questItems[cont].quantityHeld += item.quantityHeld;
                }
                else if (cont == questItems.Count - 1 && item.itemId != questItems[cont].itemId)
                {
                    questItems.Add(item);
                }
            }
        }
        else
        {
            for (int cont = 0; cont < unknownItems.Count; cont++)
            {
                if (cont == unknownItems.Count - 1 && itemToAdd != questItems[cont])
                {
                    unknownItems.Add(itemToAdd);
                }
            }
        }
        allItems.Add(itemToAdd);
    }
    
    //method for adding quantities for an already created item
    public void StoreItemInInventory(ItemReference item)
    {
        if (item.type == EnumsScript.ItemType.Consumable)
        {
            for (int cont = 0; cont < consumableItems.Count; cont++)
            {
                if (item.itemId == consumableItems[cont].itemId)
                {
                    consumableItems[cont].quantityHeld += item.itemQuantity;
                }
            }
        }
        else if (item.type == EnumsScript.ItemType.Equipable)
        {
            for (int cont = 0; cont < equipableItems.Count; cont++)
            {
                if (item.itemId == equipableItems[cont].itemId)
                {
                    equipableItems[cont].quantityHeld += item.itemQuantity;
                }
            }
        }
        else if (item.type == EnumsScript.ItemType.Battle)
        {
            for (int cont = 0; cont < battleItems.Count; cont++)
            {
                if (item.itemId == battleItems[cont].itemId)
                {
                    battleItems[cont].quantityHeld += item.itemQuantity;
                }
            }
        }
        else if (item.type == EnumsScript.ItemType.Key)
        {
            for (int cont = 0; cont < keyItems.Count; cont++)
            {
                if (item.itemId == keyItems[cont].itemId)
                {
                    keyItems[cont].quantityHeld += item.itemQuantity;
                }
            }
        }
        else if (item.type == EnumsScript.ItemType.Quest)
        {
            for (int cont = 0; cont < questItems.Count; cont++)
            {
                if (item.itemId == questItems[cont].itemId)
                {
                    questItems[cont].quantityHeld += item.itemQuantity;
                }
            }
        }
    }

    //check if the item exist in inventory return false if it doesn't
    public bool ItemExist(BaseItem itemToAdd)
    {
        if(itemToAdd.GetType() == typeof(ConsumableItem))
        {
            ConsumableItem item = (ConsumableItem)itemToAdd;
            for (int cont = 0; cont < consumableItems.Count; cont++)
            {
                if (consumableItems[cont].itemId == item.itemId)
                {
                    return true;
                }
                else if (cont == consumableItems.Count - 1 && item.itemId != consumableItems[cont].itemId)
                {
                    return false;
                }
            }
        }
        else if (itemToAdd.GetType() == typeof(EquipableItem))
        {
            EquipableItem item = (EquipableItem)itemToAdd;
            for (int cont = 0; cont < equipableItems.Count; cont++)
            {
                if (equipableItems[cont].itemId == item.itemId)
                {
                    return true;
                }
                else if (cont == equipableItems.Count - 1 && item.itemId != equipableItems[cont].itemId)
                {
                    return false;
                }
            }
        }
        else if (itemToAdd.GetType() == typeof(BattleItem))
        {
            BattleItem item = (BattleItem)itemToAdd;
            for (int cont = 0; cont < battleItems.Count; cont++)
            {
                if (battleItems[cont].itemId == item.itemId)
                {
                    return true;
                }
                else if (cont == battleItems.Count - 1 && item.itemId != battleItems[cont].itemId)
                {
                    return false;
                }
            }
        }
        else if (itemToAdd.GetType() == typeof(KeyItem))
        {
            KeyItem item = (KeyItem)itemToAdd;
            for (int cont = 0; cont < keyItems.Count; cont++)
            {
                if (keyItems[cont].itemId == item.itemId)
                {
                    return true;
                }
                else if (cont == keyItems.Count - 1 && item.itemId != keyItems[cont].itemId)
                {
                    return false;
                }
            }
        }
        else if (itemToAdd.GetType() == typeof(QuestItem))
        {
            QuestItem item = (QuestItem)itemToAdd;
            for (int cont = 0; cont < questItems.Count; cont++)
            {
                if (questItems[cont].itemId == item.itemId)
                {
                    return true;
                }
                else if (cont == questItems.Count - 1 && item.itemId != questItems[cont].itemId)
                {
                    return false;
                }
            }
        }
        return false;
    }

    public List<BaseItem> GetAllItems()
    {
        allItems = new List<BaseItem>();
        for (int cont = 0; cont < consumableItems.Count; cont++)
        {
            allItems.Add(consumableItems[cont]);
        }
        for (int cont = 0; cont < equipableItems.Count; cont++)
        {
            allItems.Add(equipableItems[cont]);
        }
        for (int cont = 0; cont < battleItems.Count; cont++)
        {
            allItems.Add(battleItems[cont]);
        }
        for (int cont = 0; cont < keyItems.Count; cont++)
        {
            allItems.Add(keyItems[cont]);
        }
        for (int cont = 0; cont < questItems.Count; cont++)
        {
            allItems.Add(questItems[cont]);
        }
        return allItems;
    }

    //add gold to inventory
    public void AddGold(int goldToAdd)
    {
        gold += goldToAdd;
    }

    //sustract gold from inventory
    public void SustractGold(int goldToSustract)
    {
        if (goldToSustract < gold)
        {
            gold-=goldToSustract;
        }
    }

    public BaseItem GetItem(int itemId)
    {
        BaseItem itemToReturn = null;
        for (int cont = 0; cont < allItems.Count; cont++)
        {
            if (allItems[cont].itemId == itemId && allItems[cont].quantityHeld >= 1)
            {
                itemToReturn=allItems[cont];
            }
        }
        return itemToReturn;
    }

    public BaseItem GetConsumable(int itemId)
    {
        ConsumableItem itemToReturn = null;
        for (int cont = 0; cont < consumableItems.Count; cont++)
        {
            if (consumableItems[cont].itemId == itemId && consumableItems[cont].quantityHeld >= 1)
            {
                itemToReturn = consumableItems[cont];
            }
        }
        return itemToReturn;
    }

    public BaseItem GetEquipable(int itemId)
    {
        EquipableItem itemToReturn = null;
        for (int cont = 0; cont < equipableItems.Count; cont++)
        {
            if (equipableItems[cont].itemId == itemId && equipableItems[cont].quantityHeld >= 1)
            {
                itemToReturn = equipableItems[cont];
            }
        }
        return itemToReturn;
    }

    //check if the item exist in inventory
    public bool ItemExistOrHaveQuantity(int itemId)
    {
        for(int cont = 0; cont < allItems.Count; cont++)
        {
            if (allItems[cont].itemId == itemId && allItems[cont].quantityHeld >= 1)
            {
                return true;
            }
        }
        return false;
    }

    public bool ItemExist(int itemId)
    {
        for (int cont = 0; cont < allItems.Count; cont++)
        {
            if (allItems[cont].itemId == itemId)
            {
                return true;
            }
        }
        return false;
    }

    public void AddQuantityToInventory(int itemId, int quantity)
    {
        for (int cont = 0; cont < allItems.Count; cont++)
        {
            if (allItems[cont].itemId == itemId)
            {
                allItems[cont].quantityHeld += quantity;
                return;
            }
        }
    }

    public void LoadInventary()
    {

    }
}
