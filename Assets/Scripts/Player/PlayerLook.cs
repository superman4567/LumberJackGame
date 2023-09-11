using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask floorMask;

    private RaycastHit hit;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        RotateUpperBodyToMouse();
    }

    public void RotateUpperBodyToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitPos, Mathf.Infinity, floorMask))
        {
            hit = hitPos;

            Vector3 lookDirection = hit.point - transform.position;
            lookDirection.y = 0f;

            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }

    public RaycastHit GetRayCastHit()
    {
        return hit;
    }
}
