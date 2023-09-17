using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IndoorTransition : MonoBehaviour
{
    [SerializeField] private int sceneIndexToLoad;
    [SerializeField] private CabinetDoor cabinetDoor;
    [SerializeField] private GameObject tutorialPanel;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private TextMeshProUGUI loadingPercentText;
    private bool isLoadingNextLevel = false;

    private void Start()
    {
        loadingScreen.SetActive(false);
        tutorialPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && cabinetDoor.canInteract)
        {
            other.GetComponent<PlayerMovement>().enabled = false;
            other.GetComponent<PlayerAnimations>().enabled = false;
            other.GetComponent<PlayerThrowAxe>().enabled = false;
            other.GetComponent<BuildCampfire>().enabled = false;
            other.GetComponent<PlayerLook>().enabled = false;
            other.GetComponent<Animator>().SetFloat("Velocity", 0f);

            DataPersistenceManager.instance.SaveGame();
            
            AskForTutorial();
        }
    }

    private void AskForTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void GoToTutorial()
    {
        if (isLoadingNextLevel == false)
        {
            StartCoroutine(StartLoading(3));
            isLoadingNextLevel = true;
        }
    }

    public void GoToWilderniss()
    {
        if (isLoadingNextLevel == false)
        {
            StartCoroutine(StartLoading(2));
            isLoadingNextLevel = true;
        }
    }

    IEnumerator StartLoading(int index)
    {
        // Show the loading screen immediately
        loadingScreen.SetActive(true);

        float startTime = Time.time;

        // We don't need a fixed duration here; it depends on the loading progress
        while (true)
        {
            // Calculate progress based on time elapsed
            float progressValue = Mathf.Clamp01((Time.time - startTime) / 3.5f); // Assuming 3.5 seconds max

            // Set the loading text to the progress percentage
            loadingPercentText.text = (progressValue * 100f).ToString("F0") + "%";

            // Check if the loading operation has completed
            if (progressValue >= 1f)
            {
                break;
            }

            yield return null;
        }

        // Start loading the scene after the fixed duration
        yield return StartCoroutine(LoadSceneAsync(index));
    }

    IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f); // Divide by 0.9f for normalization
            loadingPercentText.text = (progressValue * 100f).ToString("F1") + "%";
            yield return null;
        }

        // Ensure that the loading text shows 100% when the scene is fully loaded
        loadingPercentText.text = "100%";
    }
}
