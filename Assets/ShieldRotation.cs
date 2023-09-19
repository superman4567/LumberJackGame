using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;
    public bool isSpinning = false;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        isSpinning = true;
    }

    private void OnDisable()
    {
        isSpinning = false;
    }

    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    
    
}
