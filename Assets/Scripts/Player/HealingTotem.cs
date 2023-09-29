using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealingTotem : MonoBehaviour
{
    [SerializeField] private float ultDuration;
    private PlayerUltimates playerUltimates;
    private PlayerStats playerStats;
    private NavMeshAgent[] enemyAgents;
    private Transform totum;
    public float totemHealth = 500f;

    void Start()
    {
        totum = transform;
        playerStats = FindObjectOfType<PlayerStats>();
        playerUltimates = FindObjectOfType<PlayerUltimates>();
        ultDuration = playerUltimates.healingUltDuration;
        StartCoroutine(FindEnemies());
    }

    private IEnumerator FindEnemies()
    {
        Orc_Attack.IsChasingTotem = true;
        float totumAgroTime = 0f;

        while (totumAgroTime < ultDuration)
        {
            //Chase the totem
            enemyAgents = FindObjectsOfType<NavMeshAgent>();
            foreach (var agent in enemyAgents)
            {
                agent.SetDestination(totum.position);
            }
            Orc_Attack.attackingTotem = true;

            //heal the player
            playerStats.AddHealth(10f);

            totumAgroTime += 1f;
            yield return new WaitForSeconds(1f);
        }
        ChasePlayer();
    }

    public void TakeDamage(float damage)
    {
        totemHealth =- damage;
        Debug.Log(totemHealth);

        if (totemHealth <=0f )
        {
            Destroy(gameObject);
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Orc_Attack.attackingTotem = false;
        Orc_Attack.IsChasingTotem = false;
        Destroy(gameObject);
    }
}
