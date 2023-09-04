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
            // Set the canvas rotation to match the camera's rotation
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}
