using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private int blueprintIndex;

    public override void InteractComplete()
    {
        base.InteractComplete();
        BlueprintManager.Instance.UnlockBlueprint(blueprintIndex);
        CheckOffQuest();

        //Show in the UI what you have unlocked
        Invoke("DestroyChest", 60f);
    }

    private void CheckOffQuest()
    {
        string questName = QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].questName;
        int requiredAmount = QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].requiredAmount;


        //check for quest names
        if (questName == "Treasure lays around" || questName == "Treasure hunter")
        {
            QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].currentAmount += 1;

            int currentAmount = QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].currentAmount;

            if (currentAmount >= requiredAmount)
            {
                QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].isCompleted = true;
            }
        }
    }

    private void DestroyChest()
    {
        Destroy(gameObject);
    }
}
