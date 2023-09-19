using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour, IDataPersistance
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

    private bool healing_IncreaseMaxHealth1 = false;
    private bool healing_IncreaseMaxHealth2 = false;
    private bool healing_HealthBoostFromChest1 = false;
    private bool healing_HealthBoostFromChest2 = false;
    private bool healing_HealthBoostFromChest3 = false;
    private bool healing_LifeSteal1 = false;
    private bool healing_LifeSteal2 = false;
    private bool healing_LifeSteal3 = false;
    private bool healing_Ultimate = false;

    private bool damage_DoubleDamage1 = false;
    private bool damage_DoubleDamage2 = false;
    private bool damage_ExplosiveHits1 = false;
    private bool damage_ExplosiveHits2 = false;
    private bool damage_ThrowSpeed1 = false;
    private bool damage_ThrowSpeed2 = false;
    private bool damage_Ultimate = false;

    private bool survival_ExtraFocus1 = false;
    private bool survival_ExtraFocus2 = false;
    private bool survival_CampfireEffect1 = false;
    private bool survival_CampfireEffect2 = false;
    private bool survival_LessStaminaReducation = false;
    private bool survival_Ultimate = false;

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

        playerUltimates.UltimateSpriteUnlockCheck();
    }

    private void Start()
    {
        UpdatePurchaseStateSprite();
    }

    public void LoadData(GameData data)
    {
        this.healing_IncreaseMaxHealth1 = data.healing_IncreaseMaxHealth1;
        this.healing_IncreaseMaxHealth2 = data.healing_IncreaseMaxHealth2;
        this.healing_HealthBoostFromChest1 = data.healing_HealthBoostFromChest1;
        this.healing_HealthBoostFromChest2 = data.healing_HealthBoostFromChest2;
        this.healing_HealthBoostFromChest3 = data.healing_HealthBoostFromChest3;
        this.healing_LifeSteal1 = data.healing_LifeSteal1;
        this.healing_LifeSteal2 = data.healing_LifeSteal2;
        this.healing_LifeSteal3 = data.healing_LifeSteal3;
        this.healing_Ultimate = data.healing_Ultimate;

        this.damage_DoubleDamage1 = data.damage_DoubleDamage1;
        this.damage_DoubleDamage2 = data.damage_DoubleDamage2;
        this.damage_ExplosiveHits1 = data.damage_ExplosiveHits1;
        this.damage_ExplosiveHits2 = data.damage_ExplosiveHits2;
        this.damage_ThrowSpeed1 = data.damage_ThrowSpeed1;
        this.damage_ThrowSpeed2 = data.damage_ThrowSpeed2;
        this.damage_Ultimate = data.damage_Ultimate;

        this.survival_ExtraFocus1 = data.survival_ExtraFocus1;
        this.survival_ExtraFocus2 = data.survival_ExtraFocus2;
        this.survival_CampfireEffect1 = data.survival_CampfireEffect1;
        this.survival_CampfireEffect2 = data.survival_CampfireEffect2;
        this.survival_LessStaminaReducation = data.survival_LessStaminaReducation;
        this.survival_Ultimate = data.survival_Ultimate;
    }

    public void SaveData(GameData data)
    {
        data.healing_IncreaseMaxHealth1 = this.healing_IncreaseMaxHealth1;
        data.healing_IncreaseMaxHealth2 = this.healing_IncreaseMaxHealth2;
        data.healing_HealthBoostFromChest1 = this.healing_HealthBoostFromChest1;
        data.healing_HealthBoostFromChest2 = this.healing_HealthBoostFromChest2;
        data.healing_HealthBoostFromChest3 = this.healing_HealthBoostFromChest3;
        data.healing_LifeSteal1 = this.healing_LifeSteal1;
        data.healing_LifeSteal2 = this.healing_LifeSteal2;
        data.healing_LifeSteal3 = this.healing_LifeSteal3;
        data.healing_Ultimate = this.healing_Ultimate;

        data.damage_DoubleDamage1 = this.damage_DoubleDamage1;
        data.damage_DoubleDamage2 = this.damage_DoubleDamage2;
        data.damage_ExplosiveHits1 = this.damage_ExplosiveHits1;
        data.damage_ExplosiveHits2 = this.damage_ExplosiveHits2;
        data.damage_ThrowSpeed1 = this.damage_ThrowSpeed1;
        data.damage_ThrowSpeed2 = this.damage_ThrowSpeed2;
        data.damage_Ultimate = this.damage_Ultimate;

        data.survival_ExtraFocus1 = this.survival_ExtraFocus1;
        data.survival_ExtraFocus2 = this.survival_ExtraFocus2;
        data.survival_CampfireEffect1 = this.survival_CampfireEffect1;
        data.survival_CampfireEffect2 = this.survival_CampfireEffect2;
        data.survival_LessStaminaReducation = this.survival_LessStaminaReducation;
        data.survival_Ultimate = this.survival_Ultimate;
    }

    private void Update()
    {
        UpdateSelectedSprite();
    }

    private void UpdateSelectedSprite()
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

    public void UpdatePurchaseStateSprite()
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

    public void CanAbilityBeUnlocked()
    {
        if (selectedAbility.canBeBought && !selectedAbility.isUnlocked && selectedAbility != null)
        {
            if (GameManager.Instance.GetCoins() >= selectedAbility.skillCost)
            {
                selectedAbility.isUnlocked = true;
                //match the selected ability with the local bool

                GameManager.Instance.SubstractResource(GameManager.ResourceType.Coins, selectedAbility.skillCost);
                ApplyPermaAbilities(selectedAbility.ID);
                UpdatePurchaseStateSprite();
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
                healing_IncreaseMaxHealth1 = true;
                playerStats.maxHealth = 150;
                break;

            case StoreItem.Healing_IncreaseMaxHealth2:
                healing_IncreaseMaxHealth2 = true;
                playerStats.maxHealth = 300;
                break;

            case StoreItem.Healing_HealthBoostFromChest1:
                healing_HealthBoostFromChest1 = true;
                GameManager.Instance.chestHealthGain = 50;
                break;

            case StoreItem.Healing_HealthBoostFromChest2:
                healing_HealthBoostFromChest2 = true;
                GameManager.Instance.chestHealthGain = playerStats.maxHealth /2;
                break;

            case StoreItem.Healing_HealthBoostFromChest3:
                healing_HealthBoostFromChest3 = true;
                GameManager.Instance.chestHealthGain = playerStats.maxHealth;
                break;

            case StoreItem.Healing_LifeSteal1:
                healing_LifeSteal1 = true;
                AxeDetection.Instance.lifesteal = true;
                AxeDetection.Instance.axeLifestealAmount = .25f;
                break;

            case StoreItem.Healing_LifeSteal2:
                healing_LifeSteal2 = true;
                AxeDetection.Instance.axeLifestealAmount = 1f;
                break;

            case StoreItem.Healing_LifeSteal3:
                healing_LifeSteal3 = true;
                AxeDetection.Instance.axeLifestealAmount = 3f;
                break;

            case StoreItem.Healing_Ultimate:
                healing_Ultimate = true;
                playerEmmissionChange.healingActive = true;
                break;

            //////////////////////////////////////
            //DAMAGE
            //////////////////////////////////////

            case StoreItem.Damage_DoubleDamage1:
                damage_DoubleDamage1= true;
                axeDetection.axeDamage *= 2;
                break;

            case StoreItem.Damage_DoubleDamage2:
                damage_DoubleDamage2= true;
                axeDetection.axeDamage *= 2;
                break;

            case StoreItem.Damage_ExplosiveHits1:
                damage_ExplosiveHits1 = true;
                axeDetection.explosiveRadiusT1 = true;
                break;

            case StoreItem.Damage_ExplosiveHits2:
                damage_ExplosiveHits2 = true;
                axeDetection.explosiveRadiusT2 = true;
                break;

            case StoreItem.Damage_ThrowSpeed1:
                damage_ThrowSpeed1= true;
                playerAnimations.tier1Unlocked = true;
                playerThrowAxe.throwforceMultiplier = 1.5f;
                break;

            case StoreItem.Damage_ThrowSpeed2:
                damage_ThrowSpeed2= true;
                playerAnimations.tier2Unlocked = true;
                playerThrowAxe.throwforceMultiplier = 2f;
                break;

            case StoreItem.Damage_Ultimate:
                damage_Ultimate = true;
                playerEmmissionChange.damageActive = true;
                break;

            //////////////////////////////////////
            //Survival
            //////////////////////////////////////
            
            case StoreItem.Survival_ExtraFocus1:
                survival_ExtraFocus1= true;
                playerStats.maxStamina = 125;
                break;

            case StoreItem.Survival_ExtraFocus2:
                survival_ExtraFocus2= true;
                playerStats.maxStamina = 150;
                break;

            case StoreItem.Survival_CampfireEffect1:
                survival_CampfireEffect1 = true;
                playerCampfire.campfireDuration = 12f;
                break;

            case StoreItem.Survival_CampfireEffect2:
                survival_CampfireEffect2 = true;
                playerCampfire.campfireDuration = 18f;
                break;

            case StoreItem.Survival_LessStaminaReducation:
                survival_LessStaminaReducation = true;
                playerStats.reducer = 1.5f;
                break;

            case StoreItem.Survival_Ultimate:
                survival_Ultimate= true;
                playerEmmissionChange.surviveActive = true;
                break;

            default:
                Debug.LogError("Invalid ability ID");
                break;
        }
    }
}
