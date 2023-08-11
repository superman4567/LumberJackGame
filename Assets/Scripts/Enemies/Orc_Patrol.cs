using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc_Patrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Define the patrol points in the Inspector
    public string playerTag = "Player"; // Tag of the player GameObject
    public float playerDetectionRadius = 5.0f; // Distance at which the AI detects the player
    public float alarmRadius = 10.0f; // Distance at which the AI alerts nearby AI
    private NavMeshAgent navMeshAgent;

    private int currentPatrolIndex = 0;
    private Transform player;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        if (player == null)
        {
            Debug.LogWarning("Player with tag '" + playerTag + "' not found in the scene.");
        }

        SetNextPatrolPoint();
    }

    private void Update()
    {
        if (player != null)
        {
            if (IsPlayerNearby())
            {
                navMeshAgent.destination = player.position;

                // Alert nearby AI if the player is detected
                AlertNearbyAI();
            }
            else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                SetNextPatrolPoint();
            }
        }
    }

    private void SetNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private bool IsPlayerNearby()
    {
        if (player == null)
            return false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
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

    private void OnDrawGizmosSelected()
    {
        // Draw the player detection radius
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

        // Draw the alarm radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alarmRadius);
    }

    public void OnAlert(Vector3 alertPosition)
    {
        // You can implement the behavior for the alerted AI here
        // For example, set the destination to the alert position
        navMeshAgent.destination = alertPosition;
    }
}
