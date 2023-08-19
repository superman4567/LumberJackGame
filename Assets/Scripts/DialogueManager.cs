using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Reference to the UI Text element

    public void UpdateDialogueText(string newText)
    {
        dialogueText.text = newText;
    }
}
