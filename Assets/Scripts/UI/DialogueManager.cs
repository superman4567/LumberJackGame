using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Animator aniamtor;

    [SerializeField]
    private List<string> tutorial1Texts = new();

    [SerializeField]
    private List<string> tutorial2Texts = new();

    private List<string> activeTextList = new();
    private int currentTextIndex = 0;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShowNextDialogue();
        }
    }

    // Call from anywhere you want to show a tutorial
    private void ShowUI()
    {
        if (activeTextList == null || currentTextIndex == -1)
        {
            Debug.LogWarning("No active text list");
            return;
        }

        aniamtor.SetTrigger("StartReading");
        dialogueText.text = activeTextList[currentTextIndex];
    }

    public void ShowNextDialogue()
    {
        if (activeTextList == null || currentTextIndex == -1)
        {
            Debug.LogWarning("No active text list");
            return;
        }

        currentTextIndex++;
        if (currentTextIndex > tutorial1Texts.Count - 1)
        {
            HideTutorialUI();
            return;
        }
        dialogueText.text = activeTextList[currentTextIndex];
    }

    public void HideTutorialUI()
    {
        // TODO Animate / disable the gameobject with the text etc on it
        aniamtor.SetTrigger("FinishedReading");
        activeTextList = null; // For safety
        currentTextIndex = -1; // For safety so we know no active index
    }

    public void ShowTutorial1()
    {
        // TODO laat 1 x zien etc
        activeTextList = tutorial1Texts;
        currentTextIndex = 0;
        ShowUI();
    }

    // Call from anywhere you want to show a tutorial
    public void ShowTutorial2()
    {
        activeTextList = tutorial2Texts;
        currentTextIndex = 0;
        ShowUI();
    }
}
