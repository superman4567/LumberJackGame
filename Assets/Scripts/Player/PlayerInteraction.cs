using JetBrains.Annotations;
using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [Header("Interactable object detection")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionDistance = 3f;

    public Interactable currentInteractableObject = null;
    public GameObject currentInteractable = null;

    private bool isInteracting = false;
    private bool holdInteractionType;
    public float holdingDownInteract = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        ClosestInteractableCheck();
        Interact();
    }

    private void ClosestInteractableCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance, interactableLayer);
        float closestDot = -1f;
        GameObject closestInteractable = null;

        foreach (Collider collider in colliders)
        {
            Vector3 directionToCollider = collider.transform.position - transform.position;
            directionToCollider.Normalize();

            float dot = Vector3.Dot(transform.forward, directionToCollider);

            if (dot > closestDot)
            {
                closestDot = dot;
                closestInteractable = collider.gameObject;
            }
        }

        if (closestInteractable != null)
        {
            if (closestInteractable != currentInteractable)
            {
                currentInteractable = closestInteractable;
                holdingDownInteract = 0f;
                if(currentInteractable.TryGetComponent(out Interactable a)) 
                {
                    currentInteractableObject = a;
                };
            }
        }
        else
        {
            currentInteractable = null;
            holdingDownInteract = 0f;
            currentInteractableObject = null;
        }
    }

    private void Interact()
    {
        if (currentInteractable != null)
        if (Input.GetKey(KeyCode.E))
        {
            holdingDownInteract += Time.deltaTime;
        }   
    }

    public Interactable GetInteractable() => currentInteractableObject;
}
