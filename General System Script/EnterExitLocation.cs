using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExitLocation : MonoBehaviour
{
    public GameController gameControllerReference;
    public Transform playerReference;
    public string sceneToLoad;
    public float detectionDistance;
    public bool isWorldMapLocation;
    public Vector3 positionToSpawn;
    public SpriteRenderer sprite; //change this for a particle system effect
    public Quaternion rotationToFace;

    // Start is called before the first frame update
    void Start()
    {
        gameControllerReference = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerReference = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (Vector3.Distance(transform.position, playerReference.position) < detectionDistance) {
            sprite.enabled = true;
            if (isWorldMapLocation)
            {
                playerReference.GetComponent<WorlMapPlayerController>().SetExitLocation(this);
            }
            else
            {
                playerReference.GetComponent<TownCharacterController>().SetExitLocation(this);
            }
        }
        else
        {
            sprite.enabled = false;
        }
    }

    public void Execute()
    {
        gameControllerReference.ChangeLocation(positionToSpawn, rotationToFace, sceneToLoad);
    }
}
