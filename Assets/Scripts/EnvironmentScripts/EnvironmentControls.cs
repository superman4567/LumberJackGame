using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentControls : MonoBehaviour
{
    [SerializeField] private float fogNormal = 200;
    [SerializeField] private float fogStorm = 30;
    [SerializeField] private float timeRemaining = 3;
    private bool nightTimerFinished;
    private float currentTime = 0f, duration = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTillNIght();
        
    }

    private void CountDownTillNIght()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            nightTimerFinished = false;
        }
        else 
        { 
            nightTimerFinished = true; 
        }

        if (nightTimerFinished == false)
        {
            RenderSettings.fogEndDistance = fogNormal;
        }
        else
        {
            Debug.Log(currentTime);
            currentTime += Time.deltaTime;
            float fogtransition = Mathf.Lerp(fogNormal, fogStorm, currentTime / duration);

            RenderSettings.fogEndDistance = fogtransition;
        }
    }
}
