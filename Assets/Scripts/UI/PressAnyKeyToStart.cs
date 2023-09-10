using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PressAnyKeyToStart : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingBarFill;
    [SerializeField] private TextMeshProUGUI loadingPercentText;
    private bool isLoadingNextLevel = false;

    private void Start()
    {
        loadingScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKey && isLoadingNextLevel == false)
        {
            DataPersistenceManager.instance.SaveGame();
            isLoadingNextLevel = true;
            StartCoroutine(StartLoading());
        }
    }

    IEnumerator StartLoading()
    {
        // Show the loading screen immediately
        loadingScreen.SetActive(true);

        float startTime = Time.time;
        float loadingDuration = 3.5f; // Duration in seconds

        while (Time.time - startTime < loadingDuration)
        {
            // Calculate progress based on time elapsed
            float progressValue = Mathf.Clamp01((Time.time - startTime) / loadingDuration);
            loadingBarFill.fillAmount = progressValue;
            loadingPercentText.text = (progressValue * 100f).ToString("F0") + "%";
            yield return null;
        }

        // Start loading the scene after the fixed duration
        yield return StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            // You can still update the loading bar based on operation.progress here
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            loadingPercentText.text = (progressValue * 100f).ToString("F1") + "%";
            yield return null;
        }
    }
}
