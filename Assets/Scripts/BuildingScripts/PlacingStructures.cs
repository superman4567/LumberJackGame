using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class PlacingStructures : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachineFramingTransposer cinemachineFramingTransposer;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rotateAmount;
    [SerializeField] private float cameraDistanceDefault = 18;
    [SerializeField] private float cameraDistanceBuilding = 35;

    public GameObject[] building;
    private GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    private bool isBuilding;

    void Start()
    {
        cinemachineFramingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        cinemachineFramingTransposer.m_CameraDistance = cameraDistanceDefault;
        isBuilding = false;
    }

    void Update()
    {
        BuildingMode();
    }

    private void FixedUpdate()
    {
        if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                pos = hit.point;
            }
        }
    }

    private void BuildingMode()
    {
        if (pendingObject != null)
        {
            isBuilding = true;
            cinemachineFramingTransposer.m_CameraDistance = cameraDistanceBuilding;

            pendingObject.transform.position = pos;

            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                RotateObjectClockwise();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateObjectCounterClock();
            }
        }
        else
        {
            isBuilding = false;
            cinemachineFramingTransposer.m_CameraDistance = cameraDistanceDefault;
        }
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(building[0], pos, transform.rotation);
    }

    public void PlaceObject()
    {
        pendingObject = null;
        isBuilding = false;
    }

    public void RotateObjectClockwise()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    public void RotateObjectCounterClock()
    {
        pendingObject.transform.Rotate(Vector3.up, -rotateAmount);
    }
}