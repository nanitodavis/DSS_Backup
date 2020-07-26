using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public Button mySelf;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemPrice;
    public GameObject quantityPanel;
    public TextMeshProUGUI quantityText;
    public Button buyButton;

    public BaseItem item;
    public ItemReference itemRef;
    public GeneralUiManager uiManager;

    private void Start()
    {
        mySelf.onClick.AddListener(ShowQuantityPanel);
    }

    public void SetupItemButton(ConsumableItem receivedItem, GeneralUiManager ui)
    {
        item = receivedItem;
        uiManager = ui;
        itemName.text = item.itemName;
        itemPrice.text = item.price.ToString();
    }

    public void SetupItemButton(EquipableItem receivedItem, GeneralUiManager ui)
    {
        item = receivedItem;
        uiManager = ui;
        itemName.text = item.itemName;
        itemPrice.text = item.price.ToString();
        itemRef = new ItemReference(item.itemId, item.quantityHeld, item.itemType);
    }

    public void SetupItemButton(BattleItem receivedItem, GeneralUiManager ui)
    {
        item = receivedItem;
        uiManager = ui;
        itemName.text = item.itemName;
        itemPrice.text = item.price.ToString();
        itemRef = new ItemReference(item.itemId, item.quantityHeld, item.itemType);
    }

    public void SetUpItemButton(BaseItem receivedItem, GeneralUiManager ui)
    {
        item = receivedItem;
        uiManager = ui;
        itemName.text = item.itemName;
        itemPrice.text = item.price.ToString();
        itemRef = new ItemReference(item.itemId, item.quantityHeld, item.itemType);
    }

    public void ShowQuantityPanel() 
    {
        quantityPanel.SetActive(true);
        quantityText.text = item.quantityHeld.ToString();
        buyButton.Select();
        uiManager.quantityActive = true;
        uiManager.shopItemButtonRef = this;
        uiManager.DisableOtherShopButtons(item);
    }

    public void RaiseQuantity()
    {
        if (item.quantityHeld < 99)
        {
            item.quantityHeld += 1;
        }
        else
        {
            item.quantityHeld = 1;
        }
        quantityText.text = item.quantityHeld.ToString();
    }

    public void LowerQuantity()
    {
        if (item.quantityHeld > 1)
        {
            item.quantityHeld -= 1;
        }
        else
        {
            item.quantityHeld = 99;
        }
        quantityText.text = item.quantityHeld.ToString();
    }

    public void BuyItem()
    {
        uiManager.BuyShopItem(item);
        quantityPanel.SetActive(false);
        item.quantityHeld = 1;
        mySelf.Select();
    }
}
