using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCopyRotation : MonoBehaviour
{
    private Quaternion initialLocalRotation;

    private void Start()
    {
        // Store the initial local rotation when the script starts.
        initialLocalRotation = transform.localRotation;
    }

    private void LateUpdate()
    {
        // Reset the local rotation to its initial value.
        transform.localRotation = initialLocalRotation;
    }
}
