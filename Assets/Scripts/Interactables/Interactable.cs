using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float savedProgressInSeconds;
    public float interactInSeconds;

    public bool canBeInteractedWith = true;
    protected bool interactionComplete = false;

    public virtual void AddProgress(float progressInSeconds)
    {
        if (savedProgressInSeconds <= interactInSeconds)
        {
            savedProgressInSeconds += progressInSeconds;
        }
    }

    public virtual bool CheckProgressComplete()
    {
        return (savedProgressInSeconds >= interactInSeconds);
    }

    public virtual void InteractComplete()
    {
        interactionComplete = true;
    }
}
