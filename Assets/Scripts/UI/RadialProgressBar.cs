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

    private void OnEnable()
    {
        playerInteraction.InteractionHappening += UpdateAndShowRadialUI;
    }

    private void OnDisable()
    {
        playerInteraction.InteractionHappening -= UpdateAndShowRadialUI;
    }

    private void Start()
    {
        radialFill.color = Color.clear;
        radialBG.color = Color.clear;

        radialFill = GetComponent<Image>();
        radialFill.fillAmount = 0;
    }

    private void Update()
    {
        //Debug.Log(normalizedValue);
    }

    private void UpdateAndShowRadialUI(bool isInteracting)
    {
        if (isInteracting)
        {
            radialFill.color = Color.white;
            radialBG.color = Color.white;

            normalizedValue = playerInteraction.currentInteractable.GetComponent<Interactable>().savedProgressInSeconds / playerInteraction.currentInteractable.GetComponent<Interactable>().interactInSeconds;

            radialFill.fillAmount = normalizedValue;
            
        }
        else
        {
            radialFill.color = Color.clear;
            radialBG.color = Color.clear;
        }
    }
}
