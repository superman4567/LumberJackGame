using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Orc_Attack : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] private float meleeAttackRange = 3.0f;
    [SerializeField] private float meleeAttackDuration = 1.0f; 
    [SerializeField] private float throwCooldown = 4.0f;
    [SerializeField] private float throwForce = 15f;
    [SerializeField] private float minDistanceToPlayer = 0.75f;

    [Header("Orc speed")]
    [SerializeField] private float baseOrcSpeed = 1;
    [SerializeField] private float chaseSpeed = 5;
    [SerializeField] private const float lastOrcSpeed = 6;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Orc_DealDamage[] orcDealDamage;
    [SerializeField] private Orc_Health orcHealth;
    [SerializeField] private GameObject rockPrefab; 
    [SerializeField] private Transform throwSpawnPoint;

    public NavMeshAgent navMeshAgent;
    public static bool IsChasingTotem = false;
    public static bool attackingTotem = false;
    
    private RoundManager roundManager;
    private ObjectPool rockPool;
    private CharacterController playerController;
    private float throwCooldownTimer = 0.0f;
    private Transform player;
    private float distanceToPlayer;

    private enum OrcState
    {
        MeleeAttack,
        ThrowRock
    }

    private OrcState currentState = OrcState.MeleeAttack;
    private float meleeAttackTimer = 0f;

    private void Awake()
    {
        StatemachineOrcDamage();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<CharacterController>();
        rockPool = FindObjectOfType<ObjectPool>();
        roundManager = FindObjectOfType<RoundManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.SetDestination(player.position);
        Disablehands();
    }

    private void Update()
    {
        MeleeOrRangedAttack();
        if (IsChasingTotem) { return; }
        ChasePlayer();
    }

    private void StatemachineOrcDamage()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                for (int i = 0; i < orcDealDamage.Length; i++)
                {
                    orcDealDamage[i].damageAmount = 25;
                }
                break;

            case 1:
                for (int i = 0; i < orcDealDamage.Length; i++)
                {
                    orcDealDamage[i].damageAmount = 34;
                }
                break;

            case 2:
                for (int i = 0; i < orcDealDamage.Length; i++)
                {
                    orcDealDamage[i].damageAmount = 50;
                }
                break;

            default:
                break;
        }
    }

    private void MeleeOrRangedAttack()
    {
        if (orcHealth.orcIsDeath == true || orcHealth.isKnockbackActive) { return; }

        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case OrcState.MeleeAttack:
                if (distanceToPlayer < meleeAttackRange || attackingTotem)
                {
                    MeleeAttack();
                    navMeshAgent.speed = baseOrcSpeed + 5 + GameManager.Instance.GetDifficulty();
                    animator.SetBool("OrcMeleeAttack", true);
                    meleeAttackTimer += Time.deltaTime;

                    if (meleeAttackTimer >= 5f) // Change to the desired duration
                    {
                        currentState = OrcState.ThrowRock;
                        meleeAttackTimer = 0f; // Reset the timer
                    }
                }
                break;

            case OrcState.ThrowRock:
                if (distanceToPlayer < (meleeAttackRange + 7) || attackingTotem)
                {
                    navMeshAgent.speed = baseOrcSpeed + 3 + GameManager.Instance.GetDifficulty();

                    throwCooldownTimer += Time.deltaTime;
                    if (throwCooldownTimer >= throwCooldown)
                    {
                        animator.SetBool("OrcRangedAttack", true);
                    }
                }
                break;
        }
    }

    private void MeleeAttack()
    {
        StartCoroutine(MeleeAttackCoroutine());
    }

    private IEnumerator MeleeAttackCoroutine()
    {
        orcDealDamage[0].canDamage = true;
        orcDealDamage[1].canDamage = true;
        yield return new WaitForSeconds(meleeAttackDuration);
        animator.SetBool("OrcMeleeAttack", false);
    }

    public void ThrowAttack()
    {
        GameObject rock = rockPool.GetObject();

        if (rock != null)
        {
            rock.transform.position = throwSpawnPoint.position;

            Vector3 playerVelocity = playerController.velocity;
            Vector3 anticipatedPlayerPosition = player.position + (playerVelocity * (Vector3.Distance(throwSpawnPoint.position, player.position) / 10f));

            Vector3 throwDirection = (anticipatedPlayerPosition - throwSpawnPoint.position).normalized;
            rock.GetComponent<Rigidbody>().velocity = Vector3.zero;
            rock.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse); // Use the serialized throwForce

            throwCooldownTimer = throwCooldown;
            animator.SetBool("OrcRangedAttack", false);
        }
    }

    public void Disablehands()
    {
        orcDealDamage[0].canDamage = false;
        orcDealDamage[1].canDamage = false;
    }

    private void ChasePlayer()
    {
        if (!navMeshAgent.isActiveAndEnabled) { return; }

        navMeshAgent.SetDestination(player.position);
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > 100)
        {
            navMeshAgent.speed = chaseSpeed + GameManager.Instance.GetDifficulty() + 5;
        }

        else
        {
            navMeshAgent.speed = chaseSpeed + GameManager.Instance.GetDifficulty();
        }

        if (roundManager.orcsSpawnedInCurrentRound - roundManager.orcsKilledInCurrentRound == 1)
        {
            navMeshAgent.speed = lastOrcSpeed;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);
    }
}
