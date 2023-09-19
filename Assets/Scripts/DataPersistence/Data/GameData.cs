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
    public Dictionary<StoreItem, bool> abilityStatusMap = new();

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
        this.abilityStatusMap = new();
    }
}
