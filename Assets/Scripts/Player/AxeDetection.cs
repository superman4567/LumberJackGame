using UnityEngine;

public class AxeDetection : MonoBehaviour, IDataPersistance
{
    public static AxeDetection Instance;

    [Header("Axe data")]
    public bool axeHitSomething = false;
    public int axeDamage = 20;

    [Header("Lifesteal")]
    public bool lifesteal = false;
    public float axeLifestealAmount;

    private PlayerStats playerStats;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        playerStats = FindObjectOfType<PlayerStats>(); 
    }

    private void Start()
    {
        // Assign the "Axe" layer to the axe GameObject in the Inspector
        gameObject.layer = LayerMask.NameToLayer("Axe");
    }

    public void LoadData(GameData data)
    {
        this.lifesteal = data.lifesteal;
        this.axeLifestealAmount = data.axeLifestealAmount;
    }

    public void SaveData(ref GameData data)
    {
        data.lifesteal = this.lifesteal;
        data.axeLifestealAmount = this.axeLifestealAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.TryGetComponent(out Orc_Health orc))
            {
                Vector3 axeVelocity = GetComponent<Rigidbody>().velocity.normalized;

                Vector3 direction = new Vector3(axeVelocity.x, 0, axeVelocity.z);

                orc.KnockBack(direction);
            }

            if (lifesteal && playerStats.Health < (playerStats.maxHealth - axeLifestealAmount))
            {
                playerStats.AddHealth(axeLifestealAmount);
            }
        }
    }

    public int GetAxeDamage()
    {
        return axeDamage;
    }

    public bool HasAxeHitSomething()
    {
        return axeHitSomething;
    }
}
