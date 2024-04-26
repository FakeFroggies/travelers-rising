using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPatrolPointIndex = 0;
    public float moveSpeed = 3.0f;

    public Transform target;
    public float chaseRange = 10.0f;
    public float attackRange = 2.0f;
    public float attackRate = 1.0f;
    private float nextAttackTime = 0.0f;

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
    }

    void Update()
    {

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }
        else if (distanceToTarget <= chaseRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        Vector3 targetPosition = patrolPoints[currentPatrolPointIndex].position;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Chase()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        Debug.Log("Attacking the target!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Camera.main.GetComponent<Drugs>().onDrugs = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Camera.main.GetComponent<Drugs>().onDrugs = false;
        }
    }
}