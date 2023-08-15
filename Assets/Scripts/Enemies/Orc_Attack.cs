using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Attack : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float meleeAttackRange = 3.0f;
    [SerializeField] private float rangedAttackRange = 10.0f;
    [SerializeField] private float meleeAttackDuration = 1.0f; 
    [SerializeField] private float throwAttackDuration = 1.0f; 
    [SerializeField] private float throwCooldown = 4.0f;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject rockPrefab; 
    [SerializeField] private Transform throwSpawnPoint; 
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private Orc_Patrol orcPatrolScript;

    private bool isMeleeAttacking = false;
    private bool isThrowAttacking = false;
    private bool isChasingPlayer = false;
    private float throwCooldownTimer = 0.0f;

    private void Update()
    {
        if (player != null)
        {
            MeleeOrRangedAttack();
        }
    }

    private void MeleeOrRangedAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isMeleeAttacking && !isThrowAttacking)
        {
            if (distanceToPlayer <= meleeAttackRange)
            {
                MeleeAttack();
                animator.SetBool("OrcMeleeAttack", true);
            }
            else if (distanceToPlayer <= rangedAttackRange && orcPatrolScript.isChasing)
            {
                throwCooldownTimer += Time.deltaTime;
                if (throwCooldownTimer >= throwCooldown)
                {
                    ThrowAttack();
                    animator.SetBool("OrcRangedAttack", true);
                }
                else 
                {
                    isChasingPlayer = true;
                    orcPatrolScript.IsChasing();
                    animator.SetBool("OrcMeleeAttack", false);
                    animator.SetBool("OrcRangedAttack", false);
                }
            }
        }
    }

    private void MeleeAttack()
    {
        if (isMeleeAttacking) { return; }
        StartCoroutine(MeleeAttackCoroutine());
    }

    private void ThrowAttack()
    {
        if (isThrowAttacking) { return; }
        StartCoroutine(ThrowAttackCoroutine());
        throwCooldownTimer = throwCooldown;
    }

    private IEnumerator MeleeAttackCoroutine()
    {
        isMeleeAttacking = true;
        yield return new WaitForSeconds(meleeAttackDuration);
        isMeleeAttacking = false;
    }

    private IEnumerator ThrowAttackCoroutine()
    {
        isThrowAttacking = true;

        GameObject rock = Instantiate(rockPrefab, throwSpawnPoint.position, Quaternion.identity);
        Vector3 throwDirection = (player.position - throwSpawnPoint.position).normalized;
        rock.GetComponent<Rigidbody>().AddForce(throwDirection * 10f, ForceMode.Impulse);

        yield return new WaitForSeconds(throwAttackDuration);
        isThrowAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangedAttackRange);
    }
}
