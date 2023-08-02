using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Interactable Interaction")]
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactableLayerMask;

    public static Action PlayerCanInteract_Phase1;
    public static Action PlayerCanInteract_Phase2;

    private TreeScript closestInteractable = null;
    private bool isPlayerCloseEnough = false;
    private bool isTreeStanding = true;
    private bool isTreeLaying = false;

    [Header("Interacting Timer")]
    [SerializeField] public float holdDuration = 2.0f;

    void Update()
    {
        Interact_StandingTree();
        InteractFallenTree();
    }

    private void Interact_StandingTree()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerCloseEnough == true && isTreeStanding == true)
        {
            if (PlayerCanInteract_Phase1 != null)
            {
                if (closestInteractable != null) {closestInteractable.State1_TreeHasBeenInteractedWith();}
            }
        }
        else
        {
            //how to change these values per tree
            isTreeStanding = false;
        }
    }

    private void InteractFallenTree()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerCloseEnough == true && isTreeLaying == true)
        {
            if (PlayerCanInteract_Phase2 != null)
            {
                if (closestInteractable != null) { closestInteractable.State3_FallenTreeHasBeenInteractedWith(); }
            }
        }
        else
        {
            //how to change these values per tree
            isTreeStanding = false;
            isTreeLaying = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TreeScript interactable = other.GetComponent<TreeScript>();
        if (interactable != null)
        {
            isPlayerCloseEnough = true;
            closestInteractable = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TreeScript interactable = other.GetComponent<TreeScript>();
        if (interactable != null && interactable == closestInteractable)
        {
            isPlayerCloseEnough = false;
            closestInteractable = null;
        }
    }

    private TreeScript GetClosestInteractable()
    {
        // You don't need to make any changes here.
        // The detection field will update the 'closestInteractable' variable in OnTriggerEnter and OnTriggerExit.
        // You can still use the 'closestInteractable' variable here to get the closest interactable.
        return closestInteractable;
    }
}
