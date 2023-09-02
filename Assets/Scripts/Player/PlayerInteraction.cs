using JetBrains.Annotations;
using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [Header("Interactable object detection")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private Animator animator;

    public Action<bool> InteractionHappening;
    public Interactable currentInteractableObject = null;
    public GameObject currentInteractable = null;


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
        float closestDot = .7f;
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
                if(currentInteractable.TryGetComponent(out Interactable interactableComponent))
                {
                    currentInteractableObject = interactableComponent;
                }
            }
        }
        else
        {
            currentInteractable = null;
            currentInteractableObject = null;
        }
    }

    private void Interact()
    {
        if (currentInteractable != null)
        {
            if (Input.GetKey(KeyCode.E) && currentInteractableObject.canBeInteractedWith)
            {
                InteractionHappening?.Invoke(true);
                if (currentInteractable.tag == "Chest")
                {
                    currentInteractableObject.GetComponentInChildren<Chest>().ChestInteract();
                    currentInteractableObject.AddProgress(Time.deltaTime);
                }
                else
                {
                    currentInteractableObject.AddProgress(Time.deltaTime);
                }

                if (currentInteractableObject.CheckProgressComplete())
                {
                    currentInteractableObject?.InteractComplete();
                    InteractionHappening?.Invoke(false);
                }
            }
        }
        else
        {
            animator.SetBool("Tree", false);
            animator.SetBool("Chest", false);
        }
    }
}
