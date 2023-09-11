using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Animator aniamtor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.LogError($"{name} is a copy of an instance that already exist in your scene.");
            return;
        }

        Instance = this;
    }

    public void ShowUI()
    {
        aniamtor.SetBool("IsReading", true);
    }

    public void UpdateDialogueText(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    public void HideUI()
    {
        aniamtor.SetBool("IsReading", false);
        
    }
}
