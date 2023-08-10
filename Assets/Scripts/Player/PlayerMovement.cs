using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController CharacterController;
    [SerializeField] private PlayerAnimations playerAnimations;
    [SerializeField] private float movSpeed = 6f;
    public PlayerLook playerLook;

    void Update()
    {
        TryMovement();
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
            CharacterController.Move(movementDirection * movSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }
}
