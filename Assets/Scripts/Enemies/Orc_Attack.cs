using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Orc_Attack : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] private float meleeAttackRange = 3.0f;
    [SerializeField] private float rangedAttackRange = 10.0f;
    [SerializeField] private float meleeAttackDuration = 1.0f; 
    [SerializeField] private float throwCooldown = 4.0f;
    [SerializeField] private float throwForce = 15f;

    [Header("Orc speed")]
    [SerializeField] private float baseOrcSpeed = 2;
    [SerializeField] private const float lastOrcSpeed = 6;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Orc_DealDamage[] orcDealDamage;
    [SerializeField] private Orc_Health orcHealth;
    [SerializeField] private GameObject rockPrefab; 
    [SerializeField] private Transform throwSpawnPoint; 
    [SerializeField] private LayerMask attackLayer;
    
    private RoundManager roundManager;
    private ObjectPool rockPool;
    private CharacterController playerController;
    private Transform player;
    public NavMeshAgent navMeshAgent;
    private float throwCooldownTimer = 0.0f;
    private float distanceToPlayer;
    private bool startThrowing = false;


    private void Awake()
    {
        Statemachine();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<CharacterController>();
        rockPool = FindObjectOfType<ObjectPool>();
        roundManager = FindObjectOfType<RoundManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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
                baseOrcSpeed = Random.Range(2.5f, 3f);
                break;

            case 1:
                for (int i = 0; i < orcDealDamage.Length; i++)
                {
                    orcDealDamage[i].damageAmount = 34;
                }
                baseOrcSpeed = Random.Range(3f, 3.5f);
                break;

            case 2:
                for (int i = 0; i < orcDealDamage.Length; i++)
                {
                    orcDealDamage[i].damageAmount = 50;
                }
                baseOrcSpeed = Random.Range(3.5f, 4f);
                break;

            default:
                break;
        }
    }

    private void MeleeOrRangedAttack()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        startThrowing = (distanceToPlayer < rangedAttackRange) ? true : false;

        if (distanceToPlayer < meleeAttackRange)
        {
            MeleeAttack();
            animator.SetBool("OrcMeleeAttack", true);
        }
        else if (distanceToPlayer > meleeAttackRange && startThrowing == true)
        {
            throwCooldownTimer += Time.deltaTime;
            if (throwCooldownTimer >= throwCooldown)
            {
                animator.SetBool("OrcRangedAttack", true);
            }
        }
        else
        {
            ChasePlayer();
        }
    }

    private void MeleeAttack()
    {
        StartCoroutine(MeleeAttackCoroutine());
        orcDealDamage[0].canDamage = true;
        orcDealDamage[1].canDamage = true;
    }

    private IEnumerator MeleeAttackCoroutine()
    {
        yield return new WaitForSeconds(meleeAttackDuration);
        animator.SetBool("OrcMeleeAttack", false);
        Disablehands();
    }

    public void Disablehands()
    {
        orcDealDamage[0].canDamage = false;
        orcDealDamage[1].canDamage = false;
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

    private void ChasePlayer()
    {
        navMeshAgent.speed = baseOrcSpeed;

        if (!navMeshAgent.isActiveAndEnabled) { return; }
        navMeshAgent.SetDestination(player.position);

        if (distanceToPlayer <= rangedAttackRange)
        {
            navMeshAgent.speed = baseOrcSpeed += 1.5f;
        }

        if (distanceToPlayer <= meleeAttackRange)
        {
            navMeshAgent.speed = baseOrcSpeed += 2.5f;
        }

        if (roundManager.orcsSpawnedInCurrentRound - roundManager.orcsKilledInCurrentRound == 1)
        {
            navMeshAgent.speed = baseOrcSpeed += (lastOrcSpeed / 2);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangedAttackRange);
    }
}
