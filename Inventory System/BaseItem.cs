using UnityEngine;

[System.Serializable]
public class BaseItem: ScriptableObject
{
    //basic attributes for all items, pending to code encapsulation
    public string itemName;
    public string itemDescription;
    public EnumsScript.ItemType itemType;
    public Sprite mySprite;
    public int itemId;
    public int quantityHeld;
    public bool available;
    public bool canBeSelled;
    public bool canBeDiscarted;

    public int price;
}
