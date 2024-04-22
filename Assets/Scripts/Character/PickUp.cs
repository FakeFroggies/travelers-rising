using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    float distance = 200;
    public Transform pos;
    private Rigidbody rb;
    private BoxCollider bc;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, distance))
        {
                rb.isKinematic = true;
                rb.MovePosition(pos.position);
                bc.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.isKinematic == true)
        {
            this.gameObject.transform.position = pos.position;
            if(Input.GetKeyDown(KeyCode.G))
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                bc.enabled = true;
            }
        }
    }
}
