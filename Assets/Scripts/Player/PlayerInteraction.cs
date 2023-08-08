using JetBrains.Annotations;
using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }


    public Action OnStartInteract;
    public Action OnStopInteract;

    private Interactable objectWeAreInteractingWith;

    [Header("Interacting Timer")]
    public float holdDuration = 2.0f;
    public float holdingDownInteract = 0f;

    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (objectWeAreInteractingWith == null) return;

        if (holdingDownInteract > holdDuration)
        {
            objectWeAreInteractingWith.InteractComplete();
            objectWeAreInteractingWith = null;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            objectWeAreInteractingWith.StartInteract();
            OnStartInteract?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            objectWeAreInteractingWith.StopInteract();
            OnStopInteract?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactable interact))
        {
            objectWeAreInteractingWith = interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactable interact))
        {
            if (interact == objectWeAreInteractingWith)
            {
                objectWeAreInteractingWith = null;
            }
        }
    }

    public Interactable GetInteractable() => objectWeAreInteractingWith;
}
