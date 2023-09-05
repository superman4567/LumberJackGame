using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InteractPanel : MonoBehaviour
{
    [SerializeField] public GameObject radialCanvas;
    [SerializeField] public UnityEngine.UI.Image circle;
    [SerializeField] private Vector3 positionOffset; // Adjust this offset as needed
    [SerializeField] private Vector3 globalScale = new Vector3(6f, 6f, 6f);

    void Start()
    {
        radialCanvas.SetActive(false);
    }

    private void Update()
    {
        Vector3 newPosition = transform.position + positionOffset;
        radialCanvas.transform.position = newPosition;

        Vector3 parentScale = radialCanvas.transform.parent != null ? radialCanvas.transform.parent.lossyScale : Vector3.one;
        Vector3 localScale = new Vector3(
            globalScale.x / parentScale.x,
            globalScale.y / parentScale.y,
            globalScale.z / parentScale.z
        );

        radialCanvas.transform.localScale = localScale;
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
