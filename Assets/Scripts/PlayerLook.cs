using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask floorMask;

    private void Update()
    {
        PlayerLookDir();
    }

    private void PlayerLookDir()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorMask))
        {
            Vector3 lookDirection = hit.point - transform.position;
            lookDirection.y = 0f; // Keep only the horizontal component
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}
