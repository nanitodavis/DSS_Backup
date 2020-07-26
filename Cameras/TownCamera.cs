using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = target.position + offset;
        Vector3 pos = Vector3.Lerp(transform.position, newPos, Time.deltaTime* damping);
        transform.position = pos;
        transform.LookAt(target);
    }
}
