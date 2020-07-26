using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestCineManager : MonoBehaviour
{

    public CinemachineBrain cameraBrain;
    public GameObject[] cameras;

    public Vector3 camForward;
    public Vector3 camRight;

    public void ActivatesCamera(int cameraToActivate)
    {
        if (cameraToActivate < cameras.Length)
        {
            cameras[cameraToActivate].SetActive(true);
        }
        for(int cont = 0; cont < cameras.Length;cont++)
        {
            if (cont != cameraToActivate)
            {
                cameras[cont].SetActive(false);
            }
        }
    }
    public void CameraPointingDirections()
    {
        camForward = transform.forward;
        camRight = transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
}