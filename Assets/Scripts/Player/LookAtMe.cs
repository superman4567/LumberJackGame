using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtMe : MonoBehaviour
{

    [SerializeField] private PlayerThrowAxe playerThrowAxe;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerParent;
    [SerializeField] private float lerpSpeed = 1.0f;
    [SerializeField] private float dotProductThreshold = 0.7f;
    [SerializeField] private float offset;
    [SerializeField] private LayerMask floorMask;

    private Vector3 desiredRightRot;

    private void Update()
    {
        LookAt();
    }

    public void LookAt()
    {
        Vector3 playerForward = playerParent.forward;
        Vector3 targetDirection = transform.position - playerParent.position;
        targetDirection.Normalize();
        float dotProduct = Vector3.Dot(playerForward, targetDirection);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitPos, Mathf.Infinity, floorMask))
        {
            Vector3 elevatedPos = new Vector3(hitPos.point.x, hitPos.point.y + offset, hitPos.point.z);
            transform.position = elevatedPos;

            if (dotProduct < dotProductThreshold)
            {
                // Rotate to the right
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    Vector3 desiredRightRot = new Vector3(playerParent.eulerAngles.x, playerParent.eulerAngles.y + 90, playerParent.eulerAngles.z);
                    playerParent.eulerAngles = Vector3.Lerp(playerParent.eulerAngles, desiredRightRot, lerpSpeed * Time.deltaTime);
                }
                // Rotate to the left
                else
                {
                    Vector3 desiredLeftRot = new Vector3(playerParent.eulerAngles.x, playerParent.eulerAngles.y - 90, playerParent.eulerAngles.z);
                    playerParent.eulerAngles = Vector3.Lerp(playerParent.eulerAngles, desiredLeftRot, lerpSpeed * Time.deltaTime);
                }
            }
        }
    }
}
