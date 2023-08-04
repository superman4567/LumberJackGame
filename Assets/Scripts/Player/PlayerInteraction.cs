using JetBrains.Annotations;
using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [Header("Interactable Interaction")]
    [SerializeField] private LayerMask interactableLayerMask;

    public Action OnStartInteract;
    public Action OnStopInteract;

    private IInteractable interactableObject;

    [Header("Interacting Timer")]
    public float holdDuration = 2.0f;
    public float holdingDownInteract = 0f;

    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (interactableObject == null) return;

        if (holdingDownInteract > holdDuration)
        {
            interactableObject.InteractComplete();
            interactableObject = null;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactableObject.StartInteract();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            interactableObject.StopInteract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interact))
        {
            interactableObject = interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interact))
        {
            if (interact == interactableObject)
            {
                interactableObject = null;
            }
        }
    }

    public IInteractable GetInteractable() => interactableObject;
}
