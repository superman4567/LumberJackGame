using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private Image radialFill;
    [SerializeField] private Image radialBG;
    [SerializeField] private PlayerInteraction playerInteraction;

    private float normalizedValue;
    private bool isInteracting = false;

    private void Start()
    {
        radialFill = GetComponent<Image>();
        radialFill.fillAmount = 0;
    }

    private void OnEnable()
    {
        playerInteraction.onInteracting += InteractingTrue;
        playerInteraction.onFinishedInteracting += InteractingFalse;
    }

    private void OnDisable()
    {
        playerInteraction.onInteracting -= InteractingTrue;
        playerInteraction.onFinishedInteracting -= InteractingFalse;
    }

    private void Update()
    {
        ShowRadialUI();
        ResetRadialUI();
        Debug.Log(isInteracting);
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
            normalizedValue = playerInteraction.holdingDownInteract / playerInteraction.holdDuration;
            normalizedValue = Mathf.Clamp01(normalizedValue);

            radialFill.fillAmount = normalizedValue; 
        }
    }

    private void ResetRadialUI()
    {
        if (this.isInteracting == false)
        {
            radialFill.fillAmount = playerInteraction.holdingDownInteract;
        }
    }
}
