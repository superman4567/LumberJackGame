using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance;

    [Header("References")]
    private PlayerStats playerStats;

    [Header("Button")]
    [SerializeField] private Button[] buttons;
    [SerializeField] private Color CantBeBoughtColor;
    [SerializeField] private Color CanBeBoughtColor;
    [SerializeField] private Color BoughtColor;
    public Skill selectedAbility;

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
        AbilityCanBePurchased();
    }

    public void AbilityCanBePurchased()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            ColorBlock buttonColors = buttons[i].colors; // Get the current ColorBlock

            if ( buttons[i].GetComponent<UpdatePrice>().holdingSkill.isUnlocked)
            {
                buttons[i].GetComponent<UpdatePrice>().holdingSkill.canBeBought = true;

                buttonColors.normalColor = BoughtColor;
                buttonColors.highlightedColor = BoughtColor;
            }
            else if(buttons[i].GetComponent<UpdatePrice>().holdingSkill.skillCost < GameManager.Instance.GetCoins())
            {
                buttons[i].GetComponent<UpdatePrice>().holdingSkill.canBeBought = true;

                buttonColors.normalColor = CanBeBoughtColor;
                buttonColors.highlightedColor = CanBeBoughtColor;
            }
            else
            {
                buttons[i].GetComponent<UpdatePrice>().holdingSkill.canBeBought = false;

                buttonColors.normalColor = CantBeBoughtColor;
                buttonColors.highlightedColor = CantBeBoughtColor;
            }

            // Assign the modified ColorBlock back to the button
            buttons[i].colors = buttonColors;
        }
    }

    public void CanSkillBeUnlocked()
    {
        if (selectedAbility.canBeBought && !selectedAbility.isUnlocked)
        {
            if (GameManager.Instance.GetCoins() >= selectedAbility.skillCost)
            {
                selectedAbility.isUnlocked = true;
                GameManager.Instance.SubstractResource(GameManager.ResourceType.Coins, selectedAbility.skillCost);
                ApplyPermaAbilities(selectedAbility.ID);
                AbilityCanBePurchased();
            }
        }
        else
        {
            // Show error
        }
    }

    private void ApplyPermaAbilities(StoreItem ID)
    {
        switch (ID)
        {
            case StoreItem.Healing_IncreaseMaxHealth1:
                playerStats.maxHealth = 150;
                break;

            case StoreItem.Healing_IncreaseMaxHealth2:
                playerStats.maxHealth = 300;
                break;

            case StoreItem.Healing_HealthBoostFromChest1:
                GameManager.Instance.chestHealthGain = 50;
                break;

            case StoreItem.Healing_HealthBoostFromChest2:
                GameManager.Instance.chestHealthGain = playerStats.maxHealth /2;
                break;

            case StoreItem.Healing_HealthBoostFromChest3:
                GameManager.Instance.chestHealthGain = playerStats.maxHealth;
                break;

            case StoreItem.Healing_LifeSteal1:
                AxeDetection.Instance.lifesteal = true;
                AxeDetection.Instance.axeLifestealAmount = .25f;
                break;

            case StoreItem.Healing_LifeSteal2:
                AxeDetection.Instance.axeLifestealAmount = 1f;
                break;

            case StoreItem.Healing_LifeSteal3:
                AxeDetection.Instance.axeLifestealAmount = 3f;
                break;

            case StoreItem.Healing_Ultimate:
                // Apply ability for ID 1
                break;

            default:
                Debug.LogError("Invalid ability ID");
                break;
        }
    }
}
