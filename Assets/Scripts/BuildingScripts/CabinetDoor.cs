using System.Collections;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    [SerializeField] private Transform doorToOpen;
    [SerializeField] private Transform doorRotationOpen;
    [SerializeField] private Transform doorRotationClosed;
    [SerializeField] private float timeToArrive;

    private bool isMoving = false;
    public bool canInteract= false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
            StartCoroutine(DoorLerp(doorRotationClosed.localEulerAngles, doorRotationOpen.localEulerAngles));
            canInteract = true;
            // Add Cabin Door Opening
            AkSoundEngine.PostEvent("Play_Cabin_Door_Opening_SFX", gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
            StartCoroutine(DoorLerp(doorRotationOpen.localEulerAngles, doorRotationClosed.localEulerAngles));
            canInteract = false;
            // Add Cabin Door Closing Shut !!!!!!!!!!!!!!!!!!!!!!!!!
        }
    }

    private IEnumerator DoorLerp(Vector3 fromEulerAngles, Vector3 toEulerAngles)
    {
        float elapsedTime = 0f;
        Quaternion fromRotation = Quaternion.Euler(fromEulerAngles);
        Quaternion toRotation = Quaternion.Euler(toEulerAngles);

        while (elapsedTime < timeToArrive)
        {
            doorToOpen.rotation = Quaternion.Slerp(fromRotation, toRotation, elapsedTime / timeToArrive);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        doorToOpen.rotation = toRotation;
        isMoving = false;
    }
}
