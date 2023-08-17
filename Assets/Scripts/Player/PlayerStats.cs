using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Health ")]
    [SerializeField] private float  maxHealth = 100f;
    [SerializeField] private float initialHealth = 100f;
    [SerializeField] private float health;
    
    private EnvironmentManager environmentControls;
    private float startTime;
    private float baseDecreaseRate = 0.1f;  // Adjust this value to control the initial decrease rate
    private float increaseFactor = 1.1f;    // Adjust this value to control how the rate increases

    private bool damageByStorm = false;
    private bool playerIsOutside = false;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 50;
    [SerializeField] private float initialStamina = 50f;
    [SerializeField] private float stamina;
    

    [Header("Playerstats UI")]
    [SerializeField] private TextMeshProUGUI healthUIAmount;
    [SerializeField] private TextMeshProUGUI staminaUIAmount;

    private void Awake()
    {
        environmentControls = FindObjectOfType<EnvironmentManager>();
        if (environmentControls != null)
        {
            playerIsOutside = true;
        }
    }

    private void OnEnable()
    {
        if (environmentControls != null)
        {
            environmentControls.stormStart += IsStormStarting;
        }
    }

    private void OnDisable()
    {
        if (environmentControls != null)
        {
            environmentControls.stormStart -= IsStormStarting;
        }
    }

    void Start()
    {
        health = initialHealth;
        stamina = initialStamina;

        startTime = Time.time;

        healthUIAmount.text = health.ToString();
        staminaUIAmount.text = stamina.ToString();
    }

    void Update()
    {
        DamageByStorm();
    }

    private void IsStormStarting()
    {
        damageByStorm = true;
    }

    private void DamageByStorm()
    {
        if(damageByStorm)
        {
            float currentTime = Time.time;
            float elapsedTime = currentTime - startTime;

            float decreaseRate = baseDecreaseRate * Mathf.Pow(increaseFactor, elapsedTime);

            health -= decreaseRate * Time.deltaTime;
            health = Mathf.Max(health, 0f);

            int intHealth = Mathf.RoundToInt(health);
            healthUIAmount.text = intHealth.ToString();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0f);

        int intHealth = Mathf.RoundToInt(health);
        healthUIAmount.text = intHealth.ToString();

        if (health <= 0)
        {
            PlayerDeath();
        }
    }

    private void StaminaManagment()
    {
        
    }

    private void PlayerDeath()
    {

    }
}
