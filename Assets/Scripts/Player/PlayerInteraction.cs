using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interactable Interaction")]
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactableLayerMask;

    public Action onInteracting;
    public Action onFinishedInteracting;

    private TreeScript treeScript;
    private bool canInteract = false;
    private bool interacting = false;
    public float holdingDownInteract = 0f;

    [Header("Interacting Timer")]
    [SerializeField] public float holdDuration = 2.0f;

    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (Input.GetKey(KeyCode.E) && canInteract && treeScript.interactionCount == 0)
        {
            //Invoke Action to show and fill radial UI
            if (onInteracting != null) { onInteracting.Invoke(); }

            if (!interacting)
            {
                interacting = true;
                holdingDownInteract = 0f;
            }

            holdingDownInteract += Time.deltaTime;

            if (holdingDownInteract >= holdDuration)
            {
                treeScript.interactionCount = 1;
                
                Debug.Log("Interacted with the tree!");
            }
        }

        else if (Input.GetKey(KeyCode.E) && canInteract && treeScript.interactionCount == 2)
        {
            //Invoke Action to show and fill radial UI
            if (onInteracting != null) { onInteracting.Invoke(); }

            if (!interacting)
            {
                interacting = true;
                holdingDownInteract = 0f;
            }

            holdingDownInteract += Time.deltaTime;

            if (holdingDownInteract >= holdDuration)
            {
                treeScript.interactionCount = 2;
                holdingDownInteract = 0f;
                
                Debug.Log("Interacted with the tree on the ground!");
            }
        }

        else if (Input.GetKeyUp(KeyCode.E) && treeScript.interactionCount == 0)
        {
            interacting = false;
            holdingDownInteract = 0f;

            //Invoke Action to reset radial UI
            if (onFinishedInteracting != null) { onFinishedInteracting.Invoke(); }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Get the TreeScript component from the colliding tree object
        treeScript = other.GetComponent<TreeScript>();

        if (treeScript != null)
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (treeScript != null && other.GetComponent<TreeScript>() == treeScript)
        {
            canInteract = false;
        }
    }
}
