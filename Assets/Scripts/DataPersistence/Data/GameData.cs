using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int wood;
    public int sticks;
    public int planks;
    public int woodenSpikes;
    public int rock;

    public GameData()
    {
        this.wood = 0;
        this.sticks= 0;
        this.planks = 0;
        this.woodenSpikes = 0;
        this.rock= 0;
    }
}
