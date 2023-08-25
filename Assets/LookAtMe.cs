using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtMe : MonoBehaviour
{

    [SerializeField] private PlayerThrowAxe playerThrowAxe;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float offset;
    [SerializeField] private Transform player;
    [SerializeField] private float dotProductThreshold = 0.7f;

    private void Update()
    {
        LookAt();
    }

    public void LookAt()
    {
        Vector3 playerForward = player.forward;

        // Calculate the direction from the player to the target
        Vector3 targetDirection = transform.position - player.position;

        // Normalize the target direction to ensure consistent comparisons
        targetDirection.Normalize();

        // Calculate the dot product between player's forward direction and target direction
        float dotProduct = Vector3.Dot(playerForward, targetDirection);

        // Perform raycasting to determine where the player is aiming
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitPos, Mathf.Infinity, floorMask))
        {
            Vector3 elevatedPos = new Vector3(hitPos.point.x, hitPos.point.y + offset, hitPos.point.z);
            transform.position = elevatedPos;

            // Check if the dot product is within the acceptable range
            if (Mathf.Abs(dotProduct) >= dotProductThreshold)
            {
                playerThrowAxe.CanThrowAxe = true;
            }
            else
            {
                playerThrowAxe.CanThrowAxe = false;
            }
        }
    }

}
