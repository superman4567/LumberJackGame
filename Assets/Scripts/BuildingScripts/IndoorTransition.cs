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
        StartCoroutine(StartLoading(3));
    }

    public void GoToWilderniss()
    {
        StartCoroutine(StartLoading(2));
    }

    IEnumerator StartLoading(int index)
    {
        // Show the loading screen immediately
        loadingScreen.SetActive(true);

        float startTime = Time.time;
        float loadingDuration = 3.5f; // Duration in seconds

        while (Time.time - startTime < loadingDuration)
        {
            // Calculate progress based on time elapsed
            float progressValue = Mathf.Clamp01((Time.time - startTime) / loadingDuration);
            loadingPercentText.text = (progressValue * 100f).ToString("F0") + "%";
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
            // You can still update the loading bar based on operation.progress here
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingPercentText.text = (progressValue * 100f).ToString("F1") + "%";
            yield return null;
        }
    }
}
