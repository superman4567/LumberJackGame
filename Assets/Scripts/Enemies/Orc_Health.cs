using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Orc_Health : MonoBehaviour
{
    [SerializeField] Orc_Ragdoll orc_Ragdoll;
    [SerializeField] Orc_Animations orc_Animations;
    [SerializeField] Orc_Attack orc_Attack;
    [SerializeField] Collider hitBox;
    public RoundManager roundManager;


    [SerializeField] Animator animator;
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        roundManager = FindObjectOfType<RoundManager>();
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Axe"))
        {
           Debug.Log("I am hit");
           TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth -= 50;
        Debug.Log(currentHealth);

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
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
