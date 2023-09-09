using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]

public class GameData
{
    //Currency
    public int coins;

    //Difficulty
    public int difficulty;

    //UpgradeVaraibles - Health
    public float maxHealth;
    public float chestHealthGain;
    public float axeLifestealAmount;
    public bool lifesteal;

    public GameData()
    {
        //Currency
        this.coins= 0;

        //Difficulty
        this.difficulty = 0;

        //UpgradeVaraibles - Health
        this.maxHealth = 0;
        this.chestHealthGain = 0;
        this.axeLifestealAmount = 0;
        this.lifesteal = false;
    }
}
