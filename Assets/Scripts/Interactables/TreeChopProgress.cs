using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeChopProgress : MonoBehaviour
{
    [SerializeField] private GameObject radialCanvas;
    [SerializeField] private Image radialFill;
    [SerializeField] private Interactable tree;


    void Start()
    {
        radialCanvas.SetActive(false);
    }

    public void AddRadialAmount(bool interacting)
    {
        if (interacting)
        {
            radialCanvas.SetActive(true);
            radialFill.fillAmount = tree.savedProgressInSeconds  / tree.interactInSeconds;
        }
        else
        {
            radialCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && tree.canBeInteractedWith)
        {
            radialCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        radialCanvas.SetActive(false);
    }
}
