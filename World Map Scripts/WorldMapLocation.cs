using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapLocation : MonoBehaviour
{

    public string locationName;
    public GameObject playerReference;
    public float distance;
    public string mainSceneToLoad;
    public string specialSceneToLoad;
    public bool entranceAvailable;
    public SphereCollider triggerReference;

    // Start is called before the first frame update
    void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
    }
}
