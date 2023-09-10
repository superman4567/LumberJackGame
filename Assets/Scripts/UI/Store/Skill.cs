using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public StoreItem ID;
    public int skillCost;
    public bool canBeBought = false;
    public bool isUnlocked = false;
    public List<Skill> prerequisites;

    

}
