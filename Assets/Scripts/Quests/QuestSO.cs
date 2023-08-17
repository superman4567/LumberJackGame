using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class QuestSO : ScriptableObject
{
    public string questName;
    public string description;
    
    public int requiredAmount; 
    public int currentAmount;

    public bool isActive;
    public bool isCompleted;
}
