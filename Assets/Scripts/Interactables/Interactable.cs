using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float savedProgressInSeconds;
    public float interactInSeconds;
    private QuestManager questManager;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

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
        //do something
    }
}
