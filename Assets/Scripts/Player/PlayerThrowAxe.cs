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
    [SerializeField] private Animator animator;

    private bool isAxeThrown = false;
    private bool isReturning;

    private float timeSinceAxeThrown = 0f;
    private Transform initialParent;
    private Vector3 initialAxePosition;
    private Vector3 returnStartPosition;
    

    private void Start()
    {
        axeRb.useGravity = false;
        axeRb.isKinematic = true;
        initialParent = throwSpawnPoint;
        initialAxePosition = axeModel.transform.position;
    }

    private void Update()
    {
        InputThrowAndReceiveAxe();

        if (isReturning) 
        {
            ReturnAxe();
        }
    }

    private void InputThrowAndReceiveAxe()
    {
        timeSinceAxeThrown += Time.deltaTime;

        if (!isAxeThrown && Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Throw", true);
            isReturning = false;
            timeSinceAxeThrown = 0f;
        }

        if (axe.axeHitSomething && Input.GetKeyDown(KeyCode.Mouse0) || timeSinceAxeThrown >= 0.4f && Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Recall", true);
        }
    }

    public void ThrowAxe()
    {
        animator.SetBool("Throw", false);
        isAxeThrown = true;
        axeModel.transform.parent = null;
        axeRb.isKinematic = false;
        axeRb.useGravity = false;
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
        axeRb.isKinematic = false;
        axeModel.transform.parent = null;
        axeRb.useGravity = true;
        axeRb.mass = 50f;
        axeRb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void ReturnAxe()
    {
        animator.SetBool("Recall", false);
        isReturning = true;

        if (isAxeThrown)
        {
            isReturning = true;
            axeRb.useGravity = true;
            axeRb.velocity = Vector3.zero;
            axeRb.angularVelocity = Vector3.zero;
        }
        StartCoroutine(AxeReturnLerp());
        
    }

    private IEnumerator AxeReturnLerp()
    {
        while (isReturning)
        {
            // Smoothly move the axe towards the throwSpawnPoint
            Vector3 targetPosition = Vector3.Lerp(axeModel.transform.position, throwSpawnPoint.position, returnSpeed * Time.deltaTime);
            axeModel.transform.position = targetPosition;
            yield return new WaitForEndOfFrame();

            if (Vector3.Distance(axeModel.transform.position, throwSpawnPoint.position) < 2f)
            {
                // If the axe is close to the throwSpawnPoint, reset the position and rotation
                axeRb.isKinematic = true;
                axeModel.transform.parent = throwSpawnPoint;
                axeModel.transform.localPosition = Vector3.zero;
                axeModel.transform.localRotation = Quaternion.identity;

                // Reset values
                isAxeThrown = false;
                isReturning = false;
                axe.axeHitSomething = false;
                break;
            }
        }
    }
}