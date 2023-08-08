using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected PlayerInteraction playerInteraction;

    private bool isInteracting;
    private float interactInSeconds;

    protected virtual void Start()
    {
        playerInteraction = PlayerInteraction.Instance;
    }

    protected virtual void Update()
    {
        if (!isInteracting) return;
        if (playerInteraction.GetInteractable() != this) return;

        interactInSeconds += Time.deltaTime;
        playerInteraction.holdingDownInteract = interactInSeconds;
    }

    public virtual void StartInteract()
    {
        isInteracting = true;
    }

    public virtual void StopInteract()
    {
        isInteracting = false;
        playerInteraction.holdingDownInteract = 0;
    }

    public virtual void InteractComplete()
    {
        playerInteraction.holdingDownInteract = 0;
    }
}
