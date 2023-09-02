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
    [SerializeField] public float maxHealth = 100f;

    [Header("Stamina ")]
    [SerializeField] private float initialStamina = 100f;
    [SerializeField] public float stamina;
    [SerializeField] public float maxstamina = 100f;

    [Header("Stamina ")]
    [SerializeField] Animator getHitpanel;

    [Header("Collider ")]
    [SerializeField] private CharacterController hitbox;

    void Start()
    {
        health = initialHealth;
        stamina = initialStamina;

        deathUI.SetActive(false);
    }

    void Update()
    {
        healthUIAmount.text = Mathf.RoundToInt(health).ToString();
        staminaUIAmount.text = Mathf.RoundToInt(stamina).ToString();
        HideHitCanvas();
        AddStamina(0.25f);
    }

    public void TakeDamage(float amount)
    {
        ShowHitCanvas();
        health -= amount;
        health = Mathf.Max(health, 0f);

        int intHealth = Mathf.RoundToInt(health);
        healthUIAmount.text = intHealth.ToString();

        if (health <= 0)
        {
            PlayerDeath();
        }
    }

    public void AddStamina(float amount)
    {
        stamina += amount * Time.deltaTime;
        stamina = Mathf.Min(stamina, initialStamina); 

        int intStamina = Mathf.RoundToInt(stamina);
        staminaUIAmount.text = intStamina.ToString();
    }

    private void ShowHitCanvas()
    {
        if (health == maxHealth)
        {
            getHitpanel.SetBool("GetHit", true);
        }
        else
        {
            getHitpanel.SetTrigger("GetHitTrigger");
        }
    }

    private void HideHitCanvas()
    {
        if (health != maxHealth) { return; }    
        getHitpanel.SetBool("GetHit", false);
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

    public void UnfreezeTime()
    {
        Time.timeScale = 1f;
    }

    public void IFrameOn()
    {
        gameObject.layer = LayerMask.NameToLayer("NoCollisionWithEnemies");
    }

    public void IFrameOff()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
