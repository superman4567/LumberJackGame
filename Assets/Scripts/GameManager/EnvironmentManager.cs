using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private float fogVar1 = 200;
    [SerializeField] private float fogVar2 = 180;

    private float lerpDuration = 10f;
    private float lerpTimer = 0f;

    void Update()
    {
        lerpTimer += Time.deltaTime;

        float lerpFactor = Mathf.Clamp01(lerpTimer / lerpDuration);
        float lerpedValue = Mathf.Lerp(fogVar1, fogVar2, lerpFactor);

        // Apply the lerped value to fog settings
        RenderSettings.fogDensity = lerpedValue;

        if (lerpFactor >= 1f)
        {
            lerpTimer = 0f;
        }
    }
}
