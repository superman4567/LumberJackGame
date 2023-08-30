using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Orc_Attack : MonoBehaviour
{
    
    [SerializeField] private float meleeAttackRange = 3.0f;
    [SerializeField] private float rangedAttackRange = 10.0f;
    [SerializeField] private float meleeAttackDuration = 1.0f; 
    [SerializeField] private float throwCooldown = 4.0f;
    [SerializeField] private float baseOrcSpeed = 2;
    [SerializeField] private const float lastOrcSpeed = 6;

    [SerializeField] private Animator animator;

    [SerializeField] private Orc_DealDamage[] orcDealDamage;
    [SerializeField] private Orc_Health orcHealth;
    [SerializeField] private GameObject rockPrefab; 
    [SerializeField] private Transform throwSpawnPoint; 
    [SerializeField] private LayerMask attackLayer;
    private RoundManager roundManager;

    private Transform player;
    public NavMeshAgent navMeshAgent;
    private float throwCooldownTimer = 0.0f;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        orcDealDamage = GetComponentsInChildren<Orc_DealDamage>();
        roundManager = FindObjectOfType<RoundManager>();
    }

    private void Update()
    {
        Statemachine();
        ChasePlayer();
        MeleeOrRangedAttack();
    }

    private void Statemachine()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                for (int i = 0; i < orcDealDamage.Length; i++)
                {
                    orcDealDamage[i].damageAmount = 25;
                }
                baseOrcSpeed = Random.Range(1.8f, 2.2f);

                roundManager.orcsToSpawnInCurrentRound = 10;
                roundManager.orcSpawnIncreasePercentage = 2f;
                break;

            case 1:
                for (int i = 0; i < orcDealDamage.Length; i++)
                {
                    orcDealDamage[i].damageAmount = 34;
                }
                baseOrcSpeed = Random.Range(2.8f, 3.8f);

                roundManager.orcsToSpawnInCurrentRound = 20;
                roundManager.orcSpawnIncreasePercentage = 3f;
                break;

            case 2:
                for (int i = 0; i < orcDealDamage.Length; i++)
                {
                    orcDealDamage[i].damageAmount = 50;
                }
                baseOrcSpeed = Random.Range(5f, 7f);

                roundManager.orcsToSpawnInCurrentRound = 30;
                roundManager.orcSpawnIncreasePercentage = 4f;
                break;

            default:
                break;
        }
    }

    private void ChasePlayer()
    {
        navMeshAgent.speed = baseOrcSpeed;
        navMeshAgent.SetDestination(player.position);

        if (roundManager.orcsSpawnedInCurrentRound - roundManager.orcsKilledInCurrentRound == 1)
        {
            navMeshAgent.speed = lastOrcSpeed;
        }
    }

    private void MeleeOrRangedAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= rangedAttackRange)
        {
            //Making sure the or does not rapid fire the rocks
            throwCooldownTimer += Time.deltaTime;
            if (throwCooldownTimer >= throwCooldown)
            {
                animator.SetBool("OrcRangedAttack", true);
            }
        }
        else if (distanceToPlayer < meleeAttackRange)
        {
            MeleeAttack();
            animator.SetBool("OrcMeleeAttack", true);
        }
    }

    private void MeleeAttack()
    {
        StartCoroutine(MeleeAttackCoroutine());
    }

    private IEnumerator MeleeAttackCoroutine()
    {
        animator.SetBool("OrcMeleeAttack", true);
        yield return new WaitForSeconds(meleeAttackDuration);
        animator.SetBool("OrcMeleeAttack", false);
    }

    public void ThrowAttack()
    {
        GameObject rock = Instantiate(rockPrefab, throwSpawnPoint.position, Quaternion.identity);
        Vector3 throwDirection = (player.position - throwSpawnPoint.position).normalized;
        rock.GetComponent<Rigidbody>().AddForce(throwDirection * 10f, ForceMode.Impulse);

        throwCooldownTimer = throwCooldown;
        animator.SetBool("OrcRangedAttack", false);
        animator.SetBool("OrcMeleeAttack", false); // Add this line
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangedAttackRange);
    }
}
