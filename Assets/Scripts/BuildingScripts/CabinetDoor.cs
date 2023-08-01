using System.Collections;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    [SerializeField] Transform doorRotationOpen;
    [SerializeField] Transform doorRotationClosed;
    [SerializeField] private float timeToArrive;

    private bool isMoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
            StartCoroutine(DoorLerp(doorRotationClosed.rotation, doorRotationOpen.rotation));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
            StartCoroutine(DoorLerp(doorRotationOpen.rotation, doorRotationClosed.rotation));
        }
    }

    private IEnumerator DoorLerp(Quaternion fromRotation, Quaternion toRotation)
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeToArrive)
        {
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, elapsedTime / timeToArrive);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = toRotation; // Ensure the door reaches the target rotation exactly.
        isMoving = false;
    }
}