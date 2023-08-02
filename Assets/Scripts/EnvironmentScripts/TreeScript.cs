using System;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [Header("Tree Physics")]
    [SerializeField] private Rigidbody treeTop;
    [SerializeField] private TreeState currentState;

    public static Action PlayerCanInteract_Phase1Complete;
    public static Action PlayerCanInteract_Phase4Complete;
    public static Action PlayerCanInteract_Phase1CompleteReset;
    public static Action PlayerCanInteract_Phase4CompleteReset;

    private Player player = null;

    private bool treeHasBeenInteractedWith = false;
    private bool fallenTreeHasBeenInteractedWith = false;

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
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        TreeBreakingSound = GetComponent<AudioSource>();

        currentState = TreeState.StandingTree;
        treeTop.isKinematic = true;
    }

    private void OnEnable()
    {
        Player.PlayerCanInteract_Phase1 += State1_TreeHasBeenInteractedWith;
        Player.PlayerCanInteract_Phase2 += State3_FallenTreeHasBeenInteractedWith;
    }

    private void OnDisable()
    {
        Player.PlayerCanInteract_Phase1 -= State1_TreeHasBeenInteractedWith;
        Player.PlayerCanInteract_Phase2 -= State3_FallenTreeHasBeenInteractedWith;
    }

    private void Update()
    {
        TreeStates();
    }

    public void TreeStates()
    {
        switch (currentState)
        {
            case TreeState.StandingTree:
                //When the player script detects that you pressed E you will trigger the next state.
                if (treeHasBeenInteractedWith) 
                {
                    GameManager.Instance.AddWood();
                    currentState = TreeState.FallingTree;
                }
                break;

            case TreeState.FallingTree:
                //Here we activate the tree falling, we activate a method in the player script so taht hte player can interact again, in the if statement we check if the tree is laying still.
                State2_TreeFalling();
                if (State2_IsRigidbodyNotMoving()) 
                {
                    currentState = TreeState.FallenTree; 
                }
                break;

            case TreeState.FallenTree:
                //When the player script detects that you pressed E and notices that the tree has been interacted with, you will trigger the next state.
                if (fallenTreeHasBeenInteractedWith)
                {
                    GameManager.Instance.AddWood();
                    currentState = TreeState.DestroyedTree;
                }
                break;

            case TreeState.DestroyedTree:
                //After you interacted with the tree for the second time the tree will destroy it self, we reset all bools, maybe add a cool shader?
                Invoke("State4_DestroyTree",1f);

                treeHasBeenInteractedWith = false;
                fallenTreeHasBeenInteractedWith = false;
                //reset values
                break;

            default:
                Debug.LogWarning("Unexpected tree state!");
                break;
            
        }
    }

    public void State1_TreeHasBeenInteractedWith()
    {
        treeHasBeenInteractedWith = true;
    }

    public void State2_TreeFalling()
    {
        treeTop.isKinematic = false;
        TreeBreakingSound.Play();
    }

    private bool State2_IsRigidbodyNotMoving()
    {
        return Mathf.Approximately(treeTop.velocity.magnitude, 0f);
    }

    public void State3_FallenTreeHasBeenInteractedWith()
    {
        fallenTreeHasBeenInteractedWith = true;
    }

    public void State4_DestroyTree()
    {
        gameObject.SetActive(false);
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
