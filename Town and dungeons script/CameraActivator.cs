using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivator : MonoBehaviour
{
    public int cameraToActivate;
    public TestCineManager cameraBrainReference;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cameraBrainReference.ActivatesCamera(cameraToActivate);
        }
    }
}
