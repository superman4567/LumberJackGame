using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.LowLevel;
using Cinemachine;

public class Player : MonoBehaviour
{
    [Header("Character movement")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] private LayerMask floorMask;

    [Header("Placing Trees")]
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactableLayerMask;
    private bool isPlayerCloseEnough = false;

    [Header("Detecting Trees")]
    [SerializeField] private float lookThreshold = 0.9f;
    private GameObject closestTree = null;

    [Header("Interacting Timer")]
    [SerializeField] public float holdDuration = 2.0f; 

    //These 2 values should be stored in the TreeManager
    

    void Update()
    {
        //FindNearbyTrees(value1, value2);
        Interact();
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && closestTree != null)
        {
            
        }
    }

    //private int IncrementValueOnInteraction(float interactionPercentage)
    //{
    //    if (interactionPercentage < 20)
    //    {
    //        interactionIncrement = 0;
    //    }
    //    else if (interactionPercentage == 20 || interactionPercentage > 20 || interactionPercentage < 40 )
    //    {
    //        interactionIncrement = 1;
    //    }
    //    else if (interactionPercentage == 40 || interactionPercentage > 40 || interactionPercentage < 60)
    //    {
    //        interactionIncrement = 2;
    //    }
    //    else if (interactionPercentage == 60 || interactionPercentage > 60 || interactionPercentage < 80)
    //    {
    //        interactionIncrement = 3;
    //    }
    //    else {interactionIncrement = 4;}

    //    return interactionIncrement;
    //}

    private void FindNearbyTrees(int value1, float value2)
    {
        float closestDistance = float.MaxValue;
        closestTree = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance, interactableLayerMask);

        foreach (Collider collider in colliders)
        {
            Vector3 playerToTree = (collider.gameObject.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(playerToTree, transform.forward);

            if (dot >= lookThreshold)
            {
                // Calculate the distance between the player and the tree
                float distance = Vector3.Distance(collider.gameObject.transform.position, transform.position);

                // Update the closest tree if the current tree is closer than the previous closest tree
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTree = collider.gameObject;
                }
            }
        }

        // Set the values on the closest tree (if any)
        if (closestTree != null)
        {
            //Here is where we get the correct script from the closest tree!
            TreeScript treeScript = closestTree.GetComponent<TreeScript>();
            if (treeScript != null)
            {
                //treeScript.interactionIncrement = value1;
                //treeScript.interactionPercentage = value2;
            }
        }
    }

    private void NearbyTreesList()
    {
        List<GameObject> nearbyTrees = TreeManager.Instance.nearbyTrees;
        // You can now iterate through the list and do something with each tree.
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactables"))
        {
            isPlayerCloseEnough = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactables"))
        {
            isPlayerCloseEnough = false;
        }
    }
}


