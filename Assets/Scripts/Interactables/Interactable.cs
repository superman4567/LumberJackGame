using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    protected PlayerInteraction playerInteraction;
    protected TreeChopProgress treeChopProgress;
    public float savedProgressInSeconds;
    public float interactInSeconds;
    public bool canBeInteractedWith = true;
    protected bool interactionComplete = false;
    public bool lookingAtInteractableObject = false;

    private void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        treeChopProgress = GetComponent<TreeChopProgress>();
        interactInSeconds = Random.Range(2f, 4f);
    }

    public virtual void AddProgress(float progressInSeconds)
    {
        if (savedProgressInSeconds <= interactInSeconds)
        {
            savedProgressInSeconds += progressInSeconds;
            if (treeChopProgress != null)
            {
                treeChopProgress.AddRadialAmount(true);
            }
        }
        else
        {
            treeChopProgress.AddRadialAmount(false);
        }
    }

    public virtual bool CheckProgressComplete()
    {
        return (savedProgressInSeconds >= interactInSeconds);
    }

    public virtual void InteractComplete()
    {
        canBeInteractedWith = false;
        interactionComplete = true;
    }
}
