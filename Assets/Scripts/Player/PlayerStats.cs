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
    [SerializeField] public float health;

    [Header("Stamina ")]
    [SerializeField] private float initialStamina = 100f;
    [SerializeField] public float stamina;

    void Start()
    {
        health = initialHealth;
        stamina = initialStamina;

        deathUI.SetActive(false);

        StartCoroutine(CallMethodEverySecond());
    }

    void Update()
    {
        healthUIAmount.text = Mathf.RoundToInt(health).ToString();
        staminaUIAmount.text = Mathf.RoundToInt(stamina).ToString();
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

    private IEnumerator CallMethodEverySecond()
    {
        while (true)
        {
            AddStamina(.25f);
            yield return new WaitForSeconds(1f);
        }
    }

    public void AddStamina(float amount)
    {
        stamina += amount;
        stamina = Mathf.Min(stamina, initialStamina); 

        int intStamina = Mathf.RoundToInt(stamina);
        staminaUIAmount.text = intStamina.ToString();
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
