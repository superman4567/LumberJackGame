using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private float fogNormal = 200;
    [SerializeField] private float fogStorm = 30;
    [SerializeField] public float timeRemaining = 3;

    public Action stormStart;

    private bool nightTimerFinished = false; // Add a flag to track if transition started
    private float currentTime = 0f, duration = 2f;

    void Update()
    {
        CountDownTillNIght();
    }

    private void CountDownTillNIght()
    {
        if (!nightTimerFinished)
        {
            timeRemaining -= Time.deltaTime;

            // Check if the countdown is still running
            if (timeRemaining > 0)
            {
                RenderSettings.fogEndDistance = fogNormal;
            }
            // Check if the transition hasn't started yet and the countdown reached zero
            else if (!nightTimerFinished && timeRemaining <= 0)
            {
                nightTimerFinished = true; // Set the flag to indicate transition has started
                stormStart.Invoke();
            }

            // If the transition started, update fog distance based on currentTime and duration
            if (nightTimerFinished)
            {
                currentTime += Time.deltaTime;
                float fogtransition = Mathf.Lerp(fogNormal, fogStorm, currentTime / duration);
                RenderSettings.fogEndDistance = fogtransition;
            }
        }
    }
}
