using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TownNpc : MonoBehaviour
{
    //general town npc variables
    public BaseNpc baseNpcInformation;
    public Animator myAnimator;
    public GameObject playerReference;
    public GeneralUiManager uiManager;
    public bool giveItem;
    public bool itemGiven;
    public BaseItem itemToGive;
    public string afterGiveItemDialog;
    public Rigidbody rb;
    public float velocity;
    public float talkDistance;

    //move variables
    public bool randomDestination;
    public Vector3[] destinations;
    public Vector3 actualDestination;

    //idle variables
    public Stack<EnumsScript.NPC_State> npcState;
    public EnumsScript.NPC_State actualState;
    public bool onlyIdle;
    public float idleStateTime;
    public float idleStateCounter;

    //talk variables
    public bool userTalk;
    public bool canTalk;

    //bubble talk variables
    public GameObject bubble;
    public TextMeshPro bubbleText;
    public bool canActionWhileBubble;
    public float bubbleActivationDistance;
    public bool activated;

    private void Start()
    {
        npcState = new Stack<EnumsScript.NPC_State>();
        npcState.Push(EnumsScript.NPC_State.IDLE);
        actualState = npcState.Peek();
        rb = GetComponent<Rigidbody>();
        playerReference = GameObject.FindGameObjectWithTag("Player");
        //gcReference = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        actualState = npcState.Peek();
        ExecuteState();
        if (canActionWhileBubble)
        {
            if (Vector3.Distance(playerReference.transform.position, transform.position) < bubbleActivationDistance)
            {
                if (!activated)
                {
                    activated = true;
                    bubbleText.text = (baseNpcInformation.ReturnFirstDialog().message);
                    bubble.SetActive(true);
                }
            }
            else
            {
                if (bubble.activeInHierarchy)
                {
                    bubble.SetActive(false);
                    activated = false;
                }
            }
        }
    }

    public void ExecuteState()
    {
        switch (actualState)
        {
            case EnumsScript.NPC_State.IDLE:
                IdleState();
                break;
            case EnumsScript.NPC_State.WALKING:
                MoveNpc();
                break;
            case EnumsScript.NPC_State.TALKING:
                TalkToNpc();
                break;
            case EnumsScript.NPC_State.BUBBLE_TALK:
                break;
        }
    }

    public void TalkToNpc()
    {
        myAnimator.SetFloat("walk", 0f); //To-Do check for changing to walking
        if (playerReference != null&&userTalk)
        {
            if (baseNpcInformation.finishSpeak)
            {
                baseNpcInformation.finishSpeak = false;
                uiManager.CloseNPCDialog();
                npcState.Pop();
            }
            else
            {
                uiManager.ShowNPCDialog(baseNpcInformation.NextChat());
            }
        }
        userTalk = false;
    }

    public void BubbleTalk()
    {
        if (Vector3.Distance(playerReference.transform.position, transform.position) < bubbleActivationDistance)
        {
            if (activated)
            {
                activated = true;
                bubbleText.text = (baseNpcInformation.ReturnFirstDialog().message);
                bubble.SetActive(true);
            }
        }
        else
        {
            npcState.Pop();
        }
    }

    public void GiveItem()
    {
        if (giveItem)
        {

        }
    }

    public void DecideWhereToGo()
    {
        actualDestination = destinations[Random.Range(0, destinations.Length)];
    }

    public void MoveNpc()
    {
        transform.position = Vector3.MoveTowards(transform.position, actualDestination, velocity * Time.deltaTime);
        transform.LookAt(actualDestination, Vector3.up);
        myAnimator.SetFloat("walk", 0.5f);
        if (Vector3.Distance(transform.position, actualDestination) < 1)
        {
            npcState.Pop();
        }
    }

    public void IdleState()
    {
        myAnimator.SetFloat("walk", 0f);
        idleStateCounter += Time.deltaTime;
        if (idleStateCounter > idleStateTime)
        {
            idleStateCounter = 0;
            DecideWhereToGo();
            npcState.Push(EnumsScript.NPC_State.WALKING);
        }
    }

    public void NPCPushTalk()
    {
        if (canTalk)
        {
            if (npcState.Peek() != EnumsScript.NPC_State.TALKING)
            {
                npcState.Push(EnumsScript.NPC_State.TALKING);
                userTalk = true;
            }
            else
            {
                userTalk = true;
            }
        }
    }
}