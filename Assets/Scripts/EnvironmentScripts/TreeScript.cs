using System;
using System.Collections;
using UnityEngine;

public class TreeScript : MonoBehaviour, Interactable
{
    [Header("Tree Audio")]
    [SerializeField] private AudioSource TreeBreakingSound;

    [Header("Tree Physics")]
    [SerializeField] private Rigidbody treeTop;
    [SerializeField] private TreeState currentState;

    private PlayerInteraction playerInteraction;
    public bool doneFalling = false;
    private bool startIncrementing = false;
    private bool resetIncrement = false;
    public int interactionPhase = 0;
    public int interactionIncrement = 0;
    public float interactionSeconds = 0;
    private float fallingTimer = 0.2f;
    private float newTimer = 0f;

    public enum TreeState
    {
        StandingTree,
        FallingTree,
        FallenTree,
        DestroyedTree
    }

    private void Awake()
    {
        TreeBreakingSound = GetComponent<AudioSource>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        currentState = TreeState.StandingTree;
        treeTop.isKinematic = true;
    }

    private void Update()
    {
        interactionSeconds = playerInteraction.holdingDownInteract;

        TreeStates();
        IncrementValueOnInteraction();

        if (playerInteraction.GetInteractable() == (Interactable)this) 
        { 
            Debug.Log(interactionIncrement); 
            Debug.Log(currentState); 
        }
    }

    public void TreeStates()
    {
        switch (currentState)
        {
            case TreeState.StandingTree:
                //When the player script detects that you pressed E you will trigger the next state.
                if (interactionIncrement == 3 && interactionPhase == 1) 
                {
                    GameManager.Instance.AddWood();
                    currentState = TreeState.FallingTree;
                }
                break;

            case TreeState.FallingTree:
                //Here we activate the tree falling, in the if statement we check if the tree is laying still.
                treeTop.isKinematic = false;
                //Check the AUDIO!
                TreeBreakingSound.Play();

                startIncrementing = false;
               
                newTimer += Time.deltaTime;

                if(newTimer > fallingTimer)
                {
                    if (Mathf.Approximately(treeTop.velocity.magnitude, 0f))
                    {
                        playerInteraction.holdingDownInteract = 0f;
                        interactionPhase++;
                        doneFalling = true;
                        interactionIncrement = 0;
                        currentState = TreeState.FallenTree;
                    }
                }
                break;

            case TreeState.FallenTree:
                //When the player script detects that you pressed E and notices that the tree has been interacted with, you will trigger the next state.
                if (interactionIncrement == 3 && doneFalling == true && interactionPhase == 2)
                {
                    resetIncrement = true;
                    GameManager.Instance.AddWood();
                    currentState = TreeState.DestroyedTree;
                }
                break;

            case TreeState.DestroyedTree:
                //After you interacted with the tree for the second time the tree will destroy it self, we reset all bools, maybe add a cool shader?
                Destroy(gameObject);
                resetIncrement = true;

                break;

            default:
                Debug.LogWarning("Unexpected tree state!");
                break;
            
        }
    }

    private void StartIncrementing()
    {
        if (playerInteraction.GetInteractable() != (Interactable)this) { return; }

        startIncrementing = true;
    }

    private void StopIncrementing()
    {
        startIncrementing = false;
    }

    private void IncrementValueOnInteraction()
    {
        float interactionPercentage = interactionSeconds / playerInteraction.holdDuration;
        if (startIncrementing)
        {
            if (interactionPercentage < 0.25)
            {
                interactionIncrement = 0;
            }

            else if (interactionPercentage < 0.50)
            {
                interactionIncrement = 1;
            }

            else if (interactionPercentage < 0.75)
            {
                interactionIncrement = 2;
            }

            else if (interactionPercentage >= 1)
            {
                interactionIncrement = 3;

                if (resetIncrement)
                {
                    startIncrementing = false;
                    interactionIncrement = 0;

                }
            }
        }
    }

    void Interactable.InteractComplete()
    {
        throw new NotImplementedException();
    }

    void Interactable.StartInteract()
    {
        throw new NotImplementedException();
    }

    void Interactable.StopInteract()
    {
        throw new NotImplementedException();
    }
}
