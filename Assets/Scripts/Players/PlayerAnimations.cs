using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour, IDataPersistance
{
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Animator animator;

    private int IsDodgingHash;
    private float idleAnimation1Frequency = 85f;
    private float timeSinceLastIdleChange = 0f;

    public bool tier1Unlocked = false;
    public bool tier2Unlocked = false;

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

        if (tier1Unlocked)
        {
            ThrowTier1Unlocked();
        }
        if (tier2Unlocked)
        {
            ThrowTier2Unlocked();
        }
    }

    public void LoadData(GameData data)
    {
        this.tier1Unlocked = data.throwspeedTier1Unlocked;
        this.tier2Unlocked = data.throwspeedTier2Unlocked;
    }

    public void SaveData(GameData data)
    {
        data.throwspeedTier1Unlocked = this.tier1Unlocked;
        data.throwspeedTier2Unlocked = this.tier2Unlocked;
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

    public void ThrowTier1Unlocked()
    {
        animator.SetBool("ThrowTier1Unlocked", true);
    }

    public void ThrowTier2Unlocked()
    {
        animator.SetBool("ThrowTier2Unlocked", true);
    }
}
