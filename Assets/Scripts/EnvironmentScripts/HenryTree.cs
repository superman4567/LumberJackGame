using System;
using System.Collections;
using UnityEngine;

public class HenryTree : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource TreeBreakingSound;
    [SerializeField] private Transform fallingTreePrefab;

    private PlayerInteraction playerInteraction;

    private bool isInteracting;
    private float interactInSeconds;

    private void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void Update()
    {
        if (playerInteraction.GetInteractable() != (IInteractable)this) return;
        if (!isInteracting) return;

        interactInSeconds += Time.deltaTime;
        playerInteraction.holdingDownInteract = interactInSeconds;
    }

    public void StartInteract()
    {
        isInteracting = true;
    }

    public void StopInteract()
    {
        isInteracting = false;
        playerInteraction.holdingDownInteract = 0;
    }

    public void InteractComplete()
    {
        TreeBreakingSound.Play();
        GameManager.Instance.AddWood();
        playerInteraction.holdingDownInteract = 0;
        Instantiate(fallingTreePrefab, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
