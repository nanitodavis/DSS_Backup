using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInventoryButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public Button mySelf;
    public Image charSprite;
    public TextMeshProUGUI charName;
    public Slider hpSlider;
    public TextMeshProUGUI currentMaxHp;
    public Slider spSlider;
    public TextMeshProUGUI currentMaxSp;
    public TextMeshProUGUI status;

    public CharacterProfile character;
    public GeneralUiManager uiReference;
    public int itemToUse;

    // Start is called before the first frame update
    void Start()
    {
        mySelf.onClick.AddListener(UseItem);
    }

    public void SetUpCharacterButton(CharacterProfile charReference, GeneralUiManager ui, int item)
    {
        character = charReference;
        charSprite.sprite = character.smallInventoryNormalSprite;
        charName.text = character.baseInformation.name;
        hpSlider.maxValue = character.baseInformation.healthPoints;
        hpSlider.value = character.actualHealthPoints;
        currentMaxHp.text = character.actualHealthPoints.ToString() + "/" + character.baseInformation.healthPoints;
        spSlider.maxValue = character.baseInformation.spiritualPoints;
        spSlider.value = character.actualSpiritualPoints;
        currentMaxSp.text = character.actualSpiritualPoints.ToString() + "/" + character.baseInformation.SpiritualPointsProperty;
        status.text = character.baseInformation.condition.ToString();
        uiReference = ui;
        itemToUse = item;
    }

    public void UseItem()
    {
        
        uiReference.UseInventoryItem(itemToUse, character.baseInformation.characterId);
    }

    public void OnSelect(BaseEventData eventData)
    {
        //uiReference.ShowItemCharacteristicsInInventory(item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //uiReference.ShowItemCharacteristicsInInventory(item);
    }
}
