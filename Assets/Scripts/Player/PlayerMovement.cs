using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerAnimations playerAnimations;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] public float movSpeed = 6f;
    [SerializeField] private float lowerBodyRotationSpeed = 5;

    [Header("Sprinting")]
    [SerializeField] public float sprintSpeed = 10f;
    [SerializeField] private float sprintStaminaDrain;

    [Header("Dodgeroll")]
    [SerializeField] private float dodgeRollDistance = 5f;
    [SerializeField] private float dodgeRollDuration = 0.5f;
    [SerializeField] private float dodgeRollCooldown = 2f;

    private Vector3 dodgeRollStartPosition;
    private bool isDodgeRolling = false;
    private float dodgeRollStartTime = 0f;
    private float verticalVelocity = 0.0f;
    private float gravity = -9.81f;


    void Update()
    {
        SprintingCost();
        DodgeRoll();
    }

    private void LateUpdate()
    {
        Movement();
        playerGravity();
    }

    public void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical).normalized;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && playerStats.stamina >= sprintStaminaDrain;

        float currentSpeed = isSprinting ? sprintSpeed : movSpeed;

        if (movementDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation, lowerBodyRotationSpeed * Time.deltaTime
            );

            // Apply the rotation to the movement direction
            Vector3 rotatedMovement = targetRotation * Vector3.forward;

            // Move the character using the rotated movement direction
            characterController.Move(rotatedMovement * currentSpeed * Time.deltaTime);
        }

        playerAnimations.IdleAndWalk(movementDirection.magnitude);
    }

    public void SprintingCost()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical).normalized;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) &&
                          playerStats.stamina >= sprintStaminaDrain &&
                          movementDirection.magnitude > 0.1f; 

        if (isSprinting)
        {
            playerAnimations.IdleAndWalk(2f);
            playerStats.stamina -= sprintStaminaDrain * Time.deltaTime;
        }
    }

    private void playerGravity()
    {
        if (characterController.isGrounded) 
        {
            verticalVelocity = 0.0f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 moveDirection = new Vector3(0.0f, verticalVelocity, 0.0f);
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void DodgeRoll()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerStats.stamina >= 5f)
        {
            if (isDodgeRolling) return;

            if (Time.time - dodgeRollStartTime >= dodgeRollCooldown)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");

                if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 0.1f)
                {
                    Vector3 dodgeDirection = new Vector3(horizontal, 0f, vertical).normalized;

                    dodgeRollStartPosition = transform.position;
                    StartCoroutine(StartDodgeRoll(dodgeDirection));

                    playerAnimations.SetDodgeRollState(true);
                    playerStats.stamina -= 5f;
                }
            }
        }
    }

    private IEnumerator StartDodgeRoll(Vector3 dodgeDirection)
    {
        isDodgeRolling = true;
        dodgeRollStartTime = Time.time;

        float elapsedTime = 0f;

        while (elapsedTime < dodgeRollDuration)
        {
            float t = elapsedTime / dodgeRollDuration;
            Vector3 newPosition = Vector3.Lerp(
                dodgeRollStartPosition, dodgeRollStartPosition + dodgeDirection * dodgeRollDistance, t
            );
            characterController.Move(newPosition - transform.position);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDodgeRolling = false;

        playerAnimations.SetDodgeRollState(false);
    }
}
