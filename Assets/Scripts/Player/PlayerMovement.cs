using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] float movSpeed = 6f;
    public PlayerLook playerLook;

    private int VelocityHash;
    private int IdleAnimations;
    private float idleAnimation1Frequency = 70f;

    private float timeSinceLastIdleChange = 0f;
    private bool isIdleAnimation1Playing = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        VelocityHash = Animator.StringToHash("Velocity");
        IdleAnimations = Animator.StringToHash("IdleAnimations");
    }

    void Update()
    {
        Movement();
        playerLook.PlayerLookDir();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude == 0f)
        {
            IdleChange();
            animator.SetFloat(VelocityHash, 0f);
        }
        else if (direction.magnitude >= 0.1f)
        {
            // Use the forward direction (world space) for player movement.
            Vector3 movementDirection = transform.forward * vertical + transform.right * horizontal;
            controller.Move(movementDirection * movSpeed * Time.deltaTime);
            animator.SetFloat(VelocityHash, direction.magnitude);
        }
    }

    private void IdleChange()
    {
        timeSinceLastIdleChange += Time.deltaTime;

        // Check if it's time to change the idle animation
        if (timeSinceLastIdleChange >= UnityEngine.Random.Range(3f, 6f))
        {
            // Randomly select the next idle animation
            if (UnityEngine.Random.Range(0f, 100f) <= idleAnimation1Frequency)
            {
                animator.SetFloat(IdleAnimations, 0f);
                isIdleAnimation1Playing = true;
            }
            else
            {
                animator.SetFloat(IdleAnimations, 1f);
                isIdleAnimation1Playing = false;
            }

            timeSinceLastIdleChange = 0f;
        }
    }
}