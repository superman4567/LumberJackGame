using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Animator animator;

    private int IsDodgingHash;
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

        IsDodgingHash = Animator.StringToHash("DodgeRoll");
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
                animator.SetBool("Idle1", true);
                animator.SetBool("Idle2", false);
            }
            else
            {
                animator.SetBool("Idle2", true);
                animator.SetBool("Idle1", false);
            }

            timeSinceLastIdleChange = 0f;
        }
    }

    public void IdleAndWalk(float playerSpeed)
    {
        animator.SetFloat("Velocity", playerSpeed);
    }

    private void InteractionAnimations(bool isInteracting)
    {
        if (isInteracting)
        {
            interactableTag = playerInteraction.currentInteractable.tag;
            animator.SetBool(interactableTag, isInteracting);
        }
        animator.SetBool(interactableTag, isInteracting);
    }

    public void SetDodgeRollState(bool isDodging)
    {
        animator.SetBool(IsDodgingHash, isDodging);
    }
}
