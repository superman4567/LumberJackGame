using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [Header("Interactable object detection")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionDistance = 3f;
    
    public Action<bool> interactAction;
    public Action onInteracting;
    public Action onFinishedInteracting;

    public InteractableObject currentInteractableObject = null;
    public GameObject currentInteractable = null;

    private bool isInteracting = false;
    private bool holdInteractionType;
    public float interactionTimer = 0f;

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
        InteractTypeCheck();
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
                interactionTimer = 0f;
                if(currentInteractable.TryGetComponent(out InteractableObject a)) 
                {
                    currentInteractableObject = a;
                };
            }
        }
        else
        {
            currentInteractable = null;
            interactionTimer = 0f;
            currentInteractableObject = null;
        }
    }

    private void InteractTypeCheck()
    {
        if (currentInteractable != null) 
        if (Input.GetKey(KeyCode.E))
        {
            interactionTimer += Time.deltaTime;
            
            if (Input.GetKeyDown(KeyCode.E) && !currentInteractableObject.HoldDownType)
            {
                Debug.Log("I am a tap interacting type");
                interactAction?.Invoke(true);
            }

            else if (currentInteractableObject.HoldDownType)
            {
                if(interactionTimer >= currentInteractableObject.holdDuration)
                {
                    Debug.Log("I am a hold interacting type");
                    interactAction?.Invoke(false);
                    onInteracting?.Invoke();
                }
            }
        }

        else if (Input.GetKeyUp(KeyCode.E))
        {
            onFinishedInteracting?.Invoke();
            interactionTimer = 0f;
        }
    }
}