using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private PlayerAnimations playerAnimations;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private float movSpeed = 6f;
    [SerializeField] private float lowerBodyRotationSpeed = 5;

    private float verticalVelocity = 0.0f; 
    private float gravity = -9.81f; 
    

    void Update()
    {
        TryMovement();
        playerGravity();
    }

    public void TryMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical).normalized;

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
            characterController.Move(rotatedMovement * movSpeed * Time.deltaTime);
        }
        playerAnimations.IdleAndWalk(movementDirection.magnitude);
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
}
