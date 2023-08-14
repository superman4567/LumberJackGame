using UnityEngine;

[CreateAssetMenu(fileName = "New Blueprint Data", menuName = "Blueprint Data")]
public class BlueprintData : ScriptableObject
{
    public string blueprintName;
    public string description;
    public Sprite icon;
    public bool isUnlocked; 
}
