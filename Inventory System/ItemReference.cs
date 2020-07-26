[System.Serializable]
public class ItemReference
{
    public int itemId;
    public int itemQuantity;
    public EnumsScript.ItemType type;

    public ItemReference()
    {
        itemId = 0;
        itemQuantity = 0;
        type = EnumsScript.ItemType.Consumable;
    }

    public ItemReference(int id, int quantity, EnumsScript.ItemType itemType)
    {
        itemId = id;
        itemQuantity = quantity;
        type = itemType;
    }
}
