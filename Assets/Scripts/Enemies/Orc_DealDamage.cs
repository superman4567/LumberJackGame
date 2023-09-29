using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_DealDamage : MonoBehaviour
{
    [SerializeField] public float damageAmount = 10f;
    [SerializeField] private LayerMask playerLayer; // Assign the "Player" layer in the Inspector
    public bool canDamage = true;

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayerInLayer(other) && canDamage)
        {
            PlayerStats playerStats = other.GetComponentInChildren<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damageAmount);
            }
        }
        else if (other.tag == "Totem")
        {
            Debug.Log("Hitting the totem");
            HealingTotem healingTotem = other.GetComponent<HealingTotem>();
            if (healingTotem != null)
            {
                healingTotem.TakeDamage(damageAmount);
            }
        }
        else
        {
            return;
        }
    }

    // Check if the collider's GameObject is in the "Player" layer
    private bool IsPlayerInLayer(Collider collider)
    {
        return (playerLayer.value & 1 << collider.gameObject.layer) != 0;
    }
}
