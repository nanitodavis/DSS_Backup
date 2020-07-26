using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCamera : MonoBehaviour
{
    //requires encapsulation
    public Transform target;
    public Vector3 offset;
    Vector3 cameraMove;
    public float rotationAngle;
    public Vector3 camForward;
    public Vector3 camRight;

    //for rotating
    public float damping;
    float currentRotation;
    public float rotateSpeed;

    //for zooming
    [SerializeField]float currentZoom;
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    float temp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentRotation += Input.GetAxis("Horizontal2") * rotateSpeed * Time.deltaTime;
        currentZoom -= Input.GetAxis("Vertical2") *zoomSpeed* Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        Quaternion rot = Quaternion.Euler(cameraMove.x, rotationAngle, 0);
        cameraMove = target.position + offset*currentZoom;//sum the ZOOM value for approaching or withdraw the camera
        transform.position = cameraMove;
        transform.LookAt(target);//sum vector3.up and multiply by damping when creating zoom
        transform.RotateAround(target.position, Vector3.up, currentRotation);
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
