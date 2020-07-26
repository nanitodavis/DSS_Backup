using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCharacterController : MonoBehaviour
{
    public GameController gcReference;
    public bool canOpenMenu;

    float x, z;
    public float moveSpeed;
    public float runningSpeed;
    public bool isRunning;
    public bool canMove;
    public bool canInteract;

    public float rotationSpeed;
    public float gravity = 20f;
    Vector3 moveVector = Vector3.zero;
    public CharacterController controller;
    public Animator myAnimator;
    public TestCineManager townCameraReference;
    public EnterExitLocation actualExitLocation;
    public EnterExitBuilding actualEnterBuilding;
    public TownNpc actualTownNpc;
    public ShopNpc actualShopNpc;
    public bool onShop;

    public bool translateToBuilding;
    public Vector3 buildingPos;

    // Start is called before the first frame update
    void Start()
    {
        gcReference = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gcReference.player = this.gameObject;
        transform.position = gcReference.positionToLoad;
        canMove = true;
        canInteract = true;
        canOpenMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (translateToBuilding)
        {
            transform.position = buildingPos;
            if (transform.position == buildingPos)
            {
                translateToBuilding = false;
                controller.enabled = true;
                canMove = true;
            }
        }

        if (Input.GetButton("BButton") && canMove)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        townCameraReference.CameraPointingDirections();
        if (canMove)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }
        else
        {
            x = 0;
            z = 0;
        }
        myAnimator.SetFloat("walkrun", Mathf.Sqrt(Mathf.Pow((x + z), 2)));

        //applies camera direction to the player
        moveVector = x * townCameraReference.camRight + z * townCameraReference.camForward;
        //moveVector.z = z;

        //always faces the direction you're moving to
        controller.transform.LookAt(controller.transform.position + moveVector);

        //applies gravity
        moveVector.y = -gravity * Time.deltaTime;

        //applies movement using character controller
        if (isRunning&&canMove)
        {
            controller.Move(moveVector.normalized * runningSpeed * Time.deltaTime);
        }
        //if the user press the running button, enter here instead
        else if (canMove)
        {
            controller.Move(moveVector.normalized * moveSpeed * Time.deltaTime);
        }

        if (!onShop && !translateToBuilding&&canOpenMenu)
        {
            if (Input.GetButtonDown("YButton"))
            {
                canMove = false;
                canInteract = false;
                gcReference.actualUiManager.OpenGeneralPlayerMenu();
            }
        }


        if (canInteract)
        {
            if (Input.GetButtonDown("AButton"))
            {
                canOpenMenu = false;
                ExecuteAction();
            }
        }

        /*if (onShop)
        {
            if (Input.GetButtonDown("BButton"))
            {
                actualShopNpc.CloseShoppingPanel();
            }
        }*/
    }

    //action to execute if the player press the action button
    void ExecuteAction()
    {
        if (actualShopNpc != null&& Vector3.Distance(actualShopNpc.transform.position, transform.position) < actualShopNpc.interactDistance)
        {
            actualShopNpc.Interact();
        }
        else if(actualTownNpc!=null&& Vector3.Distance(actualTownNpc.transform.position, transform.position) < actualTownNpc.talkDistance)
        {
            actualTownNpc.NPCPushTalk();
        }
        else if (actualEnterBuilding != null && Vector3.Distance(actualEnterBuilding.transform.position, transform.position) < actualEnterBuilding.detectionDistance)
        {
            actualEnterBuilding.Execute();
        }
        else if (actualExitLocation != null && Vector3.Distance(actualExitLocation.transform.position, transform.position) < actualExitLocation.detectionDistance)
        {
            actualExitLocation.Execute();
        }
        else
        {
            actualShopNpc = null;
            actualTownNpc = null;
            actualEnterBuilding = null;
            actualExitLocation = null;
        }
    }

    public void CloseMenu()
    {
        canMove = true;
        canInteract = true;
    }

    public void ForceMovement(Vector3 newPosition)
    {
        buildingPos = newPosition;
        controller.enabled = false;
        translateToBuilding = true;
        canMove = false;
    }

    public void RotatePlayerOutside(float rotation)
    {
        transform.localRotation = Quaternion.Euler(0f, rotation, 0f);
    }

    public void SetExitLocation(EnterExitLocation loc)
    {
        actualExitLocation = loc;
    }

    public void SetActualBuilding(EnterExitBuilding building)
    {
        actualEnterBuilding = building;
    }

    public void SetNpc(TownNpc npc)
    {
        actualTownNpc = npc;
    }

    public void SetShopNpc(ShopNpc shop)
    {
        actualShopNpc = shop;
    }
}
