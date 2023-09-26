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

    [Header("Health ")]
    [SerializeField] private float health;
    public float Health { get; private set; }
    [SerializeField] public float maxHealth = 100f;

    [Header("Stamina ")]
    [SerializeField] private float stamina;
    public float reducer = 1f;
    public bool ult3 = false;

    public float Stamina { get; private set; }
    [SerializeField] public float maxStamina = 100f;

    [Header("Stamina ")]
    [SerializeField] Animator getHitpanel;
    [SerializeField] private PlayerCameraShake playerCameraShake;

    [Header("Collider ")]
    [SerializeField] private CharacterController hitbox;

    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;
    }

    void Update()
    {
        Health = health;
        Stamina = stamina;

        if (SceneManager.GetActiveScene().buildIndex == 1) { return; } 
        healthUIAmount.text = Mathf.RoundToInt(health).ToString();
        staminaUIAmount.text = Mathf.RoundToInt(stamina).ToString();
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
        if (ult3) { return; }
        playerCameraShake.ShakeCamera();
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
        if (health >= maxHealth) { return; }

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
        stamina += amount/reducer;
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

        if (health >= maxHealth)
        {
            getHitpanel.SetTrigger("GetHealedTrigger");
        }
        else { return; }
    }

    private void PlayerDeath()
    {
        RoundManager.Instance.ChnageDiffCompleteText(false);
        SteamAchievementManager.instance.UnlockAchievement("ACHIEVEMENT_Death");
        Invoke("FreezeTime", 10f);
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
