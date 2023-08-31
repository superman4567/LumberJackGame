using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc_Health : MonoBehaviour
{
    [SerializeField] Orc_Ragdoll orc_Ragdoll;
    [SerializeField] Orc_Animations orc_Animations;
    [SerializeField] Orc_Attack orc_Attack;
    [SerializeField] Collider hitBox;

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

        if (roundManager == null)
        {
            Debug.LogError("RoundManager not found or not initialized.");
        }
    }

    private void Update()
    {
        if (isKnockbackActive)
        {
            PerformKnockback();
        }
    }

    public void KnockBack(Vector3 direction)
    {
        isKnockbackActive = true;
        knockbackDirection = direction;
        navMeshAgent.enabled = false;

        Invoke("EndKnockback", knockbackDuration);
    }

    private void PerformKnockback()
    {
        Vector3 newPosition = transform.position + new Vector3(knockbackDirection.x, 0f, knockbackDirection.z) * knockbackSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void EndKnockback()
    {
        isKnockbackActive = false;
        navMeshAgent.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Axe") { return; }

        currentHealth -= axeDetection.axeDamage;

        //health is 0
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
        Destroy(gameObject);
    }
}
