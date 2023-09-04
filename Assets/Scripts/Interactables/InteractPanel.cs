using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPanel : MonoBehaviour
{
    [SerializeField] public GameObject radialCanvas;
    [SerializeField] public Image circle;
    [SerializeField] private Vector3 positionOffset; // Adjust this offset as needed
    [SerializeField] private Vector3 globalScale = new Vector3(3f, 3f, 3f);

    void Start()
    {
        radialCanvas.SetActive(false);
    }

    private void Update()
    {
        Vector3 newPosition = transform.position + positionOffset;
        radialCanvas.transform.position = newPosition;

        radialCanvas.transform.localScale = globalScale;
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
