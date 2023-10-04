using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowAxe : MonoBehaviour, IDataPersistance
{
    [SerializeField] private GameObject axeModel;
    [SerializeField] private GameObject throwPoint;
    [SerializeField] private AxeDetection axe;
    [SerializeField] private Transform throwSpawnPoint; 
    [SerializeField] private Rigidbody axeRb;
    [SerializeField] private float throwForce = 10f;
    public float throwforceMultiplier = 1f;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float spinForce;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;

    public bool isAxeThrown = false;
    public bool isReturning;
    public bool CanThrowAxe { get; set; }

    private float timeSinceAxeThrown = 0f;
    

    private void Start()
    {
        axeRb.useGravity = false;
        axeRb.isKinematic = true;
    }

    public void LoadData(GameData data)
    {
        this.throwforceMultiplier = data.throwforceMultiplier;
    }

    public void SaveData(GameData data)
    {
        data.throwforceMultiplier = this.throwforceMultiplier;
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
            isAxeThrown = true;
            isReturning = false;
            timeSinceAxeThrown = 0f;
            // Throw Axe SFX
            AkSoundEngine.PostEvent("Play_Throw_Axe_1_SFX", gameObject);
        }

        if (axe.axeHitSomething && Input.GetKeyDown(KeyCode.Mouse0) || timeSinceAxeThrown >= 0.4f && Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Recall", true);
        }
    }

    public void ThrowAxe()
    {
        animator.SetBool("Throw", false);
        axeModel.transform.parent = null;
        isAxeThrown = true;
        axeRb.isKinematic = false;
        axeRb.useGravity = true;
        axeRb.constraints =  RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        Vector3 spinAxis = transform.up;
        axeRb.angularVelocity = spinAxis * spinForce;

        Vector3 throwDirection = (throwPoint.transform.position - throwSpawnPoint.position).normalized;
        axeRb.velocity = throwDirection * (throwForce * throwforceMultiplier);
        // Add Axe Hits Something Generic (Soft) Sound, could not make it work as the sound plays before hitting snow / generic
        // AkSoundEngine.PostEvent("Play_Axe_Hitting_Something_Soft__Moist_", gameObject);

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
                // Retrieve Axe SFX; NOTE: WORKS, BUT WATCH OUT FOR ANY DELAY OR STRETCHING OF THE AUDIO.
                AkSoundEngine.PostEvent("Play_Retrieve_Axe_SFX", gameObject);
                break;
            }
        }
    }
}
