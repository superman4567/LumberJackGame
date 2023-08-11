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

    public bool isAlerted = false;
    public bool isChasing = false; 
    private float chaseTimer = 0.0f;
    private int currentPatrolIndex = 0;
    
    private void Start()
    {
        ShufflePatrolPoints();
        SetNextPatrolPoint();
        navMeshAgent.speed = 2f;
    }

    private void Update()
    {
        if (player != null)
        {
            if (IsPlayerNearby() || isAlerted)
            {
                isChasing = true;
                navMeshAgent.speed = 5f;
                chaseTimer = 0.0f;
                CheckIfChasing();
                AlertNearbyAI();
            }
            else if (!IsPlayerNearby())
            {
                isChasing = false;
                navMeshAgent.speed = 2f;
            }
        }
    }

    public void CheckIfChasing()
    {
        if (isChasing && chaseTimer >= chaseDuration)
        {
            isChasing = false;
            isAlerted = false;
            SetNextPatrolPoint();
        }
        else
        {
            chaseTimer += Time.deltaTime;
            navMeshAgent.destination = player.position;
        }

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
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

        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
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
