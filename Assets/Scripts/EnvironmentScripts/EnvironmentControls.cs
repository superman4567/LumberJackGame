using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentControls : MonoBehaviour
{
    [SerializeField] private float fogNormal = 200;
    [SerializeField] private float fogStorm = 30;
    [SerializeField] private float timeRemaining = 3;

    public Action stormStart;

    private bool nightTimerFinished;
    private float currentTime = 0f, duration = 2f;


    void Update()
    {
        CountDownTillNIght();
    }

    private void CountDownTillNIght()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining > 0)
        {
            RenderSettings.fogEndDistance = fogNormal;
        }
        else if (timeRemaining <= 0)
        {
            currentTime += Time.deltaTime;
            float fogtransition = Mathf.Lerp(fogNormal, fogStorm, currentTime / duration);

            RenderSettings.fogEndDistance = fogtransition;
            stormStart.Invoke();
        }
    }
}
