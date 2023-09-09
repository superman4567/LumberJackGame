using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    [Header("PowerUps")]
    [SerializeField] private Animator PowerupPanelanimator;
    [SerializeField] private GameObject powerUpPanel;
    [SerializeField] private Image powerUpIcon;
    [SerializeField] private TextMeshProUGUI powerUpTitle;
    [SerializeField] private TextMeshProUGUI powerUpDescription;
    [SerializeField] private float showTime = 2.5f;
    [SerializeField] private PowerupSO[] powerUps;

    [Header("PowerUp UI update")]
    [SerializeField] private GameObject[] abilityStack;
    private int speedBoostsUnlocked = 1;
    private int healthBoostsUnlocked = 1;
    private int staminaBoostsUnlocked = 1;
    private int damageBoostsUnlocked = 1;
    private int woodBoostsUnlocked = 1;
    private int coinBoostsUnlocked = 1;
    private bool rageModesUnlocked = false;

    

    private float endTime;
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;
    private AxeDetection playerThrowAxe;

    public enum PowerUpType
    {
        SpeedBoost,
        HealthBoost,
        StaminaBoost,
        DamageBoost,
        WoodBoost,
        CoinBoost,
        RageMode
    }

    private PowerUpType[] powerUpOptions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerMovement = FindObjectOfType<PlayerMovement>();
        playerStats = FindObjectOfType<PlayerStats>();
        playerThrowAxe = FindObjectOfType<AxeDetection>();
        PopulatePowerUpOptions();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        foreach (var item in abilityStack)
        {
            item.SetActive(false);
        }
    }

    private void Update()
    {
        CountDownTimerVisible();
    }

    private void PopulatePowerUpOptions()
    {
        // Set powerUpOptions array based on the PowerUpType enum
        powerUpOptions = (PowerUpType[])System.Enum.GetValues(typeof(PowerUpType));
    }

    public void GrantRandomPowerUpToPlayer()
    {
        int randomIndex = Random.Range(0, powerUpOptions.Length);
        PowerUpType selectedPowerUpType = powerUpOptions[randomIndex];

        ApplyPowerUpEffect(selectedPowerUpType);
    }

    private void ApplyPowerUpEffect(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.SpeedBoost:
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[0]);

                AddGainedSprite(0);
                speedBoostsUnlocked++;

                playerMovement.movSpeed += 0.5f;
                playerMovement.sprintSpeed += 0.5f;

                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.HealthBoost:
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[1]);

                AddGainedSprite(1);
                healthBoostsUnlocked++;

                playerStats.AddHealth(10f);
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.StaminaBoost:
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[2]);

                AddGainedSprite(2);
                staminaBoostsUnlocked++;

                playerStats.AddStamina(8f);
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.DamageBoost:
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[3]);

                AddGainedSprite(3);
                damageBoostsUnlocked++;

                playerThrowAxe.axeDamage += 25;
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.WoodBoost:
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[4]);

                AddGainedSprite(4);
                woodBoostsUnlocked++;

                GameManager.Instance.IncreaseWoodMultiplier(2);
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.CoinBoost:
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[5]);

                AddGainedSprite(5);
                coinBoostsUnlocked++;

                GameManager.Instance.IncreaseCoindMultiplier(2);
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.RageMode:
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[6]);
                StartCoroutine(ApplyRageModeEffect(10f)); 
                break;
        }
    }

    private IEnumerator ApplyRageModeEffect(float duration)
    {
        AddGainedSprite(6);
        rageModesUnlocked = true;
        playerMovement.movSpeed += 1;
        playerMovement.sprintSpeed += 0.5f;
        playerThrowAxe.axeDamage += 9999;

        float timeRemaining = duration;

        while (timeRemaining > 0f)
        {
            if (playerStats.Health != playerStats.maxHealth)
            {
                playerStats.AddHealth(2f);
            }

            if (playerStats.Stamina != playerStats.maxstamina)
            {
                playerStats.AddStamina(1f);
            }

            timeRemaining -= Time.deltaTime;
            yield return null; 
        }
        AddGainedSprite(6);
        rageModesUnlocked = false;
        playerMovement.movSpeed -= 0.5f;
        playerMovement.sprintSpeed -= 0.5f;
        playerThrowAxe.axeDamage -= 9999;
        CloseAbilityPanel(showTime);
    }

    private void AddGainedSprite(int indexNumber)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        Debug.Log(indexNumber);
        switch (indexNumber)
        {
            case 0:
                if (speedBoostsUnlocked == 1)
                {
                    abilityStack[0].SetActive(true);
                    abilityStack[0].transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    abilityStack[0].transform.GetChild(0).gameObject.SetActive(true);
                    abilityStack[0].GetComponentInChildren<TextMeshProUGUI>().text = speedBoostsUnlocked.ToString();
                }
                break;

            case 1:
                if (healthBoostsUnlocked == 1)
                {
                    abilityStack[1].SetActive(true);
                    abilityStack[1].transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    abilityStack[1].transform.GetChild(0).gameObject.SetActive(true);
                    abilityStack[1].GetComponentInChildren<TextMeshProUGUI>().text = healthBoostsUnlocked.ToString();
                }
                break;

            case 2:
                if (staminaBoostsUnlocked == 1)
                {
                    abilityStack[2].SetActive(true);
                    abilityStack[2].transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    abilityStack[2].transform.GetChild(0).gameObject.SetActive(true);
                    abilityStack[2].GetComponentInChildren<TextMeshProUGUI>().text = staminaBoostsUnlocked.ToString();
                }
                break;

            case 3:
                if (damageBoostsUnlocked == 1)
                {
                    abilityStack[3].SetActive(true);
                    abilityStack[3].transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    abilityStack[3].transform.GetChild(0).gameObject.SetActive(true);
                    abilityStack[3].GetComponentInChildren<TextMeshProUGUI>().text = damageBoostsUnlocked.ToString();
                }
                break;

            case 4:
                if (woodBoostsUnlocked == 1)
                {
                    abilityStack[4].SetActive(true);
                    abilityStack[4].transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    abilityStack[4].transform.GetChild(0).gameObject.SetActive(true);
                    abilityStack[4].GetComponentInChildren<TextMeshProUGUI>().text = woodBoostsUnlocked.ToString();
                }
                break;

            case 5:
                if (coinBoostsUnlocked == 1)
                {
                    abilityStack[5].SetActive(true);
                    abilityStack[5].transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    abilityStack[5].transform.GetChild(0).gameObject.SetActive(true);
                    abilityStack[5].GetComponentInChildren<TextMeshProUGUI>().text = coinBoostsUnlocked.ToString();
                }
                break;

            case 6:
                if (rageModesUnlocked == true)
                {
                    abilityStack[6].SetActive(true);
                }
                else
                {
                    abilityStack[6].SetActive(false);
                }
                break;
        }
    }

    private void ApplyImageAndText(PowerupSO abilitySO)
    {
        powerUpIcon.sprite = abilitySO.PowerUpSrpite;
        powerUpTitle.text = abilitySO.PowerupTitle;
        powerUpDescription.text = abilitySO.PowerupDescription;
    }

    private void OpenAbilityPanel()
    {
        PowerupPanelanimator.SetBool("IsVisible", true);
    }

    private void CloseAbilityPanel(float duration)
    {
        endTime = duration;
        Time.timeScale = 1f;
    }

    private void CountDownTimerVisible()
    {
        endTime -= Time.deltaTime;
        if (endTime < 0)
        {
            PowerupPanelanimator.SetBool("IsVisible", false);
        }
    }
}
