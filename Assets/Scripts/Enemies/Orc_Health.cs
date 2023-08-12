using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Health : MonoBehaviour
{
    
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
        Debug.Log("Orc took " + 50 + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Orc died.");
        // Implement death behavior here
        Destroy(gameObject);
    }
}
