using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private Image radialFill;
    [SerializeField] private Image radialBG;
    [SerializeField] private PlayerInteraction playerInteraction;

    private float normalizedValue;

    private void Start()
    {
        radialFill.color = Color.clear;
        radialBG.color = Color.clear;

        radialFill = GetComponent<Image>();
        radialFill.fillAmount = 0;
    }

    public void UpdateAndShowRadialUI(bool isInteracting)
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
