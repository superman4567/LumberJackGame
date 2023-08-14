using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Health : MonoBehaviour
{
    [SerializeField] Orc_Ragdoll orc_Ragdoll;
    [SerializeField] Orc_Patrol orc_Patrol;
    [SerializeField] Orc_Animations orc_Animations;
    [SerializeField] Orc_Attack orc_Attack;


    [SerializeField] Animator animator;
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Debug.Log(currentHealth);
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
            orc_Patrol.enabled= false;
            orc_Animations.enabled = false;
            orc_Attack.enabled= false;

            orc_Ragdoll.EnableRagdoll();
            Invoke("Die", 10f);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
