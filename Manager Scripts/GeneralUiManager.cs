using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System;

public class GeneralUiManager : MonoBehaviour
{
    GameController gcReference;
    bool inputControl;
    public Button initialGeneralMenuButton;

    //PlayerMenu
    [Header("Player Menu Panels")]
    public GameObject generalMenuPanel;
    public GameObject inventoryPanel;
    public GameObject playerStatusPanel;
    public GameObject equipmentPanel;
    public GameObject questLogPanel;
    public GameObject settingsPanel;
    public GameObject abilitiesPanel;
    public GameObject spiritualArtsPanel;
    public GameObject confirmPanel;
    public TextMeshProUGUI confirmPanelText;

    #region Talk NPC Elements
    //NPC talk elements
    [Header("NPC Elements")]
    public GameObject npcTalkPanel;
    public TextMeshProUGUI npcText;
    public Image npcSprite;
    #endregion

    #region Shop Elements
    //Shop NPC Elements
    [Header("Shop Elements")]
    public GameObject shopPanel;
    public List<GameObject> totalItems;
    public List<GameObject> sortedItems;
    public Transform allItemsContainer;
    public bool allItems;
    public bool onlyConsumables;
    public bool onlyEquipment;
    public bool onlyBattle;
    public TextMeshProUGUI shopName;
    public TextMeshProUGUI itemDescription;
    public Image itemSprite;
    public TextMeshProUGUI itemStats;
    public GameObject itemButtonPrefab;
    public Image shopNpcSprite;
    public bool quantityActive;
    public ShopItemButton shopItemButtonRef;
    #endregion

    #region Inventory Elements
    //Inventory System Elements
    [Header("Inventory Elements")]
    public List<GameObject> totalInventoryItems;
    public List<GameObject> sortedInventoryItems;
    public Transform inventoryContainer;
    public Image inventoryItemSprite;
    public TextMeshProUGUI inventoryItemName;
    public TextMeshProUGUI inventoryItemDescription;
    public TextMeshProUGUI inventoryItemDescription2;
    public GameObject inventoryItemButtonPrefab;

    public List<GameObject> totalCharactersList;
    public GameObject useItemPanel;
    public Transform inventoryCharactersContainer;
    public GameObject inventoryCharacterPrefab;
    public GameObject equipItemPanel;
    #endregion

    #region Equipment Elements
    [Header("Equipment Elements")]
    public Button initialEquipment;
    public CharacterProfile actualCharacterToEquip;
    public List<GameObject> totalEquipmentItems;
    public List<GameObject> sortedEquipmentItems;
    public Transform equipmentContainer;
    public GameObject equipmentItemButtonPrefab;
    public TextMeshProUGUI equipmentCharacterName;
    public TextMeshProUGUI equipmentDescription;
    public TextMeshProUGUI equipedItemStats;
    public TextMeshProUGUI selectedItemStats;
    public List<EquipmentInventoryButton> equipmentPanelButtons;
    #endregion

    #region Player Status Elements
    [Header("Player Status Elements")]
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI statsValues;
    public int actualPlayerStatusIndex;
    public CharacterProfile actualPlayerStatus;
    public Button exitPlayerStatusButton;
    #endregion

    private void Start()
    {
        gcReference = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gcReference.actualUiManager = this;

        //shop list Initialization
        totalItems = new List<GameObject>();
        sortedItems = new List<GameObject>();

        //inventory list initialization
        totalInventoryItems = new List<GameObject>();
        sortedInventoryItems = new List<GameObject>();

        totalCharactersList = new List<GameObject>();
    }

