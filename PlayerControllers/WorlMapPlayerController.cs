using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorlMapPlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float rotationSpeed;
    public float gravity = 20f;
    Vector3 moveVector = Vector3.zero;
    public CharacterController controller;
    public GameController gcReference;
    WorldMapCamera camReference;

    public EnterExitLocation actualExitLocation;
    public TownNpc actualTownNpc;
    public ShopNpc actualShopNpc;

    public bool canMove;
    public bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        camReference = Camera.main.GetComponent<WorldMapCamera>();
        gcReference= GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        transform.position = gcReference.positionToLoad;
        canMove = true;
        canInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            camReference.CameraPointingDirections();//actualizes camera values
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //guarantees the move vector to face the same angle as the camera
            moveVector = x * camReference.camRight + z * camReference.camForward;

            //always faces the direction you're moving to
            controller.transform.LookAt(controller.transform.position + moveVector);

            //applies gravity
            moveVector.y = -gravity * Time.deltaTime;

            //applies movement using character controller
            controller.Move(moveVector.normalized * moveSpeed * Time.deltaTime);
        }

        if (canInteract)
        {
            if (Input.GetButtonDown("Submit"))
            {
                ExecuteAction();
            }
        }
    }

    //action to execute if the player press the action button
    void ExecuteAction()
    {
        if (actualShopNpc != null && Vector3.Distance(actualShopNpc.transform.position, transform.position) < 5)
        {

        }
        else if (actualTownNpc != null && Vector3.Distance(actualTownNpc.transform.position, transform.position) < actualTownNpc.talkDistance)
        {
            actualTownNpc.NPCPushTalk();
        }
        else if (actualExitLocation != null && Vector3.Distance(actualExitLocation.transform.position, transform.position) < actualExitLocation.detectionDistance)
        {
            actualExitLocation.Execute();
        }
        else
        {
            actualShopNpc = null;
            actualTownNpc = null;
            actualExitLocation = null;
        }
    }

    public void RotatePlayerOutside(float rotation)
    {
        transform.localRotation = Quaternion.Euler(0f, rotation, 0f);
    }

    public void SetExitLocation(EnterExitLocation loc)
    {
        actualExitLocation = loc;
    }

    public void SetNpc()
    {

    }
}
