using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }
    public enum PowerUpType
    {
        SprintBoost,
        DodgeCoolDownReducation,
        StaminaBoost,
        DamageBoost,
        ResourceBoost,
    }

    private PowerUpType[] powerUpOptions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Populate powerUpOptions array
            PopulatePowerUpOptions();
        }
        else
        {
            Destroy(gameObject);
        }
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
            case PowerUpType.SprintBoost:
                Debug.Log("Sprint boost");
                break;

            case PowerUpType.DodgeCoolDownReducation:
                Debug.Log("Health boost");
                break;

            case PowerUpType.StaminaBoost:
                Debug.Log("Sprint boost");
                break;

            case PowerUpType.DamageBoost:
                Debug.Log("Damage boost");
                break;

            case PowerUpType.ResourceBoost:
                Debug.Log("Resource boost");
                break;
        }
    }
}
