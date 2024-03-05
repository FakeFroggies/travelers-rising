using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targer;

    public float speed = 0.1f;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPos = targer.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, speed);
        transform.position = smoothPos;
    }
}
