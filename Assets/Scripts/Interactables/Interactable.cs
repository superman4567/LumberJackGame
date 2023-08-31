using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected Image fill;
    [SerializeField] protected GameObject canvas;

    protected PlayerInteraction playerInteraction;
    public float savedProgressInSeconds;
    public float interactInSeconds;
    public bool canBeInteractedWith = true;
    protected bool interactionComplete = false;

    private void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        if (gameObject.tag != "Chest") 
        {
            canvas.SetActive(false); 
        }
        else { return; }
    }

    public virtual void AddProgress(float progressInSeconds)
    {
        if (savedProgressInSeconds <= interactInSeconds)
        {
            savedProgressInSeconds += progressInSeconds;

            if (gameObject.tag == "Chest") { return; }
            fill.fillAmount = savedProgressInSeconds / interactInSeconds;
        }
    }

    public virtual bool CheckProgressComplete()
    {
        if (savedProgressInSeconds >= interactInSeconds)
        {
            canvas.SetActive(false);
        }
        return (savedProgressInSeconds >= interactInSeconds);
    }

    public virtual void InteractComplete()
    {
        canBeInteractedWith = false;
        interactionComplete = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Chest") { return; }
        if (interactionComplete) { return; }

        if (playerInteraction.currentInteractableObject == this)
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "Chest") { return; }
        canvas.SetActive(false);
    }
}
