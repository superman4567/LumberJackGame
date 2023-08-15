using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Animator animator;

    private int VelocityHash;
    private int IdleAnimationsHash;

    private float idleAnimation1Frequency = 85f;
    private float timeSinceLastIdleChange = 0f;

    private string interactableTag;

    

    private void OnEnable()
    {
        playerInteraction.InteractionHappening += InteractionAnimations;
    }

    private void OnDisable()
    {
        playerInteraction.InteractionHappening -= InteractionAnimations;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        IdleAnimationsHash = Animator.StringToHash("IdleAnimations");
        VelocityHash = Animator.StringToHash("Velocity");
    }

    private void Update()
    {
        IdleChange();
    }

    private void IdleChange()
    {
        timeSinceLastIdleChange += Time.deltaTime;

        if (timeSinceLastIdleChange >= Random.Range(3f, 6f))
        {
            if (Random.Range(0f, 100f) <= idleAnimation1Frequency)
            {
                animator.SetFloat(IdleAnimationsHash, 0f);
            }
            else
            {
                animator.SetFloat(IdleAnimationsHash, 1f);
            }

            timeSinceLastIdleChange = 0f;
        }
    }

    public void IdleAndWalk(float playerSpeed)
    {
        animator.SetFloat(VelocityHash, playerSpeed);
    }

    private void InteractionAnimations(bool isInteracting)
    {
        if (isInteracting)
        {
            interactableTag = playerInteraction.currentInteractable.tag;
        }
        animator.SetBool(interactableTag, isInteracting);
    }
}
