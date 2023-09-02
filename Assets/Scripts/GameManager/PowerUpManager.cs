using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    [SerializeField] private Animator PowerupPanelanimator;

    [SerializeField] private GameObject powerUpPanel;
    [SerializeField] private Image powerUpIcon;
    [SerializeField] private TextMeshProUGUI powerUpTitle;
    [SerializeField] private TextMeshProUGUI powerUpDescription;
    [SerializeField] private float showTime = 2.5f;

    [SerializeField] private PowerupSO[] powerUps;
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
    }

    private void Start()
    {
        PopulatePowerUpOptions();
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
                Debug.Log("Sprint boost");
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[0]);

                playerMovement.movSpeed += 0.5f;
                playerMovement.sprintSpeed += 0.5f;

                Debug.Log("players movementspeed is: " + playerMovement.movSpeed);
                Debug.Log("player sprint speed is: " + playerMovement.sprintSpeed);
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.HealthBoost:
                Debug.Log("Health boost");
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[1]);

                playerStats.health += 10f;
                
                Debug.Log("players health is now: " + playerStats.health);
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.StaminaBoost:
                Debug.Log("Stamina boost");
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[2]);

                playerStats.stamina += 8f;
                Debug.Log("players stamina is now: " + playerStats.stamina);
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.DamageBoost:
                Debug.Log("Damage boost");
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[3]);

                playerThrowAxe.axeDamage += 25;
                Debug.Log("players axe does: " + playerThrowAxe.axeDamage + " damage.");
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.WoodBoost:
                Debug.Log("Wood boost");
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[4]);

                GameManager.Instance.woodMultiplier += 1;
                Debug.Log("Wood that you collect are increased: " + GameManager.Instance.woodMultiplier);
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.CoinBoost:
                Debug.Log("Resource boost");
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[5]);

                GameManager.Instance.coinMultiplier += 1;
                Debug.Log("Resource that you collect are increased: " + GameManager.Instance.coinMultiplier);
                CloseAbilityPanel(showTime);
                break;

            case PowerUpType.RageMode:
                Debug.Log("RageMode");
                OpenAbilityPanel();
                ApplyImageAndText(powerUps[6]);

                StartCoroutine(ApplyRageModeEffect(10f)); 
                break;
        }
    }

    private IEnumerator ApplyRageModeEffect(float duration)
    {
        float timeRemaining = duration;

        while (timeRemaining > 0f)
        {
            playerMovement.movSpeed += 0.5f * Time.deltaTime;
            playerMovement.sprintSpeed += 0.5f * Time.deltaTime;
            playerThrowAxe.axeDamage += 9999;

            if (playerStats.health != playerStats.maxHealth)
            {
                playerStats.health += 2f * Time.deltaTime;
            }

            if (playerStats.stamina != playerStats.maxstamina)
            {
                playerStats.stamina += 1f * Time.deltaTime;
            }

            timeRemaining -= Time.deltaTime;
            yield return null; 
        }

        playerMovement.movSpeed -= 0.5f;
        playerMovement.sprintSpeed -= 0.5f;
        playerThrowAxe.axeDamage -= 9999;
        CloseAbilityPanel(showTime);
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
