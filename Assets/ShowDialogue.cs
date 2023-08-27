using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    [SerializeField] DialogueContainer dialogueContainerReference;
    private Collider currentObjectCollider;

    private void Start()
    {
        currentObjectCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        DialogueManager.Instance.SetDialogue(dialogueContainerReference);

        if(gameObject.tag == "Board")
        {
            FindObjectOfType<DifficultySelection>().hasPlayerInteractedWithBoard = true;
        }

        if (gameObject.tag == "GameStarter")
        {
            FindObjectOfType<OrcSpawner>().startToSpawnOrcs = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (DialogueManager.Instance.currentTextIndex >= dialogueContainerReference.description.Length)
        {
            DialogueManager.Instance.HideTutorialUI();
            Destroy(currentObjectCollider);
        }
    }
}
