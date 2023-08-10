using System.Collections;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
            StartCoroutine(DoorLerp(doorRotationOpen.localEulerAngles, doorRotationClosed.localEulerAngles));
            canInteract = false;
        }
    }

    private IEnumerator DoorLerp(Vector3 fromEulerAngles, Vector3 toEulerAngles)
    {
        float elapsedTime = 0f;
        Quaternion fromRotation = Quaternion.Euler(fromEulerAngles);
        Quaternion toRotation = Quaternion.Euler(toEulerAngles);

        while (elapsedTime < timeToArrive)
        {
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, elapsedTime / timeToArrive);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        transform.rotation = toRotation;
        isMoving = false;
    }
}
