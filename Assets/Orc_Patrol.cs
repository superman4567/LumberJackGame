using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc_Patrol : MonoBehaviour
{
    public Transform[] patrolPoints; 
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomPatrolPoint();
    }

    private void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomPatrolPoint();
        }
    }

    private void SetRandomPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        // Choose a random index for the patrol point
        int randomIndex = Random.Range(0, patrolPoints.Length);

        // Set the randomly chosen patrol point as the AI's destination
        navMeshAgent.destination = patrolPoints[randomIndex].position;
    }
}
