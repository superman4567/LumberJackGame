using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestSO mainQuest;    // Assign main quest Scriptable Object in the Inspector
    public QuestSO[] subQuests;  // Assign quest Scriptable Objects in the Inspector

    public int activeQuestIndex = 0; // Index of the active sub-quest
    public static QuestManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Debug.Log(subQuests[0].questName);
        for (int i = 0; i < subQuests.Length; i++)
        {
            subQuests[i].isCompleted = false;
            subQuests[i].currentAmount = 0;
            if (i != 0)
            {
                subQuests[i].isActive = false;
            }
        }
    }

    private void Update()
    {
        // Check if the current active quest is complete
        if (subQuests[activeQuestIndex].isCompleted)
        {
            Debug.Log("Sub Quest complete");
            Debug.Log(subQuests[0].questName);
            // Move to the next quest in the sequence if available
            if (activeQuestIndex < subQuests.Length - 1)
            {
                activeQuestIndex++;
            }
            else
            {
                CompleteMainQuest();
            }
        }
    }

    private void CompleteMainQuest()
    {
        mainQuest.isCompleted = true;
        Debug.Log("Main Quest complete");
        // Trigger main quest completion logic
    }
}
