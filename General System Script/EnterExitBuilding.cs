using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExitBuilding : MonoBehaviour
{
    public Transform playerReference;
    public float detectionDistance;
    public Vector3 positionToMove;
    public SpriteRenderer sprite; //change this for a particle system effect
    public Quaternion rotationToFace;
    public GameObject cameraToDeactivate;
    public GameObject buildingCamera;



    // Start is called before the first frame update
    void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerReference.position) < detectionDistance)
        {
            sprite.enabled = true;
            playerReference.GetComponent<TownCharacterController>().SetActualBuilding(this);
        }
        else
        {
            sprite.enabled = false;
        }
    }

    public void Execute()
    {
        playerReference.GetComponent<TownCharacterController>().ForceMovement(positionToMove);
        cameraToDeactivate.SetActive(false);
        buildingCamera.SetActive(true);
    }
}
