using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class PlayerThrowAxe : MonoBehaviour
{
    [SerializeField] private GameObject axe; // Reference to the existing axe GameObject
    [SerializeField] private Transform throwSpawnPoint; // This should be a child of the player
    [SerializeField] private Rigidbody axeRb;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float returnSpeed = 5f;

    private bool isAxeThrown = false; // Track whether the axe is thrown
    private bool isAxeLanded = false;
    private Vector3 axeLandingLocation; // Store the landing location

    private Vector3 initialAxePosition; // Store initial position for returning

    private void Start()
    {
        isAxeLanded = false;
        axeRb.useGravity = false;
        initialAxePosition = throwSpawnPoint.transform.position;
    }

    private void Update()
    {
        if (!isAxeThrown && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowAxe();
        }

        if (isAxeLanded && Input.GetKeyDown(KeyCode.Mouse1))
        {
            ReturnAxe();
        }
    }

    private void ThrowAxe()
    {
        axeRb.useGravity = true;
        Vector3 throwDirection = transform.forward; // Use player's forward direction
        axeRb.velocity = throwDirection * throwForce; // Set velocity for throw

        isAxeLanded = false;
        isAxeThrown = true;
    }

    private void ReturnAxe()
    {
        Vector3 targetPosition = Vector3.Lerp(axe.transform.position, initialAxePosition, returnSpeed * Time.deltaTime);

        axe.transform.position = targetPosition;

        if (Vector3.Distance(axe.transform.position, initialAxePosition) < 0.1f)
        {
            isAxeThrown = false;
            axeRb.isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isAxeThrown)
        {
            if (other.gameObject.CompareTag("Floor"))
            {
                axeRb.useGravity = false;
                axeLandingLocation = other.contacts[0].point;
                Debug.Log("Axe hit the floor at: " + axeLandingLocation);
                isAxeLanded = true;
                // Handle floor collision logic here
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                axeRb.useGravity = false;
                axeLandingLocation = other.contacts[0].point;
                Debug.Log("Axe hit an enemy at: " + axeLandingLocation);
                isAxeLanded = true;
                // Handle enemy collision logic here
            }
        }
    }
}
