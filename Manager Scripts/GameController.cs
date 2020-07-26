using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //positions
    public Vector3 positionToLoad;
    public Quaternion rotationToFace;

    public GameObject player;
    public GameObject[] playerProfiles;

    public ShopNpc actualShop;
    public GeneralUiManager actualUiManager;

    public PlayerInventory inventoryReference;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    //method for changing locations between towns, dungeons and worldmap
    public void ChangeLocation(Vector3 pos, Quaternion quat, string sceneToLoad)
    {
        positionToLoad = pos;
        rotationToFace = quat;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void RestricPlayerMovementAndInteractionsTown()
    {
        if (player.GetComponent<TownCharacterController>() != null)
        {
            player.GetComponent<TownCharacterController>().canInteract = false;
            player.GetComponent<TownCharacterController>().canMove = false;
        }
    }

    public void RestorePlayerMovementAndInteractionsTown()
    {
        if (player.GetComponent<TownCharacterController>() != null)
        {
            player.GetComponent<TownCharacterController>().canInteract = true;
            player.GetComponent<TownCharacterController>().canMove = true;
        }
    }

    public void RestricPlayerMovementAndInteractionsWorldMap()
    {
        if (player.GetComponent<WorlMapPlayerController>() != null)
        {
            player.GetComponent<WorlMapPlayerController>().canInteract = false;
            player.GetComponent<WorlMapPlayerController>().canMove = false;
        }
    }

    public void RestorePlayerMovementAndInteractionsWorldMap()
    {
        if (player.GetComponent<WorlMapPlayerController>() != null)
        {
            player.GetComponent<WorlMapPlayerController>().canInteract = true;
            player.GetComponent<WorlMapPlayerController>().canMove = true;
        }
    }

    public void UseInventoryItem(int itemId, int charId)
    {
        for(int cont = 0; cont < playerProfiles.Length; cont++)
        {
            if (playerProfiles[cont].GetComponent<CharacterProfile>().baseInformation.characterId == charId)
            {
                playerProfiles[cont].GetComponent<CharacterProfile>().ApplyItem(inventoryReference.GetItem(itemId));
                cont = playerProfiles.Length;
            }
        }
    }

    public void EquipInventoryItem(int itemId, int playerId)
    {
        
    }

    public void EquipInventoryItem(EquipableItem item, CharacterProfile character)
    {
        switch (item.equipedOn)
        {
            case EnumsScript.EquipedOn.HEAD:
                if (CheckIfCanEquip(item)){
                    character.headEquipment = item;
                    item.timesEquiped += 1;
                }
                break;
            case EnumsScript.EquipedOn.CHEST:
                if (CheckIfCanEquip(item))
                {
                    character.chestEquipment = item;
                    item.timesEquiped += 1;
                }
                break;
            case EnumsScript.EquipedOn.ARMS:
                if (CheckIfCanEquip(item))
                {
                    character.armEquipment = item;
                    item.timesEquiped += 1;
                }
                break;
            case EnumsScript.EquipedOn.MAIN_WEAPON:
                if (CheckIfCanEquip(item))
                {
                    character.mainWeaponEquipment = item;
                    item.timesEquiped += 1;
                }
                break;
            case EnumsScript.EquipedOn.OFF_HAND:
                if (CheckIfCanEquip(item))
                {
                    character.offHandEquipment = item;
                    item.timesEquiped += 1;
                }
                break;
            case EnumsScript.EquipedOn.FOOT:
                if (CheckIfCanEquip(item))
                {
                    character.footEquipment = item;
                    item.timesEquiped += 1;
                }
                break;
            case EnumsScript.EquipedOn.ACCESSORY1:
                if (CheckIfCanEquip(item))
                {
                    character.accesoryOneEquipment = item;
                    item.timesEquiped += 1;
                }
                break;
            case EnumsScript.EquipedOn.ACCESSORY2:
                if (CheckIfCanEquip(item))
                {
                    character.accesoryTwoEquipment = item;
                    item.timesEquiped += 1;
                }
                break;
        }
    }

    public bool CheckIfCanEquip(EquipableItem item)
    {
        //To-Do check if the specific user can equip the item

        //check if there's stock to equip the item selected
        if (item.quantityHeld > item.timesEquiped)
        {
            return true;
        }
        return false;
    }

    //swap equipment if there is already an item equiped
    public void SwapEquipment(EquipableItem ei, EnumsScript.EquipedOn eo, CharacterProfile character)
    {
        //based on where the item is equiped, it removes the actual item and reduce the equipment countar
        //then equip the new item and increases the equipment counter
        switch (eo)
        {
            case EnumsScript.EquipedOn.HEAD:
                character.headEquipment.timesEquiped -= 1;
                character.headEquipment = ei;
                ei.timesEquiped += 1;
                break;
            case EnumsScript.EquipedOn.CHEST:
                character.chestEquipment.timesEquiped -= 1;
                character.chestEquipment = ei;
                ei.timesEquiped += 1;
                break;
            case EnumsScript.EquipedOn.ARMS:
                character.armEquipment.timesEquiped -= 1;
                character.armEquipment = ei;
                ei.timesEquiped += 1;
                break;
            case EnumsScript.EquipedOn.MAIN_WEAPON:
                character.mainWeaponEquipment.timesEquiped -= 1;
                character.mainWeaponEquipment = ei;
                ei.timesEquiped += 1;
                break;
            case EnumsScript.EquipedOn.OFF_HAND:
                character.offHandEquipment.timesEquiped -= 1;
                character.offHandEquipment = ei;
                ei.timesEquiped += 1;
                break;
            case EnumsScript.EquipedOn.FOOT:
                character.footEquipment.timesEquiped -= 1;
                character.footEquipment = ei;
                ei.timesEquiped += 1;
                break;
            case EnumsScript.EquipedOn.ACCESSORY1:
                character.accesoryOneEquipment.timesEquiped -= 1;
                character.accesoryOneEquipment = ei;
                ei.timesEquiped += 1;
                break;
            case EnumsScript.EquipedOn.ACCESSORY2:
                character.accesoryTwoEquipment.timesEquiped -= 1;
                character.accesoryTwoEquipment = ei;
                ei.timesEquiped += 1;
                break;
        }
    }


    //To-Do
    public void BuyItem(BaseItem itemToBuy)
    {
        if (inventoryReference.gold > itemToBuy.price * itemToBuy.quantityHeld&&inventoryReference.ItemExist(itemToBuy))
        {
            inventoryReference.SustractGold(itemToBuy.price * itemToBuy.quantityHeld);
            inventoryReference.StoreItemInInventory(itemToBuy);
        }
        itemToBuy.quantityHeld = 1;
    }

    public void BuyItem(int itemId, int price, int quantity)
    {
        if (inventoryReference.ItemExist(itemId)&& inventoryReference.gold >price*quantity)
        {
            inventoryReference.SustractGold(price * quantity);
            inventoryReference.AddQuantityToInventory(itemId, quantity);
        }
    }
}