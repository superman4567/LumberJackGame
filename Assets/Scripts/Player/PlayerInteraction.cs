using Cinemachine;
using JetBrains.Annotations;
using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [Header("Interactable object detection")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private PlayerThrowAxe playerThrowAxe;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Color blockedColor;

    [Header("Interactable zoom")]
    [SerializeField] private float defaultzoom = 15f;
    [SerializeField] private float interactZoom = 6f;
    [SerializeField] private GameObject lookAtMe;
    private CinemachineFramingTransposer transposer;
    

    public Action<bool> InteractionHappening;
    public Interactable currentInteractableObject = null;
    public GameObject currentInteractable = null;
    private GameObject lastInteractedObject = null;



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

    private void Start()
    {
        lookAtMe.SetActive(true);
        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        ClosestInteractableCheck();
        Interact();
    }

    private void ClosestInteractableCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance, interactableLayer);
        float closestDot = .5f;
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
                if (currentInteractable.TryGetComponent(out Interactable interactableComponent))
                {
                    currentInteractableObject = interactableComponent;
                    if (currentInteractableObject != null)
                    {
                        // Set the color to white when looking at the object
                        currentInteractableObject.GetComponentInParent<InteractPanel>().circle.color = Color.white;
                    }
                    else
                    {
                        currentInteractableObject.GetComponentInParent<InteractPanel>().circle.color = blockedColor;
                    }
                }
                // Update the last interacted object
                lastInteractedObject = currentInteractable;
            }
        }
        else
        {
            currentInteractable = null;
            currentInteractableObject = null;
            
            if (lastInteractedObject != null)
            {
                lastInteractedObject.GetComponentInParent<InteractPanel>().circle.color = blockedColor;
            }
        }
    }

    private void Interact()
    {
        if (currentInteractable != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && currentInteractableObject.canBeInteractedWith)
            {
                //Chest stuff
                if (currentInteractable.tag == "Chest" && !currentInteractable.GetComponentInChildren<Chest>().isOpen)
                {
                    //InteractionHappening?.Invoke(true);
                    animator.SetBool("Chest", true);
                    transposer.m_CameraDistance = interactZoom;
                    lookAtMe.SetActive(false);
                    currentInteractableObject.GetComponentInChildren<Chest>().ChestInteract();
                    currentInteractableObject.AddProgress(Time.deltaTime);

                    Invoke("ResetChestValues", 1.6f);
                }
            }

            if (Input.GetKey(KeyCode.E) && currentInteractableObject.canBeInteractedWith)
            {
                //Tree stuff
                if (currentInteractable.tag == "Tree" && currentInteractable.GetComponentInChildren<Tree>().canBeInteractedWith && !playerThrowAxe.isAxeThrown)
                {
                    InteractionHappening?.Invoke(true);
                    currentInteractableObject.AddProgress(Time.deltaTime);
                }
                //Done interacting
                if (currentInteractableObject.CheckProgressComplete() && (currentInteractable.tag != "Chest"))
                {
                    currentInteractableObject?.InteractComplete();
                    InteractionHappening?.Invoke(false);
                }
            }
            else
            {
                ResetTreeValues();
            }
        }

        else
        {
            ResetTreeValues();
        }
    }

    private void ResetTreeValues()
    {
        lookAtMe.SetActive(true);
        animator.SetBool("Tree", false);
    }

    private void ResetChestValues()
    {
        transposer.m_CameraDistance = defaultzoom;
        animator.SetBool("Chest", false);
    }
}
