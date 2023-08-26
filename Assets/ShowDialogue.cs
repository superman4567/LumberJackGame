using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    [SerializeField] DialogueContainer dialgoueContainerRference;
    private Collider currentObjectCollider;

    private void Start()
    {
        currentObjectCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        DialogueManager.Instance.SetDialogue(dialgoueContainerRference);

        if(gameObject.tag == "Board")
        {
            FindObjectOfType<DifficultySelection>().hasPlayerInteractedWithBoard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (DialogueManager.Instance.currentTextIndex >= dialgoueContainerRference.description.Length)
        {
            DialogueManager.Instance.HideTutorialUI();
            Destroy(currentObjectCollider);
        }
    }
}
