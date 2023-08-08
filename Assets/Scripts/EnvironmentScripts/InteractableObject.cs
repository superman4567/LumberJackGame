using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float holdDuration = 2f;
    public bool HoldDownType;

    private void OnEnable()
    {
        if (PlayerInteraction.Instance != null)
        {
            PlayerInteraction.Instance.interactAction += HandleInteractAction;
        }
        else
        {
            Debug.LogWarning("PlayerInteraction.Instance is not set.");
        }
    }

    private void OnDisable()
    {
       if (PlayerInteraction.Instance != null)
        {
            PlayerInteraction.Instance.interactAction -= HandleInteractAction;
        }
    }

    private void HandleInteractAction(bool isHoldInteraction)
    {
        if (isHoldInteraction)
        {
            //switch (typeOfInteractable)
            //{
            //    case HoldDownType:
            //}

        }
        else
        {
            Debug.Log("Tap interaction triggered for " + gameObject.name);
            // Perform tap interaction actions
        }
    }
}