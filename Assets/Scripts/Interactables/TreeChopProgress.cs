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

    //private void Start()
    //{
    //    StartCoroutine(PlayChopTreeSound());
    //}

    public void AddRadialAmount(bool interacting)
    {
        if (interacting)
        {
            radialCanvas.SetActive(true);
            radialFill.fillAmount = tree.savedProgressInSeconds  / tree.interactInSeconds;
            // Add Axe Chopping Wood Sound DOESN'T WORK HERE
            // AkSoundEngine.PostEvent("Play_Chopping_Wood_SFX__single_chop_", gameObject);
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

    //private IEnumerator PlayChopTreeSound()
    //{
    //    yield return new WaitForSeconds(2);
    //    AkSoundEngine.PostEvent("Play_Chopping_Wood_SFX__single_chop_", gameObject);
    //}

    private void OnTriggerExit(Collider other)
    {
        radialCanvas.SetActive(false);
    }
}
