using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPanel : MonoBehaviour
{
    [SerializeField] private GameObject radialCanvas;

    void Start()
    {
        radialCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            radialCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        radialCanvas.SetActive(false);
    }
}
