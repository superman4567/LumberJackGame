using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    public static EventHandler<string> OnFinishedDialogue;
    public EventHandler<int> OnNextDialogue;
    [SerializeField] private DialogueContainer dialogue;

    private PlayerStats player;
    private DialogueManager dialogueManager;

    private int currentDialogueIndex = 0;
    private bool completedDialogue = false;
    private bool isInteractingWithPlayer = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        dialogueManager = DialogueManager.Instance;
    }

    private void Update()
    {
        if (!isInteractingWithPlayer) return;

        // Replace with input system
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!TryUpdateNextDialogue())
            {
                CompleteDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (completedDialogue) return;
        if (other.tag != player.tag) return;
        isInteractingWithPlayer = true;
        // Start Dialogue SFX
        AkSoundEngine.PostEvent("Play_Dialogue_SFX", gameObject);
        dialogueManager.UpdateDialogueText(dialogue.description[currentDialogueIndex]);
        dialogueManager.ShowUI();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != player.tag) return;
        isInteractingWithPlayer = false;

        // ADD TOGGLE AUDIO. WHERE IN THE SCRIPT TO CALL THE "Toggle Dialogue" AUDIO?
        //EVENT: AkSoundEngine.PostEvent("Play_Menu_Toggle_SFX", gameObject);

        dialogueManager.HideUI();

        if (completedDialogue)
        {
            Destroy(gameObject);
        }
    }

    private bool TryUpdateNextDialogue()
    {
        if (currentDialogueIndex < dialogue.description.Length - 1)
        {
            currentDialogueIndex++;
            dialogueManager.UpdateDialogueText(dialogue.description[currentDialogueIndex]);
            OnNextDialogue?.Invoke(this,currentDialogueIndex);
            return true;
        }

        return false;
    }

    private void CompleteDialogue()
    {
        if (currentDialogueIndex >= dialogue.description.Length - 1)
        {
            OnFinishedDialogue?.Invoke(this, tag);
            completedDialogue = true;
            dialogueManager.HideUI();
            // Hide Dialogue SFX
            AkSoundEngine.PostEvent("Play_Menu_Toggle_RC", gameObject);
            // BUG: You'll hear this audio any time you press 'F' on the keyboard, AFTER the dialogue is closed.
            // This audio and the dialogue toggle audio is possibly going to be a random container or a different sound.
        }
    }
}
