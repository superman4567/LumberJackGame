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

    private float verticalVelocity = 0.0f; 
    private float gravity = -9.81f; 
    

    void Update()
    {
        TryMovement();
        playerGravity();
        playerLook.PlayerLookDir();
    }

    public bool TryMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        playerAnimations.IdleAndWalk(direction.magnitude);

        if (direction.magnitude >= 0.1f)
        {
            Vector3 movementDirection = transform.forward * vertical + transform.right * horizontal;
            characterController.Move(movementDirection * movSpeed * Time.deltaTime);
            return true;
        }
        return false;
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
