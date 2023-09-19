using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance;

    [Header("References")]
    private PlayerStats playerStats;
    private PlayerThrowAxe playerThrowAxe;
    private AxeDetection axeDetection;
    private PlayerAnimations playerAnimations;
    private BuildCampfire playerCampfire;
    private PlayerEmmissionChange playerEmmissionChange;
    private PlayerUltimates playerUltimates;

    [Header("Button")]
    [SerializeField] private Button[] healingButtons;
    [SerializeField] private Button[] damageButtons;
    [SerializeField] private Button[] survivalButtons;

    [Header("BoughtSprites")]
    [SerializeField] private Sprite[] boughtSprites;

    [Header("CanBeBoughtSprites")]
    [SerializeField] private Sprite[] canBeBoughtSprites;

    [Header("CantBeBoughtSprites")]
    [SerializeField] private Sprite[] cantBeBoughtSprites;

    [Header("selectedSprites")]
    [SerializeField] private Sprite[] selectedSprites;

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
        playerThrowAxe = FindObjectOfType<PlayerThrowAxe>();
        axeDetection = FindObjectOfType<AxeDetection>();
        playerAnimations = FindObjectOfType<PlayerAnimations>();
        playerCampfire = FindObjectOfType<BuildCampfire>();
        playerEmmissionChange = FindObjectOfType<PlayerEmmissionChange>();
        playerUltimates = FindObjectOfType<PlayerUltimates>();
    }

    private void Start()
    {
        AbilityCanBePurchased();
    }

    private void Update()
    {
        for (int i = 0; i < healingButtons.Length; i++)
        {
            Button button = healingButtons[i];
            UpdatePrice updatePrice = button.GetComponent<UpdatePrice>();

            if (updatePrice.holdingSkill == selectedAbility)
            {
                if (i == 0 || i == 3) { button.image.sprite = selectedSprites[0]; }
                if (i == 1 || i == 4 || i == 6) { button.image.sprite = selectedSprites[1]; }
                if (i == 2 || i == 5 || i == 7) { button.image.sprite = selectedSprites[2]; }
                if (i == 8) { button.image.sprite = selectedSprites[3]; }
            }
        }

        for (int i = 0; i < damageButtons.Length; i++)
        {
            Button button = damageButtons[i];
            UpdatePrice updatePrice = button.GetComponent<UpdatePrice>();

            if (updatePrice.holdingSkill == selectedAbility)
            {
                if (i == 0 || i == 3) { button.image.sprite = selectedSprites[4]; }
                if (i == 1 || i == 4) { button.image.sprite = selectedSprites[5]; }
                if (i == 2 || i == 5) { button.image.sprite = selectedSprites[6]; }
                if (i == 6) { button.image.sprite = selectedSprites[7]; }
            }
        }

        for (int i = 0; i < survivalButtons.Length; i++)
        {
            Button button = survivalButtons[i];
            UpdatePrice updatePrice = button.GetComponent<UpdatePrice>();

            if (updatePrice.holdingSkill == selectedAbility)
            {
                if (i == 0 || i == 2) { button.image.sprite = selectedSprites[8]; }
                if (i == 1 || i == 3) { button.image.sprite = selectedSprites[9]; }
                if (i == 4) { button.image.sprite = selectedSprites[10]; }
                if (i == 5) { button.image.sprite = selectedSprites[11]; }
            }
        }
    }

    public void AbilityCanBePurchased()
    {
        for (int i = 0; i < healingButtons.Length; i++)
        {
            Button button = healingButtons[i];
            UpdatePrice updatePrice = button.GetComponent<UpdatePrice>();

            if (updatePrice.holdingSkill.isUnlocked)
            {
                updatePrice.holdingSkill.canBeBought = true;

                if (i == 0 || i == 3)           { button.image.sprite = boughtSprites[0]; }
                if (i == 1 || i == 4 || i == 6) { button.image.sprite = boughtSprites[1]; }
                if (i == 2 || i == 5 || i == 7) { button.image.sprite = boughtSprites[2]; }
                if (i == 8)                     { button.image.sprite = boughtSprites[3]; }
            }
            else if (updatePrice.holdingSkill.skillCost < GameManager.Instance.GetCoins())
            {
                updatePrice.holdingSkill.canBeBought = true;
                if (i == 0 || i == 3)           { button.image.sprite = canBeBoughtSprites[0]; }
                if (i == 1 || i == 4 || i == 6) { button.image.sprite = canBeBoughtSprites[1]; }
                if (i == 2 || i == 5 || i == 7) { button.image.sprite = canBeBoughtSprites[2]; }
                if (i == 8)                     { button.image.sprite = canBeBoughtSprites[3]; }
            }
            else
            {
                updatePrice.holdingSkill.canBeBought = false;
                if (i == 0 || i == 3)           { button.image.sprite = cantBeBoughtSprites[0]; }
                if (i == 1 || i == 4 || i == 6) { button.image.sprite = cantBeBoughtSprites[1]; }
                if (i == 2 || i == 5 || i == 7) { button.image.sprite = cantBeBoughtSprites[2]; }
                if (i == 8)                     { button.image.sprite = cantBeBoughtSprites[3]; }

                button.interactable = false;
            }
        }

        for (int i = 0; i < damageButtons.Length; i++)
        {
            Button button = damageButtons[i];
            UpdatePrice updatePrice = button.GetComponent<UpdatePrice>();

            if (updatePrice.holdingSkill.isUnlocked)
            {
                updatePrice.holdingSkill.canBeBought = true;
                if (i == 0 || i == 3) { button.image.sprite = boughtSprites[4]; }
                if (i == 1 || i == 4) { button.image.sprite = boughtSprites[5]; }
                if (i == 2 || i == 5) { button.image.sprite = boughtSprites[6]; }
                if (i == 6) { button.image.sprite = boughtSprites[7]; }
            }
            else if (updatePrice.holdingSkill.skillCost < GameManager.Instance.GetCoins())
            {
                updatePrice.holdingSkill.canBeBought = true;
                if (i == 0 || i == 3) { button.image.sprite = canBeBoughtSprites[4]; }
                if (i == 1 || i == 4) { button.image.sprite = canBeBoughtSprites[5]; }
                if (i == 2 || i == 5) { button.image.sprite = canBeBoughtSprites[6]; }
                if (i == 6) { button.image.sprite = canBeBoughtSprites[7]; }
            }
            else
            {
                updatePrice.holdingSkill.canBeBought = false;
                if (i == 0 || i == 3) { button.image.sprite = cantBeBoughtSprites[4]; }
                if (i == 1 || i == 4) { button.image.sprite = cantBeBoughtSprites[5]; }
                if (i == 2 || i == 5) { button.image.sprite = cantBeBoughtSprites[6]; }
                if (i == 6) { button.image.sprite = cantBeBoughtSprites[7]; }

                button.interactable = false;
            }
        }

        for (int i = 0; i < survivalButtons.Length; i++)
        {
            Button button = survivalButtons[i];
            UpdatePrice updatePrice = button.GetComponent<UpdatePrice>();

            if (updatePrice.holdingSkill.isUnlocked)
            {
                updatePrice.holdingSkill.canBeBought = true;

                if (i == 0 || i == 2) { button.image.sprite = boughtSprites[8]; }
                if (i == 1 || i == 3) { button.image.sprite = boughtSprites[9]; }
                if (i == 4) { button.image.sprite = boughtSprites[10]; }
                if (i == 5) { button.image.sprite = boughtSprites[11]; }
            }
            else if (updatePrice.holdingSkill.skillCost < GameManager.Instance.GetCoins())
            {
                updatePrice.holdingSkill.canBeBought = true;
                if (i == 0 || i == 2) { button.image.sprite = canBeBoughtSprites[8]; }
                if (i == 1 || i == 3) { button.image.sprite = canBeBoughtSprites[9]; }
                if (i == 4) { button.image.sprite = canBeBoughtSprites[10]; }
                if (i == 5) { button.image.sprite = canBeBoughtSprites[11]; }
            }
            else
            {
                updatePrice.holdingSkill.canBeBought = false;
                if (i == 0 || i == 2) { button.image.sprite = cantBeBoughtSprites[8]; }
                if (i == 1 || i == 3) { button.image.sprite = cantBeBoughtSprites[9]; }
                if (i == 4) { button.image.sprite = cantBeBoughtSprites[10]; }
                if (i == 5) { button.image.sprite = cantBeBoughtSprites[11]; }

                button.interactable = false;
            }
        }
    }

    public void CanSkillBeUnlocked()
    {
        if (selectedAbility.canBeBought && !selectedAbility.isUnlocked && selectedAbility != null)
        {
            if (GameManager.Instance.GetCoins() >= selectedAbility.skillCost)
            {
                selectedAbility.isUnlocked = true;
                GameManager.Instance.SubstractResource(GameManager.ResourceType.Coins, selectedAbility.skillCost);
                ApplyPermaAbilities(selectedAbility.ID);
                AbilityCanBePurchased();
                playerUltimates.UltimateSpriteUnlockCheck();
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
            //////////////////////////////////////
            //Healing
            //////////////////////////////////////
            
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
                playerEmmissionChange.healingActive = true;
                break;

            //////////////////////////////////////
            //DAMAGE
            //////////////////////////////////////

            case StoreItem.Damage_DoubleDamage1:
                axeDetection.axeDamage *= 2;
                break;

            case StoreItem.Damage_DoubleDamage2:
                axeDetection.axeDamage *= 2;
                break;

            case StoreItem.Damage_ExplosiveHits1:
                axeDetection.explosiveRadiusT1 = true;
                break;

            case StoreItem.Damage_ExplosiveHits2:
                axeDetection.explosiveRadiusT2 = true;
                break;

            case StoreItem.Damage_ThrowSpeed1:
                playerAnimations.tier1Unlocked = true;
                playerThrowAxe.throwforceMultiplier = 1.5f;
                break;

            case StoreItem.Damage_ThrowSpeed2:
                playerAnimations.tier2Unlocked = true;
                playerThrowAxe.throwforceMultiplier = 2f;
                break;

            case StoreItem.Damage_Ultimate:
                playerEmmissionChange.damageActive = true;
                break;

            //////////////////////////////////////
            //Survival
            //////////////////////////////////////
            
            case StoreItem.Survival_ExtraFocus1:
                playerStats.maxStamina = 125;
                break;

            case StoreItem.Survival_ExtraFocus2:
                playerStats.maxStamina = 150;
                break;

            case StoreItem.Survival_CampfireEffect1:
                playerCampfire.campfireDuration = 12f;
                break;

            case StoreItem.Survival_CampfireEffect2:
                playerCampfire.campfireDuration = 18f;
                break;

            case StoreItem.Survival_LessStaminaReducation:
                playerStats.reducer = 1.5f;
                break;

            case StoreItem.Survival_Ultimate:
                playerEmmissionChange.surviveActive = true;
                break;

            default:
                Debug.LogError("Invalid ability ID");
                break;
        }
    }
}
