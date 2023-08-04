using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private Image radialFill;
    [SerializeField] private Image radialBG;
    [SerializeField] private PlayerInteraction playerInteraction;

    private float normalizedValue;
    private float temp;
    private bool isInteracting = false;

    private void Start()
    {
        radialFill = GetComponent<Image>();
        radialFill.fillAmount = 0;
    }

    private void Update()
    {
        ShowRadialUI();
        ResetRadialUI();
        // Debug.Log(isInteracting);
    }

    private void InteractingTrue()
    {
        isInteracting = true;
    }

    private void InteractingFalse()
    {
        isInteracting = false;
    }

    private void ShowRadialUI()
    {
        if (isInteracting)
        {
            normalizedValue = playerInteraction.holdingDownInteract / playerInteraction.holdDuration;
            // normalizedValue = Mathf.Clamp01(normalizedValue);

            radialFill.fillAmount = normalizedValue; 
        }
    }

    private void ResetRadialUI()
    {
        if (isInteracting == false)
        {
            radialFill.fillAmount = playerInteraction.holdingDownInteract;
        }
    }
}
