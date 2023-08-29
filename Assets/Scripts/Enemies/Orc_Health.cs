using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Health : MonoBehaviour
{
    [SerializeField] Orc_Ragdoll orc_Ragdoll;
    [SerializeField] Orc_Animations orc_Animations;
    [SerializeField] Orc_Attack orc_Attack;
    [SerializeField] Collider hitBox;
    private RoundManager roundManager;

    [SerializeField] Animator animator;
    public float maxHealth = 100;
    private float currentHealth;

    private void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Axe"))
        {
           TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth -= 50;

        if (currentHealth <= 0)
        {
            animator.enabled= false;
            orc_Animations.enabled = false;
            orc_Attack.enabled= false;
            orc_Attack.navMeshAgent.enabled = false;
            hitBox.enabled = false;

            orc_Ragdoll.EnableRagdoll();
            roundManager.OrcKilled();
            Invoke("Die", 10f);
        }

        Debug.Log(currentHealth);
    }

    private void Statemachine()
    {
        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                maxHealth += (maxHealth + (roundManager.currentRound * 2f));
                break;

            case 1:
                maxHealth += (maxHealth + (roundManager.currentRound * 7.5f));
                break;

            case 2:
                maxHealth += (maxHealth + (roundManager.currentRound * 15f));
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
