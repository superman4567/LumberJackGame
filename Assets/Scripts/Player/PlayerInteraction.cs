using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [Header("Interactable Interaction")]
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactableLayerMask;

    public Action onInteracting;
    public Action onFinishedInteracting;
    public Action onSwitchState;

    private TreeScript treeScript;
    private bool canInteract = false;
    private bool interacting = false;
    

    [Header("Interacting Timer")]
    public float holdDuration = 2.0f;
    public float holdingDownInteract = 0f;

    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (Input.GetKey(KeyCode.E) && canInteract && treeScript.interactionPhase == 0)
        {
            //Invoke Action to show and fill radial UI 
            if (onInteracting != null) { onInteracting.Invoke(); }

            if (!interacting)
            {
                interacting = true;
                treeScript.interactionSeconds =  holdingDownInteract = 0f;
            }

            holdingDownInteract += Time.deltaTime;

            if (holdingDownInteract >= holdDuration)
            {
                treeScript.interactionPhase = 1;

                //Make sure to increase the interactionCount in the TreeScript
                if (treeScript != null) { onSwitchState.Invoke(); }
            }
        }

        else if (Input.GetKey(KeyCode.E) && canInteract && treeScript.interactionPhase == 2)
        {
            //Invoke Action to show and fill radial UI and upadte the increment amount on the tree you are interacting with
            if (onInteracting != null) { onInteracting.Invoke(); }

            if (!interacting)
            {
                interacting = true;
                holdingDownInteract = 0f;
            }

            holdingDownInteract += Time.deltaTime;

            if (holdingDownInteract >= holdDuration)
            {
                treeScript.interactionPhase = 2;

                //Make sure to increase the interactionCount in the TreeScript
                if (treeScript != null) { onSwitchState.Invoke(); }
            }
        }

        else if (Input.GetKeyUp(KeyCode.E) && treeScript.interactionPhase == 0)
        {
            interacting = false;
            holdingDownInteract = 0f;

            //Invoke Action to stop incrementing and !reset! radial UI, reset should be changed to remember the increment value and use that
            if (onFinishedInteracting != null) { onFinishedInteracting.Invoke(); }
        }
    }

    public TreeScript GetTreeScript() => treeScript;


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
