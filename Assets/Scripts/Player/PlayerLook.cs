using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Transform playerModel;

    private RaycastHit hit;
    public Vector3 lookDirection;

    private void Update()
    {
        PlayerLookDir();
    }

    public void PlayerLookDir()
    {
        if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitPos, Mathf.Infinity, floorMask))
            {
                hit = hitPos;
                lookDirection = hit.point - transform.position;
                lookDirection.y = 0f;
                if (lookDirection != Vector3.zero)
                {
                    playerModel.rotation = Quaternion.LookRotation(lookDirection);
                }
            }
        }
    }

    public RaycastHit GetRayCastHit()
    {
        return hit;
    }
}
