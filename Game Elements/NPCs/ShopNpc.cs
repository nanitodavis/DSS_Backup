using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpc : MonoBehaviour
{
    //general town npc variables
    public BaseNpc baseNpcInformation;
    public Animator myAnimator;
    public GameObject playerReference;
    public GameController gcReference;
    Rigidbody rb;
    public float interactDistance;
    public int goldAmased;

    //walk variables
    public Vector3[] destinations;
    public Vector3 actualDestination;
    public int destinationIndex;
    public float velocity;

    //idle variables
    public Stack<EnumsScript.NPC_State> npcState;
    public EnumsScript.NPC_State actualState;
    public bool onlyIdle;
    public bool userTalk;
    public bool canTalk;

    public float idleStateCounter;
    public float idleStateTime;

    //shop specific variables
    public List<ConsumableItem> consumableToSell;
    public List<EquipableItem> equipmentToSell;
    public GeneralUiManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        npcState = new Stack<EnumsScript.NPC_State>();
        npcState.Push(EnumsScript.NPC_State.IDLE);
        actualState = npcState.Peek();
        rb = GetComponent<Rigidbody>();
        playerReference = GameObject.FindGameObjectWithTag("Player");
        gcReference = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        actualState = npcState.Peek();
        ExecuteState();
        if(Vector3.Distance(playerReference.transform.position, transform.position)< interactDistance)
        {
            playerReference.GetComponent<TownCharacterController>().SetShopNpc(this);
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
        }
    }

    public void TalkToNpc()
    {
        myAnimator.SetFloat("walk", 0f); //To-Do check for changing to walking
        if (playerReference != null && userTalk)
        {
            if (baseNpcInformation.finishSpeak)
            {
                baseNpcInformation.finishSpeak = false;
                gcReference.actualUiManager.CloseNPCDialog();
                gcReference.player.GetComponent<TownCharacterController>().canMove = true;
                gcReference.player.GetComponent<TownCharacterController>().canOpenMenu = true;
                npcState.Pop();
                npcState.Push(EnumsScript.NPC_State.NONE);
                ActivateShoppingPanel();
            }
            else
            {
                gcReference.player.GetComponent<TownCharacterController>().canOpenMenu = false;
                gcReference.player.GetComponent<TownCharacterController>().canMove = false;
                gcReference.actualUiManager.ShowNPCDialog(baseNpcInformation.NextChat());
            }
        }
        userTalk = false;
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
        if (idleStateCounter > idleStateTime&&!onlyIdle)
        {
            idleStateCounter = 0;
            destinationIndex++;
            if (destinationIndex >= destinations.Length)
            {
                destinationIndex = 0;
            }
            actualDestination = destinations[destinationIndex];
            npcState.Push(EnumsScript.NPC_State.WALKING);
        }
    }

    public void Interact()
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

    public void ActivateShoppingPanel()
    {
        //To-Do
        gcReference.actualShop = this;
        gcReference.player.GetComponent<TownCharacterController>().canOpenMenu = false;
        gcReference.player.GetComponent<TownCharacterController>().onShop = true;
        gcReference.RestricPlayerMovementAndInteractionsTown();
        gcReference.actualUiManager.OpenShopPanel();
    }

    public void CloseShoppingPanel()
    {
        //To-Do
        if (!gcReference.actualUiManager.quantityActive)
        {
            npcState.Pop();
            gcReference.player.GetComponent<TownCharacterController>().onShop = false;
            gcReference.player.GetComponent<TownCharacterController>().canOpenMenu = true;
            gcReference.actualUiManager.shopPanel.SetActive(false);
            gcReference.RestorePlayerMovementAndInteractionsTown();
        }
    }
}
