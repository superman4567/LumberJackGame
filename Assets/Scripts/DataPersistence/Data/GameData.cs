using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int wood;
    public int coins;
    public int difficulty;

    public GameData()
    {
        this.wood = 0;
        this.coins= 0;
        this.difficulty = 0;
    }
}
