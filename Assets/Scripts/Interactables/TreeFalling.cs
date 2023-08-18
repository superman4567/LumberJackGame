using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class TreeFalling : Interactable
{
    [SerializeField] private float waitForTreeToFallTime;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private int addWoodsMin, addWoodsMax;

    private bool treeIsStatic;
    private float timer;
    private int randomAddWood;


    private void Update()
    {
        CheckIfStatic();
    }

    private void CheckIfStatic()
    {
        timer += Time.deltaTime;

        if (timer > waitForTreeToFallTime)
        {
            if (!treeIsStatic)
            {
                if (Mathf.Approximately(rigidBody.velocity.magnitude, 0f))
                {
                    treeIsStatic = true;
                }
                else
                {
                    treeIsStatic = false;
                }
            }
        }
    }

    public override void InteractComplete()
    {
        bool completedOnce = false;
        if (treeIsStatic && !completedOnce)
        {
            randomAddWood = Random.Range(addWoodsMin, addWoodsMax);
            GameManager.Instance.AddResource(ResourceType.Wood, 5);
            completedOnce = true;
            CheckOffQuest();

            Destroy(gameObject);
        }
    }

    private void CheckOffQuest()
    {
        string questName = QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].questName;
        int requiredAmount = QuestManager.Instance.subQuests[QuestManager.Instance.activeQuestIndex].requiredAmount;

        //check for quest names
        if (questName == "Gather Wood")
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
