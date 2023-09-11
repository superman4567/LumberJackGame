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
    }
}
