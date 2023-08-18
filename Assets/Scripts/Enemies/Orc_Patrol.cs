using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc_Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints; 
    [SerializeField] private float playerDetectionRadius = 5.0f; 
    [SerializeField] private float alarmRadius = 10.0f; 
    [SerializeField] private float chaseDuration = 10.0f;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform player;

    [SerializeField] public float orcWalkSpeed = 1.2f;
    [SerializeField] public float orcChaseSpeed = 4f;

    public bool isAlerted = false;
    public bool isChasing = false;
    private float chaseTimer = 0.0f;
    private int currentPatrolIndex = 0;
    
    private void Start()
    {
        ShufflePatrolPoints();
        SetNextPatrolPoint();
        navMeshAgent.speed = orcWalkSpeed;
    }

    private void Update()
    {
        if (player != null)
        {
            if (IsPlayerNearby() || isAlerted)
            {
                IsChasing();
                isChasing = true;
            }
            else if (!IsPlayerNearby())
            {
                IsNotChasing();
                isChasing = false;
            }
        }
    }
    public void IsChasing()
    {
        navMeshAgent.speed = orcChaseSpeed;
        chaseTimer = 0.0f;
        AlertNearbyAI();

        navMeshAgent.destination = player.position;

        //If something changes, like the chase take to long then we stop
        if (isChasing && chaseTimer >= chaseDuration)
        {
            IsNotChasing();
            isChasing = false;
        }
    }

    public void IsNotChasing()
    {
        navMeshAgent.speed = orcWalkSpeed;
        isChasing = false;
        isAlerted = false;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            SetNextPatrolPoint();
        }
    }
    
    private void ShufflePatrolPoints()
    {
        // Fisher-Yates shuffle algorithm
        for (int i = patrolPoints.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = patrolPoints[i];
            patrolPoints[i] = patrolPoints[j];
            patrolPoints[j] = temp;
        }
    }

    private void SetNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
    }

    public bool IsPlayerNearby()
    {
        if (player == null)
            return false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //This returns true
        return distanceToPlayer <= playerDetectionRadius;
    }

    private void AlertNearbyAI()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, alarmRadius);

        foreach (Collider collider in colliders)
        {
            Orc_Patrol nearbyAI = collider.GetComponent<Orc_Patrol>();

            if (nearbyAI != null && nearbyAI != this)
            {
                nearbyAI.OnAlert(transform.position);
            }
        }
    }

    public void OnAlert(Vector3 alertPosition)
    {
        if (!isAlerted)
        {
            isAlerted = true;
            navMeshAgent.destination = alertPosition;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the player detection radius
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

        // Draw the alarm radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alarmRadius);
    }
}
