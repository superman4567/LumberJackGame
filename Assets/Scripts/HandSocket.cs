using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSocket : MonoBehaviour
{
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform axeTransform;

    private Vector3 localAxePos;
    private Quaternion localAxeRot;

    private void Start()
    {
        localAxePos = axeTransform.localPosition;
        localAxeRot = axeTransform.localRotation;
    }

    private void LateUpdate()
    {
        transform.position = handTransform.position;
        transform.rotation = handTransform.rotation;

        if (axeTransform.parent != null)
        {
            axeTransform.localPosition = localAxePos;
            axeTransform.localRotation = localAxeRot;
        }  
    }
}
