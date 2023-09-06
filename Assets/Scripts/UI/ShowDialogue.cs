using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    [SerializeField] DialogueContainer dialogueContainerReference;
    private static ShowDialogue currentDialogue;
    private int currentDialogueIndex;

    private void Awake()
    {
        currentDialogue = null;
    }

    private void OnEnable()
    {
        DialogueManager.Instance.OnNextDialogue += DoneReading;
    }

    private void OnDisable()
    {
        DialogueManager.Instance.OnNextDialogue -= DoneReading;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }
        currentDialogue = this;
        DialogueManager.Instance.SetDialogue(dialogueContainerReference);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }
        {
            if (DialogueManager.Instance.currentTextIndex >= dialogueContainerReference.description.Length)
            {
                DialogueManager.Instance.HideTutorialUI();
                DialogueManager.Instance.ResetValues();
                Destroy();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }
        {
            if (DialogueManager.Instance.currentTextIndex >= dialogueContainerReference.description.Length)
            {
                DialogueManager.Instance.HideTutorialUI();
                DialogueManager.Instance.ResetValues();
                Destroy();
            }
            DialogueManager.Instance.HideTutorialUI();
            DialogueManager.Instance.ResetValues();
        }
    }

    private void DoneReading()
    {
        if (currentDialogue == this)
        {
            currentDialogueIndex++;
        }

        if (currentDialogue.gameObject.tag == "Board")
        {
            if (DialogueManager.Instance.currentTextIndex >= dialogueContainerReference.description.Length)
            {
                FindObjectOfType<DifficultySelection>().hasPlayerInteractedWithBoard = true;
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
