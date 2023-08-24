using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc_Attack : MonoBehaviour
{
    
    [SerializeField] private float meleeAttackRange = 3.0f;
    [SerializeField] private float rangedAttackRange = 10.0f;
    [SerializeField] private float meleeAttackDuration = 1.0f; 
    [SerializeField] private float throwCooldown = 4.0f;
    [SerializeField] private float baseOrcSpeed = 2;
    
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject rockPrefab; 
    [SerializeField] private Transform throwSpawnPoint; 
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private RoundManager roundManager;

    private Transform player;
    public NavMeshAgent navMeshAgent;
    private float throwCooldownTimer = 0.0f;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        roundManager = FindObjectOfType<RoundManager>();
    }

    private void Update()
    {
        ChasePlayer();
        MeleeOrRangedAttack();
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);

        // Get the orc's base speed
        float baseSpeed = navMeshAgent.speed;

        // Add a small random value to the speed to create variation
        float randomSpeedOffset = Random.Range(-0.1f, 0.1f); // Adjust the range as needed
        float orcSpeed = baseSpeed + randomSpeedOffset;

        // Set the orc's speed
        navMeshAgent.speed = orcSpeed;
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
