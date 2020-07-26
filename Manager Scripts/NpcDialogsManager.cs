using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NpcDialogsManager : MonoBehaviour
{

    public TextMeshPro actualCharacterName;
    public TextMeshPro dialogText;
    public int spriteType;
    public Image spriteType1;
    public Image spriteType2;
    public GameObject acceptButton;
    public GameObject cancelButton;
    bool answer;
    public Sprite defaultTransparentSprite;
    public GameObject actualNpcReference;

    public void NextDialog(string charName, string dialog, int spriteTypeInt, Sprite portrait)
    {
        actualCharacterName.text = charName;
        dialogText.text = dialog;
        if (spriteTypeInt == 1)
        {
            spriteType1.sprite = portrait;
            spriteType2.enabled = false;
            spriteType1.enabled=true;
        }else if (spriteTypeInt==2)
        {
            spriteType2.sprite = portrait;
            spriteType1.enabled = false;
            spriteType2.enabled = true;
        }
    }

    public void RefreshDialogs()
    {

    }

    public void ConfigureAnswerButtons(string option1, string option2)
    {
        acceptButton.GetComponent<TextMeshPro>().text = option1;
        acceptButton.SetActive(true);
        cancelButton.GetComponent<TextMeshPro>().text = option2;
        cancelButton.SetActive(true);
    }

    public bool ReturnPlayerAnswer()
    {
        return answer;
    }

    public void AnswerSelected(bool answerToSet)
    {
        answer = answerToSet;
        acceptButton.SetActive(false);
        cancelButton.SetActive(false);
    }
}