    void Update()
    {
        if (inputControl)
        {
            if (generalMenuPanel.activeInHierarchy && Input.GetButtonDown("CancelMenu")&&!shopPanel.activeInHierarchy)
            {
                CloseGeneralPlayerMenu();
            }
            else if (shopPanel.activeInHierarchy && !quantityActive && Input.GetButtonDown("CancelMenu"))
            {
                CloseShopPanel();
            }
            else if(shopPanel.activeInHierarchy && quantityActive && Input.GetButtonDown("CancelMenu"))
            {
                shopItemButtonRef.item.quantityHeld = 1;
                quantityActive = false;
                shopItemButtonRef.quantityPanel.SetActive(false);
                shopItemButtonRef.mySelf.Select();
                EnableOtherShopButtons();
                shopItemButtonRef = null;
            }
            else if (playerStatusPanel.activeInHierarchy && Input.GetButtonDown("CancelMenu"))
            {
                ClosePlayerStatusWindow();
            }
            else if (playerStatusPanel.activeInHierarchy && Input.GetButtonDown("CancelMenu"))
            {
                ClosePlayerStatusWindow();
            }
            else if (equipmentPanel.activeInHierarchy && Input.GetButtonDown("CancelMenu"))
            {
                CloseEquipmentMenu();
            }
            else if (questLogPanel.activeInHierarchy && Input.GetButtonDown("CancelMenu"))
            {
                //close quest log panel
            }
            else if (settingsPanel.activeInHierarchy && Input.GetButtonDown("CancelMenu"))
            {
                //close settings panel
            }else if (useItemPanel.activeInHierarchy && Input.GetButtonDown("CancelMenu"))
            {
                CloseApplyConsumableToCharacter();
            }
        }
    }

    #region General Menu
    public void OpenGeneralPlayerMenu()
    {
        inputControl = true;
        generalMenuPanel.SetActive(true);
    }

    public void CloseGeneralPlayerMenu()
    {
        generalMenuPanel.SetActive(false);
        inputControl = false;
        gcReference.player.GetComponent<TownCharacterController>().CloseMenu();
    }
    #endregion

    #region talk NPC section
    public void ShowNPCDialog(Dialog dialog)
    {
        if (dialog != null)
        {
            npcSprite.sprite = dialog.portrait;
            npcText.text = dialog.message;
            npcTalkPanel.SetActive(true);
        }
    }

    public void CloseNPCDialog()
    {
        npcTalkPanel.SetActive(false);
        npcSprite.sprite = null;
        npcText.text = "";
    }

    #endregion

