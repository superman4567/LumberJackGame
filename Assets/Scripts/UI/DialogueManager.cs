using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueObject;
    private Animator animator;

    private bool tutorialTextHasBeenShown = false;

    void Start()
    {
        animator= dialogueObject.GetComponent<Animator>();
        dialogueObject.SetActive(true);


        if (SceneManager.GetActiveScene().buildIndex == 1 && !tutorialTextHasBeenShown)
        {
            UpdateDialogueText("Welcome to the game! This is my cabin I build with my own two bare hands. This place allows me to find piece and unlock powers I did not know were possible.");
        }

        else if (SceneManager.GetActiveScene().buildIndex == 2 && !tutorialTextHasBeenShown)
        {
            UpdateDialogueText("Pay attention! Here is where things get real. As soon as we enter this area, orcs will feel our" +
                " presence and try to eliamte us, on top of that there is also this nasty storm, we should try to make a campfire when our stamina is to low.");
        }
    }

    public void UpdateDialogueText(string newText)
    {
        animator.SetTrigger("StartReading");
        dialogueText.text = newText;

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            animator.SetTrigger("FinishedReading");
        }
    }

    public void closeDialoguePanel()
    {
        animator.SetTrigger("FinishedReading");
    }
}
