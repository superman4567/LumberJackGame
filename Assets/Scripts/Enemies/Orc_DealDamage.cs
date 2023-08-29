using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_DealDamage : MonoBehaviour
{
    [SerializeField] public float damageAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponentInChildren<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damageAmount);
            }
        }
    }
}
