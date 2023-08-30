using System;
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
    [SerializeField] Rigidbody rb;

    [SerializeField] private float knockbackDuration = 0.5f;
    [SerializeField] private float knockbackSpeed = 5.0f;

    [SerializeField] Animator animator;
    private RoundManager roundManager;
    public float maxHealth = 100;
    private float currentHealth;

    private void Awake()
    {
        rb.isKinematic = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Axe") { return; }

        currentHealth -= 50;
        Debug.Log(currentHealth);

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

    public void KnockBack(Vector3 knockbackDirection, float knockbackForce)
    {
        orc_Attack.navMeshAgent.enabled = false;
        Vector3 knockbackVelocity = -knockbackDirection.normalized * knockbackSpeed;

        // Simulate knockback using translations
        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            transform.Translate(knockbackVelocity * Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }

        orc_Attack.navMeshAgent.enabled = true;
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
