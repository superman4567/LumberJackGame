using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private float fogVar1 = 200;
    [SerializeField] private float fogVar2 = 180;

    private float lerpDuration = 10f;
    private float lerpTimer = 0f;
    private bool isLerpingForward = true; // Tracks the current lerp direction

    void Update()
    {
        lerpTimer += Time.deltaTime;

        float lerpFactor = Mathf.Clamp01(lerpTimer / lerpDuration);

        if (!isLerpingForward)
        {
            lerpFactor = 1 - lerpFactor; // Reverse the lerp factor when going backward
        }

        float lerpedValue = Mathf.Lerp(fogVar1, fogVar2, lerpFactor);

        // Apply the lerped value to fog settings
        RenderSettings.fogEndDistance = lerpedValue;

        if (lerpFactor >= 1f)
        {
            lerpTimer = 0f;
            isLerpingForward = !isLerpingForward; // Invert the lerp direction
        }
    }
}
