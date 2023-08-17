using UnityEngine;

public class BlueprintManager : MonoBehaviour
{
    [SerializeField] public BlueprintData[] blueprints;
    public bool[] unlockedBlueprints;

    private static BlueprintManager instance;
    public static BlueprintManager Instance => instance;

    private int unlockCount = 0; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckOffQuest();
    }

    public void UnlockBlueprint(int blueprintIndex)
    {
        if (blueprintIndex >= 0 && blueprintIndex < blueprints.Length)
        {
            blueprints[blueprintIndex].isUnlocked = true;
            unlockCount++; // Increment the unlock count
        }
    }

    public bool IsBlueprintUnlocked(int blueprintIndex)
    {
        if (blueprintIndex >= 0 && blueprintIndex < unlockedBlueprints.Length)
        {
            return unlockedBlueprints[blueprintIndex];
        }
        return false;
    }

    public BlueprintData GetBlueprint(int blueprintIndex)
    {
        if (blueprintIndex >= 0 && blueprintIndex < blueprints.Length)
        {
            return blueprints[blueprintIndex];
        }
        return null;
    }

    public int GetUnlockCount()
    {
        return unlockCount;
    }

    private void CheckOffQuest()
    {
        string questName = QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].questName;
        int requiredAmount = QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].requiredAmount;

        //check for quest names
        if (questName == "First Discovery" || questName == "Preparing for the return journey")
        {
            QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].currentAmount += 1;

            int currentAmount = QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].currentAmount;

            if (currentAmount >= requiredAmount)
            {
                QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].isCompleted = true;
            }
        }
    }
}
