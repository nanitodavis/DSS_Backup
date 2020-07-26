using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public Button mySelf;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemQuantity;
    public bool equipmentWindow;
    public BaseItem item;
    public GeneralUiManager uiReference;

    // Start is called before the first frame update
    void Start()
    {
        mySelf.onClick.AddListener(UseItem);
    }

    public void SetUpItemButton(BaseItem itemToReceive, GeneralUiManager ui)
    {
        item = itemToReceive;
        itemName.text = item.itemName;
        itemQuantity.text = item.quantityHeld.ToString();
        uiReference = ui;
    }

    //refreshes the info  showed in the button
    public void ActualizeButton()
    {
        itemName.text = item.itemName;
        itemQuantity.text = item.quantityHeld.ToString();
    }

    public void UseItem()
    {
        if (!equipmentWindow)
        {
            uiReference.OpenListOfCharacterToApply(item);
        }
        else
        {
            uiReference.EquipItem((EquipableItem)item);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (!equipmentWindow)
            uiReference.ShowItemCharacteristicsInInventory(item);
        else
            uiReference.ActualizeEquipmentInformation((EquipableItem)item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!equipmentWindow)
            uiReference.ShowItemCharacteristicsInInventory(item);
        else
            uiReference.ActualizeEquipmentInformation((EquipableItem)item);
    }
}
