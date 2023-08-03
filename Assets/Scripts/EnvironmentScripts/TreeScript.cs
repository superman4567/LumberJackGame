using System;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [Header("Tree Physics")]
    [SerializeField] private Rigidbody treeTop;
    [SerializeField] private TreeState currentState;
    public bool treeHasBeenInteractedWith = false;
    public int interactionCount = 0;

    [Header("Tree Audio")]
    [SerializeField] private AudioSource TreeBreakingSound;

    public enum TreeState
    {
        StandingTree,
        FallingTree,
        FallenTree,
        DestroyedTree
    }

    private void Start()
    {
        //GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        TreeBreakingSound = GetComponent<AudioSource>();

        currentState = TreeState.StandingTree;
        treeTop.isKinematic = true;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        TreeStates();
        //Debug.Log(currentState);
    }

    public void TreeStates()
    {
        switch (currentState)
        {
            case TreeState.StandingTree:
                //When the player script detects that you pressed E you will trigger the next state.
                if (treeHasBeenInteractedWith == true && interactionCount == 1) 
                {
                    GameManager.Instance.AddWood();
                    currentState = TreeState.FallingTree;
                }
                break;

            case TreeState.FallingTree:
                //Here we activate the tree falling, in the if statement we check if the tree is laying still.
                treeTop.isKinematic = false;
                TreeBreakingSound.Play(); 


                if (Mathf.Approximately(treeTop.velocity.magnitude, 0f)) 
                {
                    currentState = TreeState.FallenTree;
                    interactionCount++;
                }
                break;

            case TreeState.FallenTree:
                //When the player script detects that you pressed E and notices that the tree has been interacted with, you will trigger the next state.
                if (treeHasBeenInteractedWith == true && interactionCount == 2)
                {
                    GameManager.Instance.AddWood();
                    currentState = TreeState.DestroyedTree;
                }
                break;

            case TreeState.DestroyedTree:
                //After you interacted with the tree for the second time the tree will destroy it self, we reset all bools, maybe add a cool shader?
                interactionCount++;
                Destroy(gameObject);

                break;

            default:
                Debug.LogWarning("Unexpected tree state!");
                break;
            
        }
    }

    //private int IncrementValueOnInteraction(float interactionPercentage)
    //{
    //    if (interactionPercentage < 20)
    //    {
    //        interactionIncrement = 0;
    //    }
    //    else if (interactionPercentage == 20 || interactionPercentage > 20 || interactionPercentage < 40)
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
    //    else { interactionIncrement = 4; }

    //    return interactionIncrement;
    //}
}
