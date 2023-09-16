using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDataPersistance
{
    [Header("Playerstats UI")]
    [SerializeField] private TextMeshProUGUI healthUIAmount;
    [SerializeField] private TextMeshProUGUI staminaUIAmount;
    [SerializeField] private GameObject deathUI;

    [Header("Health ")]
    [SerializeField] private float health;
    public float Health { get; private set; }
    [SerializeField] public float maxHealth = 100f;

    [Header("Stamina ")]
    [SerializeField] private float stamina;
    public float reducer = 0f;

    public float Stamina { get; private set; }
    [SerializeField] public float maxStamina = 100f;

    [Header("Stamina ")]
    [SerializeField] Animator getHitpanel;

    [Header("Collider ")]
    [SerializeField] private CharacterController hitbox;

    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;

        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        deathUI.SetActive(false);
    }

    void Update()
    {
        Health = health;
        Stamina = stamina;

        if (SceneManager.GetActiveScene().buildIndex == 1) { return; } 
        healthUIAmount.text = Mathf.RoundToInt(health).ToString();
        staminaUIAmount.text = Mathf.RoundToInt(stamina).ToString();
        AddStamina(0.25f * Time.deltaTime);
    }

    public void LoadData(GameData data)
    {
        this.maxHealth = data.maxHealth;
        this.maxStamina = data.maxStamina;
        this.reducer = data.reducer;
    }

    public void SaveData(GameData data)
    {
        data.maxHealth = this.maxHealth;
        data.maxStamina = this.maxStamina;
        data.reducer = this.reducer;
    }

    public void TakeDamage(float amount)
    {
        UpdateHitCanvasHIT();
        health -= amount;
        health = Mathf.Max(health, 0f);

        int intHealth = Mathf.RoundToInt(health);
        healthUIAmount.text = intHealth.ToString();

        if (health <= 0)
        {
            PlayerDeath();
        }
    }

    public void AddHealth(float amount)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        health += amount;

        UpdateHitCanvasHEALED();

        int intHealth = Mathf.RoundToInt(health);
        healthUIAmount.text = intHealth.ToString();
    }

    public void AddStamina(float amount)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        if (stamina >= maxStamina) { return; }
        stamina += amount;
        int intStamina = Mathf.RoundToInt(stamina);
        staminaUIAmount.text = intStamina.ToString();
    }

    public void SubstractHealth(float amount)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        health -= (amount / reducer);
        int intHealth = Mathf.RoundToInt(health);
        healthUIAmount.text = intHealth.ToString();
    }

    public void SubstractStamina(float amount)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        stamina -= amount;
        int intStamina = Mathf.RoundToInt(stamina);
        staminaUIAmount.text = intStamina.ToString();
    }

    private void UpdateHitCanvasHIT()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        getHitpanel.SetTrigger("GetHitTrigger");
    }

    private void UpdateHitCanvasHEALED()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }

        if (health == maxHealth)
        {
            getHitpanel.SetTrigger("GetHealedTrigger");
        }
        else { return; }
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
