using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFacesUser : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera"); 
    }

    void Update()
    {
        CanvasFaceCamera();
    }

    private void CanvasFaceCamera()
    {
        if (mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }
}
