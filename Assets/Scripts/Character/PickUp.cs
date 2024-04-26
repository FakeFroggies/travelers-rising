using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    float distance = 2000;
    public Transform pos;
    private Rigidbody rb;
    private BoxCollider bc;
    private MeshCollider[] mc;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        mc = GetComponents<MeshCollider>();
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, distance) && Vector3.Distance(hitInfo.point, pos.position) < 10f)
        {
            rb.isKinematic = true;
            rb.MovePosition(pos.position);
            if (bc)
                bc.enabled = false;
            else if (mc[0])
            {
                foreach (var mesh in mc)
                {
                    mesh.enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.isKinematic == true)
        {
            this.gameObject.transform.position = pos.position;
            if (Input.GetKeyDown(KeyCode.G))
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                if (bc)
                    bc.enabled = true;
                else if (mc[0])
                {
                    foreach (var mesh in mc)
                    {
                        mesh.enabled = true;
                    }
                }
            }
        }
    }
}
