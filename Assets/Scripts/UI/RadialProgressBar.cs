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

    private void OnEnable()
    {
        playerInteraction.OnStartInteract += InteractingTrue;
        playerInteraction.OnStopInteract += InteractingFalse;
    }

    private void OnDisable()
    {
        playerInteraction.OnStartInteract -= InteractingTrue;
        playerInteraction.OnStopInteract -= InteractingFalse;
    }

    private void Update()
    {
        ShowRadialUI();
        ResetRadialUI();
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
        if (this.isInteracting)
        {
            normalizedValue = playerInteraction.currentInteractable.GetComponent<InteractableObject>().holdDuration / playerInteraction.interactionTimer;
            normalizedValue = Mathf.Clamp01(normalizedValue);

            radialFill.fillAmount = normalizedValue; 
        }
    }

    private void ResetRadialUI()
    {
        if (this.isInteracting == false)
        {
            radialFill.fillAmount = playerInteraction.interactionTimer;
        }
    }
}