    #region shop section
    //populates the container with the buttons for the item shop
    public void PopulateShopContainer()
    {
        //takes the actual shop reference consumable item list and generates them as interactable ui buttons
        for (int cont = 0; cont < gcReference.actualShop.consumableToSell.Count; cont++)
        {
            GameObject newItem = Instantiate(itemButtonPrefab);
            newItem.GetComponent<ShopItemButton>().SetupItemButton(gcReference.actualShop.consumableToSell[cont], this);
            totalItems.Add(newItem);
            newItem.transform.parent = null;
            newItem.transform.SetParent(allItemsContainer);
        }
        //takes the actual shop reference in the Game Controller equipment item list and generates them as interactable ui buttons
        for (int cont = 0; cont < gcReference.actualShop.equipmentToSell.Count; cont++)
        {
            GameObject newItem = Instantiate(itemButtonPrefab);
            newItem.GetComponent<ShopItemButton>().SetupItemButton(gcReference.actualShop.equipmentToSell[cont], this);
            totalItems.Add(newItem);
            newItem.transform.parent = null;
            newItem.transform.SetParent(allItemsContainer);
        }

        //sets the navigation method for each button in the recently generated list
        for (int cont = 0; cont < totalItems.Count; cont++)
        {
            if (cont > 0 && cont < totalItems.Count - 1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = totalItems[cont - 1].GetComponent<Button>();
                nav.selectOnDown = totalItems[cont + 1].GetComponent<Button>();
                totalItems[cont].GetComponent<Button>().navigation = nav;
            }
            else if (cont == 0)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnDown = totalItems[cont + 1].GetComponent<Button>();
                totalItems[cont].GetComponent<Button>().navigation = nav;
            }
            else if (cont == totalItems.Count - 1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = totalItems[cont - 1].GetComponent<Button>();
                totalItems[cont].GetComponent<Button>().navigation = nav;
            }
        }
        totalItems[0].GetComponent<Button>().Select();
    }

    //destroys the shop items button prefabs created and stored in the shop items container
    public void DeleteShopItemsFromContainer()
    {
        for (int cont = allItemsContainer.childCount; cont > 0; cont--)
        {
            Destroy(allItemsContainer.GetChild(cont - 1).gameObject);
        }
    }

    //deletes any created button
    //initialices tje list reference that store them
    //populates the shops with new buttons
    public void RefreshShop()
    {
        DeleteShopItemsFromContainer();
        totalItems = new List<GameObject>();
        PopulateShopContainer();
    }

    //method need optimization
    public void OrganizeShopItems()
    {
        if (allItems)
        {
            for (int cont = 0; cont < totalItems.Count; cont++)
            {
                totalItems[cont].SetActive(true);
            }
        }
        else if (onlyConsumables)
        {
            for (int cont = 0; cont < totalItems.Count; cont++)
            {
                if (totalItems[cont].GetComponent<ShopItemButton>().item.itemType==EnumsScript.ItemType.Consumable)
                {
                    totalItems[cont].SetActive(false);
                }
            }
            onlyBattle = false;
            onlyEquipment = false;
        }
        else if (onlyEquipment)
        {
            for (int cont = 0; cont < totalItems.Count; cont++)
            {
                if (totalItems[cont].GetComponent<ShopItemButton>().item.itemType == EnumsScript.ItemType.Equipable)
                {
                    totalItems[cont].SetActive(false);
                }
            }
            onlyBattle = false;
            onlyConsumables = false;
        }
        else if (onlyBattle)
        {
            for (int cont = 0; cont < totalItems.Count; cont++)
            {
                if (totalItems[cont].GetComponent<ShopItemButton>().item.itemType == EnumsScript.ItemType.Battle)
                {
                    totalItems[cont].SetActive(false);
                }
            }
            onlyEquipment = false;
            onlyConsumables = false;
        }
    }

    //disable all shop buttons but the one the method receives
    public void DisableOtherShopButtons(BaseItem item)
    {
        for(int cont = 0; cont < totalItems.Count; cont++)
        {
            if (totalItems[cont].GetComponent<ShopItemButton>().item.itemId != item.itemId)
            {
                totalItems[cont].GetComponent<ShopItemButton>().mySelf.interactable = false;
            }
        }
    }

    //enables all the shop buttons
    public void EnableOtherShopButtons()
    {
        for (int cont = 0; cont < totalItems.Count; cont++)
        {
            totalItems[cont].GetComponent<ShopItemButton>().mySelf.interactable = true;
        }
    }

    //execute the buy function in the Game Controller reference with the item passed
    public void BuyShopItem(BaseItem item)
    {
        gcReference.BuyItem(item);
    }

    //open the shop panel and enable uimanager controller
    public void OpenShopPanel()
    {
        RefreshShop();
        shopPanel.SetActive(true);
        inputControl = true;
    }

    //Closes shop panel and enables player movement via the Game Controller
    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
        gcReference.actualShop.CloseShoppingPanel();
    }
    #endregion

    #region Player Inventory Section
    public void PopulateInventoryPanel()
    {
        for (int cont = 0; cont < gcReference.inventoryReference.GetAllItems().Count; cont++)
        {
            GameObject newItem = Instantiate(inventoryItemButtonPrefab);
            newItem.GetComponent<InventoryItemButton>().SetUpItemButton(gcReference.inventoryReference.GetAllItems()[cont], this);
            if (newItem.GetComponent<InventoryItemButton>().item.quantityHeld > 0)
            {
                totalInventoryItems.Add(newItem);
                newItem.transform.parent = null;
                newItem.transform.SetParent(inventoryContainer);
            }
        }

        for(int cont = 0; cont < totalInventoryItems.Count; cont++)
        {
            if (cont > 0&&cont< totalInventoryItems.Count-1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = totalInventoryItems[cont - 1].GetComponent<Button>();
                nav.selectOnDown = totalInventoryItems[cont + 1].GetComponent<Button>();
                totalInventoryItems[cont].GetComponent<Button>().navigation = nav;
            }else if (cont == 0)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnDown = totalInventoryItems[cont + 1].GetComponent<Button>();
                totalInventoryItems[cont].GetComponent<Button>().navigation = nav;
            }else if (cont == totalInventoryItems.Count - 1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = totalInventoryItems[cont - 1].GetComponent<Button>();
                totalInventoryItems[cont].GetComponent<Button>().navigation = nav;
            }
        }
    }

    public void DeleteInventoryButtons()
    {
        //check this method
        for (int cont = inventoryContainer.childCount; cont > 0; cont--)
        {
            Destroy(inventoryContainer.GetChild(cont - 1).gameObject);
        }
        totalInventoryItems = new List<GameObject>();
    }

    public void RefreshInventoryItems()
    {
        List<GameObject> auxiliaryList = new List<GameObject>();
        for (int cont = 0; cont < totalInventoryItems.Count; cont++)
        {
            if (totalInventoryItems[cont].GetComponent<InventoryItemButton>().item.quantityHeld <= 0)
            {
                totalInventoryItems[cont].GetComponent<InventoryItemButton>().item.quantityHeld = 0;
                totalInventoryItems[cont].SetActive(false);
            }
            else
            {
                totalInventoryItems[cont].GetComponent<InventoryItemButton>().ActualizeButton();
                auxiliaryList.Add(totalInventoryItems[cont]);
            }
        }
        for (int cont = 0; cont < auxiliaryList.Count; cont++)
        {
            if (cont > 0 && cont < auxiliaryList.Count - 1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = auxiliaryList[cont - 1].GetComponent<Button>();
                nav.selectOnDown = auxiliaryList[cont + 1].GetComponent<Button>();
                auxiliaryList[cont].GetComponent<Button>().navigation = nav;
            }
            else if (cont == 0)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnDown = auxiliaryList[cont + 1].GetComponent<Button>();
                auxiliaryList[cont].GetComponent<Button>().navigation = nav;
            }
            else if (cont == auxiliaryList.Count - 1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = auxiliaryList[cont - 1].GetComponent<Button>();
                auxiliaryList[cont].GetComponent<Button>().navigation = nav;
            }
        }
    }

    public void OpenInventory()
    {
        if (!inventoryPanel.activeInHierarchy)
        {
            if (totalInventoryItems.Count > 0)
            {
                //check this option
                //RefreshInventoryItems();
                DeleteInventoryButtons();
                PopulateInventoryPanel();
            }
            else
            {
                PopulateInventoryPanel();
            }
            generalMenuPanel.SetActive(false);
            totalInventoryItems[0].GetComponent<Button>().Select();
            inventoryPanel.SetActive(true);
        }
    }

    public void ShowItemCharacteristicsInInventory(BaseItem itemToShow)
    {
        inventoryItemName.text = itemToShow.itemName;
        inventoryItemDescription.text = itemToShow.itemDescription;
        //inventoryItemDescription2.text = itemToShow.itemName;
        inventoryItemSprite.sprite = itemToShow.mySprite;
    }

    public void OpenUseConsumable()
    {

    }

    public void OpenListOfCharacterToApply(BaseItem item)
    {
        DeleteCharactersInventory();
        PopulateInventoryCharacters(item);
        useItemPanel.SetActive(true);
    }

    public void PopulateInventoryCharacters(BaseItem item)
    {
        for (int cont = 0; cont < gcReference.playerProfiles.Length; cont++)
        {
            GameObject newCharInv = Instantiate(inventoryCharacterPrefab);
            newCharInv.GetComponent<CharacterInventoryButton>().SetUpCharacterButton(gcReference.playerProfiles[cont].GetComponent<CharacterProfile>(), this, item.itemId);
            totalCharactersList.Add(newCharInv);
            newCharInv.transform.SetParent(null);
            newCharInv.transform.SetParent(inventoryCharactersContainer);
        }

        for (int cont = 0; cont < totalCharactersList.Count; cont++)
        {
            if (cont > 0 && cont < totalCharactersList.Count - 1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = totalCharactersList[cont - 1].GetComponent<Button>();
                nav.selectOnDown = totalCharactersList[cont + 1].GetComponent<Button>();
                totalCharactersList[cont].GetComponent<Button>().navigation = nav;
            }
            else if (cont == 0)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnDown = totalCharactersList[cont + 1].GetComponent<Button>();
                totalCharactersList[cont].GetComponent<Button>().navigation = nav;
            }
            else if (cont == totalCharactersList.Count - 1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = totalCharactersList[cont - 1].GetComponent<Button>();
                totalCharactersList[cont].GetComponent<Button>().navigation = nav;
            }
        }

        totalCharactersList[0].GetComponent<Button>().Select();
    }

    public void UseInventoryItem(int item, int id)
    {
        gcReference.UseInventoryItem(item, id);
        RefreshInventoryItems();
        CloseApplyConsumableToCharacter();
    }

    public void DeleteCharactersInventory()
    {
        //check this method
        for(int cont = inventoryCharactersContainer.childCount; cont > 0; cont--)
        {
            Destroy(inventoryCharactersContainer.GetChild(cont-1).gameObject);
        }
        totalCharactersList = new List<GameObject>();
    }

    public void CloseApplyConsumableToCharacter()
    {
        useItemPanel.SetActive(false);
        totalInventoryItems[0].GetComponent<Button>().Select();
    }

    public void CloseEquipItemToCharacter()
    {

    }

    public void CloseInventory()
    {
        generalMenuPanel.SetActive(true);
        inventoryPanel.SetActive(false);
    }
    #endregion

    #region Player Stats
    public void OpenStatusWindow()
    {
        actualPlayerStatus = gcReference.playerProfiles[actualPlayerStatusIndex].GetComponent<CharacterProfile>();
        UpdateCharacterProfileInformation();
        generalMenuPanel.SetActive(false);
        playerStatusPanel.SetActive(true);
    }

    private void UpdateCharacterProfileInformation()
    {
        characterName.text = actualPlayerStatus.baseInformation.name;
        description.text = actualPlayerStatus.baseInformation.log;
        statsValues.text = "HP: " + actualPlayerStatus.baseInformation.healthPoints + '\n' + '\n' +
            "SP: " + actualPlayerStatus.baseInformation.spiritualPoints + '\n' + '\n' +
            "Attack: " + actualPlayerStatus.baseInformation.physicalAttack + '\n' + '\n' +
            "Defense: " + actualPlayerStatus.baseInformation.physicalDefense + '\n' + '\n' +
            "Spiritual Attack: " + actualPlayerStatus.baseInformation.spiritualAttack + '\n' + '\n' +
            "Spiritual Defense: " + actualPlayerStatus.baseInformation.spiritualDefense + '\n' + '\n' +
            "Critic: " + actualPlayerStatus.baseInformation.critic;
    }

    public void NextCharacterStatus()
    {
        if (actualPlayerStatusIndex < gcReference.playerProfiles.Length - 1)
        {
            actualPlayerStatusIndex++;
            actualPlayerStatus = gcReference.playerProfiles[actualPlayerStatusIndex].GetComponent<CharacterProfile>();
            UpdateCharacterProfileInformation();
        }
        else
        {
            actualPlayerStatusIndex = 0;
            actualPlayerStatus = gcReference.playerProfiles[actualPlayerStatusIndex].GetComponent<CharacterProfile>();
            UpdateCharacterProfileInformation();
        }
    }

    public void PreviousCharacterStatus()
    {
        if (actualPlayerStatusIndex > 0)
        {
            actualPlayerStatusIndex--;
            actualPlayerStatus = gcReference.playerProfiles[actualPlayerStatusIndex].GetComponent<CharacterProfile>();
            UpdateCharacterProfileInformation();
        }
        else
        {
            actualPlayerStatusIndex = gcReference.playerProfiles.Length - 1;
            actualPlayerStatus = gcReference.playerProfiles[actualPlayerStatusIndex].GetComponent<CharacterProfile>();
            UpdateCharacterProfileInformation();
        }
    }

    public void ClosePlayerStatusWindow()
    {
        actualPlayerStatusIndex = 0;
        generalMenuPanel.SetActive(true);
        playerStatusPanel.SetActive(false);
    }
    #endregion

    #region Equipment

    //method for opening the equipment menu
    public void OpenEquipmentMenu()
    {
        generalMenuPanel.SetActive(false);//deactivates the generalmenu panel
        PopulateEquipmentWindow();//call the populate equipment window method
        initialEquipment.Select();//sets the first button of the of the equipment list as the default selection
        equipmentPanel.SetActive(true);//activates the equipment panel
    }

    //method for closing the equipment menu
    public void CloseEquipmentMenu()
    {
        equipmentPanel.SetActive(false);//deactivates the equipment panel
        generalMenuPanel.SetActive(true);//activates the generalmenu panel
        initialGeneralMenuButton.Select();//set the general menu default selected button
        //To-Do
        //Clean any desition or action that the user left unfinished
    }

    //To-Do needs optimization
    public void PopulateEquipmentWindow()
    {
        totalEquipmentItems = new List<GameObject>();//initialices the list that will hold reference to all equipable items generated buttons
        for (int cont = 0; cont < gcReference.inventoryReference.equipableItems.Count; cont++)
        {
            GameObject newItem = Instantiate(equipmentItemButtonPrefab);
            newItem.GetComponent<InventoryItemButton>().SetUpItemButton(gcReference.inventoryReference.equipableItems[cont], this);
            if (newItem.GetComponent<InventoryItemButton>().item.quantityHeld > 0)
            {
                totalEquipmentItems.Add(newItem);
                newItem.transform.parent = null;
                newItem.transform.SetParent(equipmentContainer);
                newItem.SetActive(false);
            }
        }

        //set the first character in the party as the actual character to equip an item
        actualCharacterToEquip = gcReference.playerProfiles[0].GetComponent<CharacterProfile>();

        //go through the list of buttons and apply the contained method within the for
        for(int cont = 0; cont < equipmentPanelButtons.Count; cont++)
        {
            SetEquipableItemToButton(equipmentPanelButtons[cont]);
        }
    }

    public void SetEquipableItemToButton(EquipmentInventoryButton button)
    {
        switch (button.equipmentType)
        {
            case EnumsScript.EquipedOn.HEAD:
                button.SetupEquipmentButton(actualCharacterToEquip.headEquipment);
                break;
            case EnumsScript.EquipedOn.CHEST:
                button.SetupEquipmentButton(actualCharacterToEquip.chestEquipment);
                break;
            case EnumsScript.EquipedOn.ARMS:
                button.SetupEquipmentButton(actualCharacterToEquip.armEquipment);
                break;
            case EnumsScript.EquipedOn.MAIN_WEAPON:
                button.SetupEquipmentButton(actualCharacterToEquip.mainWeaponEquipment);
                break;
            case EnumsScript.EquipedOn.OFF_HAND:
                button.SetupEquipmentButton(actualCharacterToEquip.offHandEquipment);
                break;
            case EnumsScript.EquipedOn.FOOT:
                button.SetupEquipmentButton(actualCharacterToEquip.footEquipment);
                break;
            case EnumsScript.EquipedOn.ACCESSORY1:
                button.SetupEquipmentButton(actualCharacterToEquip.accesoryOneEquipment);
                break;
            case EnumsScript.EquipedOn.ACCESSORY2:
                button.SetupEquipmentButton(actualCharacterToEquip.accesoryTwoEquipment);
                break;
        }
    }

    //method for actualizing the equipment thats shown in the list, based on the actual equipedOn value
    public void ActualizeEquipmentShown(EnumsScript.EquipedOn equipedOn, Button equipedOnButtonRef)
    {
        sortedEquipmentItems = new List<GameObject>();
        for(int cont = 0; cont < totalEquipmentItems.Count; cont++)
        {
            EquipableItem itemToCheck = (EquipableItem)totalEquipmentItems[cont].GetComponent<InventoryItemButton>().item;
            if (itemToCheck.equipedOn == equipedOn)
            {
                sortedEquipmentItems.Add(totalEquipmentItems[cont]);
                totalEquipmentItems[cont].SetActive(true);
            }
            else
            {
                totalEquipmentItems[cont].SetActive(false);
            }
        }

        for (int cont = 0; cont < sortedEquipmentItems.Count; cont++)
        {
            if (cont > 0 && cont < sortedEquipmentItems.Count - 1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = sortedEquipmentItems[cont - 1].GetComponent<Button>();
                nav.selectOnDown = sortedEquipmentItems[cont + 1].GetComponent<Button>();
                nav.selectOnLeft = equipedOnButtonRef;
                sortedEquipmentItems[cont].GetComponent<Button>().navigation = nav;
            }
            else if (cont == 0)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnDown = sortedEquipmentItems[cont + 1].GetComponent<Button>();
                nav.selectOnLeft = equipedOnButtonRef;
                sortedEquipmentItems[cont].GetComponent<Button>().navigation = nav;
            }
            else if (cont == sortedEquipmentItems.Count - 1)
            {
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnUp = sortedEquipmentItems[cont - 1].GetComponent<Button>();
                nav.selectOnLeft = equipedOnButtonRef;
                sortedEquipmentItems[cont].GetComponent<Button>().navigation = nav;
            }
        }
    }

    //sets the first button of the actual sorted list of equipment as the first selection
    public void SelectEquipmentButton()
    {
        if (sortedEquipmentItems.Count > 0)
        {
            sortedEquipmentItems[0].GetComponent<Button>().Select();
        }
    }

    //equip item to character
    public void EquipItem(EquipableItem itemToEquip)
    {
        gcReference.EquipInventoryItem(itemToEquip, actualCharacterToEquip);
        for (int cont = 0; cont < equipmentPanelButtons.Count; cont++)
        {
            SetEquipableItemToButton(equipmentPanelButtons[cont]);
        }
    }

    public void ActualizeEquipmentInformation(EquipableItem item)
    {
        EquipableItem itemToCompare = null;
        
        switch (item.equipedOn)
        {
            case EnumsScript.EquipedOn.HEAD:
                itemToCompare = actualCharacterToEquip.headEquipment;
                break;
            case EnumsScript.EquipedOn.CHEST:
                itemToCompare = actualCharacterToEquip.chestEquipment;
                break;
            case EnumsScript.EquipedOn.ARMS:
                itemToCompare = actualCharacterToEquip.armEquipment;
                break;
            case EnumsScript.EquipedOn.MAIN_WEAPON:
                itemToCompare = actualCharacterToEquip.mainWeaponEquipment;
                break;
            case EnumsScript.EquipedOn.OFF_HAND:
                itemToCompare = actualCharacterToEquip.offHandEquipment;
                break;
            case EnumsScript.EquipedOn.FOOT:
                itemToCompare = actualCharacterToEquip.footEquipment;
                break;
            case EnumsScript.EquipedOn.ACCESSORY1:
                itemToCompare = actualCharacterToEquip.accesoryOneEquipment;
                break;
            case EnumsScript.EquipedOn.ACCESSORY2:
                itemToCompare = actualCharacterToEquip.accesoryTwoEquipment;
                break;
        }
        //To-Do actualize the actual character equiped item
        equipmentDescription.text = item.itemName + '\n' + item.itemDescription;
        if (itemToCompare != null)
        {
            equipedItemStats.text = "Equipped" + '\n' + "hp: " + itemToCompare.hp + '\n' +
                "sp: " + itemToCompare.sp + '\n' +
                "attack: " + itemToCompare.attack + '\n' +
                "defence: " + itemToCompare.defence + '\n' +
                "S-Attack: " + itemToCompare.spiritualAttack + '\n' +
                "S-Defence: " + itemToCompare.spiritualDefence + '\n' +
                "Critic: " + itemToCompare.critic;
        }
        else
        {
            equipedItemStats.text = "Equipped" + '\n' + "No item";
        }

        selectedItemStats.text = "Selected" + '\n' + "hp: " + item.hp + '\n' +
            "sp: " + item.sp + '\n' +
            "attack: " + item.attack + '\n' +
            "defence: " + item.defence + '\n' +
            "S-Attack: " + item.spiritualAttack + '\n' +
            "S-Defence: " + item.spiritualDefence + '\n' +
            "Critic: " + item.critic;
    }
    #endregion

    #region Quest Log
    #endregion
}