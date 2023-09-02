using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PowerUps", menuName = "PowerUps/Powerup")]

public class PowerupSO : ScriptableObject
{
    public Sprite PowerUpSrpite;
    public string PowerupTitle;
    [TextArea(2, 4)]
    public string PowerupDescription;
}
