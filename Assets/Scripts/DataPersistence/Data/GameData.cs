using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]

public class GameData
{
    //Currency
    public int coins;

    //Selected Difficulty
    public int difficulty;
    public bool diffiuclty0Unlocked;
    public bool diffiuclty1Unlocked;
    public bool diffiuclty2Unlocked;

    //UpgradeVaraibles - Health
    public float maxHealth;
    public float chestHealthGain;
    public float axeLifestealAmount;
    public bool lifesteal;

    //UpgradeVaraibles - Damage
    public int axeDamage;
    public bool explosiveRadiusT1;
    public bool explosiveRadiusT2;
    public bool throwspeedTier1Unlocked;
    public bool throwspeedTier2Unlocked;
    public float throwforceMultiplier;

    //UpgradeVariables - Survival
    public float maxStamina;
    public float campfireDuration;
    public float reducer;

    //Abilities Unlocked
    public bool healing_IncreaseMaxHealth1;
    public bool healing_IncreaseMaxHealth2;
    public bool healing_HealthBoostFromChest1;
    public bool healing_HealthBoostFromChest2;
    public bool healing_HealthBoostFromChest3;
    public bool healing_LifeSteal1;
    public bool healing_LifeSteal2;
    public bool healing_LifeSteal3;
    public bool healing_Ultimate;

    public bool damage_DoubleDamage1;
    public bool damage_DoubleDamage2;
    public bool damage_ExplosiveHits1;
    public bool damage_ExplosiveHits2;
    public bool damage_ThrowSpeed1;
    public bool damage_ThrowSpeed2;
    public bool damage_Ultimate;

    public bool survival_ExtraFocus1;
    public bool survival_ExtraFocus2;
    public bool survival_CampfireEffect1;
    public bool survival_CampfireEffect2;
    public bool survival_LessStaminaReducation;
    public bool survival_Ultimate;

    public GameData()
    {
        //Currency
        this.coins= 0;

        //Selected Difficulty
        this.difficulty = 0;
        this.diffiuclty0Unlocked = true;
        this.diffiuclty1Unlocked = false;
        this.diffiuclty2Unlocked = false;

        //UpgradeVaraibles - Health
        this.maxHealth = 100;
        this.chestHealthGain = 0;
        this.axeLifestealAmount = 0;
        this.lifesteal = false;

        //UpgradeVaraibles - Damage
        this.axeDamage = 20;
        this.explosiveRadiusT1 = false;
        this.explosiveRadiusT2 = false;
        this.throwspeedTier1Unlocked = false;
        this.throwspeedTier2Unlocked = false;
        this.throwforceMultiplier = 1f;

        //UpgradeVariables - Survival
        this.maxStamina = 100f;
        this.campfireDuration = 6f;
        this.reducer = 0f;

        //Ultimates Unlocked
        this.healing_IncreaseMaxHealth1 = false;
        this.healing_IncreaseMaxHealth2 =false;
        this.healing_HealthBoostFromChest1 = false;
        this.healing_HealthBoostFromChest2 = false;
        this.healing_HealthBoostFromChest3 = false;
        this.healing_LifeSteal1 = false;
        this.healing_LifeSteal2 = false;
        this.healing_LifeSteal3 = false;
        this.healing_Ultimate = false;

        this.damage_DoubleDamage1 = false;
        this.damage_DoubleDamage2 = false;
        this.damage_ExplosiveHits1 = false;
        this.damage_ExplosiveHits2 = false;
        this.damage_ThrowSpeed1 = false;
        this.damage_ThrowSpeed2 = false;
        this.damage_Ultimate = false;

        this.survival_ExtraFocus1 = false;
        this.survival_ExtraFocus2 = false;
        this.survival_CampfireEffect1 = false;
        this.survival_CampfireEffect2 = false;
        this.survival_LessStaminaReducation = false;
        this.survival_Ultimate = false;
}
}
