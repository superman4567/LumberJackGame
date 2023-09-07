using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc_Health : MonoBehaviour
{
    [SerializeField] Orc_Ragdoll orc_Ragdoll;
    [SerializeField] Orc_Animations orc_Animations;
    [SerializeField] Orc_Attack orc_Attack;
    [SerializeField] Collider hitBox; // Change this to a trigger collider

    [SerializeField] private float knockbackForce = 10.0f;
    [SerializeField] private float knockbackDuration = 0.5f;
    [SerializeField] private float knockbackSpeed = 5.0f;

    [SerializeField] Animator animator;
    private RoundManager roundManager;
    private AxeDetection axeDetection;

    public float maxHealth = 100;
    private float currentHealth;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    public bool isKnockbackActive = false;
    private Vector3 knockbackDirection;

    private void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        axeDetection = FindObjectOfType<AxeDetection>();
    }

    private void Start()
    {
        Statemachine();
        currentHealth = maxHealth;
    }

    public void KnockBack(Vector3 direction)
    {
        isKnockbackActive = true;
        navMeshAgent.enabled = false;
        knockbackDirection = direction.normalized * knockbackForce;
        Invoke("EndKnockback", knockbackDuration);
    }

    private void EndKnockback()
    {
        isKnockbackActive = false;
        navMeshAgent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Axe")) // Use CompareTag for tag comparison
        {
            currentHealth -= axeDetection.axeDamage;

            // Health is 0
            if (currentHealth <= 0)
            {
                animator.enabled = false;
                orc_Animations.enabled = false;
                orc_Attack.enabled = false;
                orc_Attack.navMeshAgent.enabled = false;
                hitBox.enabled = false;

                orc_Ragdoll.EnableRagdoll();
                roundManager.OrcKilled();
                Invoke("Die", 10f);
            }
        }
    }

    private void Statemachine()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                maxHealth = (maxHealth + (roundManager.currentRound * 1.25f));
                break;

            case 1:
                maxHealth = (maxHealth + (roundManager.currentRound * 1.5f));
                break;

            case 2:
                maxHealth = (maxHealth + (roundManager.currentRound * 2f));
                break;

            default:
                break;
        }
    }

    private void Die()
    {
        foreach (var trigger in orc_Attack.handScripts)
        {
            trigger.enabled = false;
        }
        Destroy(gameObject);
    }
}
