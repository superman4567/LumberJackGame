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
    private RockPool rockPool;
    private CharacterController playerController;
    private Transform player;
    public NavMeshAgent navMeshAgent;
    private float throwCooldownTimer = 0.0f;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<CharacterController>();
        rockPool = FindObjectOfType<RockPool>();
        roundManager = FindObjectOfType<RoundManager>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        orcDealDamage = GetComponentsInChildren<Orc_DealDamage>();
        
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

        if (distanceToPlayer < meleeAttackRange)
        {
            MeleeAttack();
            animator.SetBool("OrcMeleeAttack", true);
        }

        if (distanceToPlayer < rangedAttackRange)
        {
            //Making sure the or does not rapid fire the rocks
            throwCooldownTimer += Time.deltaTime;
            if (throwCooldownTimer >= throwCooldown)
            {
                animator.SetBool("OrcRangedAttack", true);
            }
        }
    }

    private void MeleeAttack()
    {
        StartCoroutine(MeleeAttackCoroutine());
    }

    private IEnumerator MeleeAttackCoroutine()
    {
        yield return new WaitForSeconds(meleeAttackDuration);
        animator.SetBool("OrcMeleeAttack", false);
    }

    public void ThrowAttack()
    {
        GameObject rock = rockPool.GetRock();

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangedAttackRange);
    }
}
