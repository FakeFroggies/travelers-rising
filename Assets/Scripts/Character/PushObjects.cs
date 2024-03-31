using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour
{
    public float pushForce = 0.5f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            
            rigidbody.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
        }
    }
}
