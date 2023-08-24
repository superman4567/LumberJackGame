using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Playerstats UI")]
    [SerializeField] private TextMeshProUGUI healthUIAmount;
    [SerializeField] private TextMeshProUGUI staminaUIAmount;
    [SerializeField] private GameObject deathUI;

    [Header("Health ")]
    [SerializeField] private float initialHealth = 100f;
    [SerializeField] private float health;

    [Header("Stamina ")]
    [SerializeField] private float initialStamina = 100f;
    [SerializeField] private float stamina;

    private EnvironmentManager environmentControls;
    private float startTime;
    private float baseDecreaseRate = 0.1f;  // Adjust this value to control the initial decrease rate
    private float increaseFactor = 1.1f;    // Adjust this value to control how the rate increases

    private bool damageByStorm = false;
    private bool playerIsOutside = false;

    private void Awake()
    {
        environmentControls = FindObjectOfType<EnvironmentManager>();
        if (environmentControls != null)
        {
            playerIsOutside = true;
        }
    }

    void Start()
    {
        startTime = Time.time;

        health = initialHealth;
        stamina = initialStamina;

        healthUIAmount.text = health.ToString();
        staminaUIAmount.text = stamina.ToString();

        deathUI.SetActive(false);
    }

    void Update()
    {
        ReduceStaminaByStorm();
    }
    

    private void ReduceStaminaByStorm()
    {
        if(playerIsOutside)
        {
            float currentTime = Time.time;
            float elapsedTime = currentTime - startTime;

            float decreaseRate = baseDecreaseRate * Mathf.Pow(increaseFactor, elapsedTime);

            stamina -= decreaseRate * Time.deltaTime;
            stamina = Mathf.Max(health, 0f);

            int intStamina = Mathf.RoundToInt(stamina);
            staminaUIAmount.text = intStamina.ToString();

            if (stamina <= 0)
            {
                TakeDamage(1);
            }
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

    private void PlayerDeath()
    {
        deathUI.SetActive(true);
        Invoke("FreezeTime", 1f);
    }

    public void FreezeTime()
    {
        Time.timeScale = 0.05f;
    }
}
