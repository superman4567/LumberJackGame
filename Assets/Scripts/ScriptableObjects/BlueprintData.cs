using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Blueprint Data", menuName = "Blueprint Data")]
public class BlueprintData : ScriptableObject
{
    public string blueprintName;
    public string description;
    public Sprite icon;
    public bool isUnlocked; 
}