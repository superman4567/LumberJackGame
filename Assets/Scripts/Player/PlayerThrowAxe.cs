using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class PlayerThrowAxe : MonoBehaviour
{
    [SerializeField] private GameObject axeModel;
    [SerializeField] private AxeDetection axe;
    [SerializeField] private Transform throwSpawnPoint; 
    [SerializeField] private Rigidbody axeRb;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float spinForce;
    [SerializeField] private float constraintTimer = 0.2f;

    private bool isAxeThrown = false;
    private bool isReturning;

    private float timeSinceAxeThrown = 0f;
    private Transform initialParent;
    private Vector3 initialAxePosition;
    private Vector3 returnStartPosition;
    

    private void Start()
    {
        axeRb.useGravity = false;
        initialParent = throwSpawnPoint;
        initialAxePosition = axeModel.transform.position;
    }

    private void Update()
    {
        Debug.Log(isReturning);
        InputThrowAndReceiveAxe();

        if (isReturning) {
            ReturnAxe();
        }
    }

    private void InputThrowAndReceiveAxe()
    {
        timeSinceAxeThrown += Time.deltaTime;

        if (!isAxeThrown && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowAxe();
            isReturning = false;
            timeSinceAxeThrown = 0f;
        }

        if (axe.axeHitSomething && Input.GetKeyDown(KeyCode.Mouse0) || timeSinceAxeThrown >= 0.4f && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isReturning = true;
        }
        else
        {
            isReturning = false;
        }
    }

    private void ThrowAxe()
    {
        isAxeThrown = true;
        axeModel.transform.parent = null;
        axeRb.useGravity = true;
        axeRb.constraints =  RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        // Apply angular velocity for initial spin
        Vector3 spinAxis = transform.up;
        axeRb.angularVelocity = spinAxis * spinForce;

        // Apply forward force for throwing
        Vector3 throwDirection = transform.forward;
        axeRb.velocity = throwDirection * throwForce;

        StartCoroutine(StopContstraint());
    }

    private IEnumerator StopContstraint()
    {
        yield return new WaitForSeconds(constraintTimer);
        axeRb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void ReturnAxe()
    {
        if (!isReturning)
        {
            // Disable most of the RB
            isReturning = true;
            axeRb.useGravity = false;
            axeRb.velocity = Vector3.zero;
            axeRb.angularVelocity = Vector3.zero;
        }

        // Smoothly move the axe towards the throwSpawnPoint
        Vector3 targetPosition = Vector3.Lerp(axeModel.transform.position, throwSpawnPoint.position, returnSpeed * Time.deltaTime);
        axeModel.transform.position = targetPosition;

        if (Vector3.Distance(axeModel.transform.position, throwSpawnPoint.position) < 2f)
        {
            // If the axe is close to the throwSpawnPoint, reset the position and rotation
            axeModel.transform.parent = throwSpawnPoint;
            axeModel.transform.localPosition = Vector3.zero;
            axeModel.transform.localRotation = Quaternion.identity;

            // Reset values
            isAxeThrown = false;
            isReturning = false;
            axe.axeHitSomething = false;
        }
    }

}
