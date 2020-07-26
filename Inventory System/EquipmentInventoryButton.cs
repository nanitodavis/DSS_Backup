using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Boo.Lang.Environments;
using System.Diagnostics.Tracing;

public class EquipmentInventoryButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{

    public Button mySelf;
    public TextMeshProUGUI equipedItemName;
    public GeneralUiManager uiReference;
    public EnumsScript.EquipedOn equipmentType;
    public EquipableItem equipedItemReference;

    void Start()
    {
        
    }

    public void SetupEquipmentButton(EquipableItem item)
    {
        equipedItemReference = item;
        ActualizeEquipedItemName();
    }

    public void ActualizeEquipedItemName()
    {
        if (equipedItemReference != null)
        {
            equipedItemName.text = equipedItemReference.itemName;
        }
        else
        {
            equipedItemName.text = "none";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiReference.ActualizeEquipmentShown(equipmentType, mySelf);
    }

    public void OnSelect(BaseEventData eventData)
    {
        uiReference.ActualizeEquipmentShown(equipmentType, mySelf);
    }
}