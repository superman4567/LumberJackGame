using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueChain", menuName = "DialogueChain/Dialgoue")]
public class DialogueContainer : ScriptableObject
{
    [TextArea(3,8)]
    public string[] description;
}
