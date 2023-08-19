using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradingStructures : MonoBehaviour
{
    [Header("List of child building objects")]
    [SerializeField] private List<GameObject> BuildingLevels = new List<GameObject>();
    [SerializeField] private int BP_index;

    [Header("Set cost of upgrading")]
    [SerializeField] private int costUpgradeTier1;
    [SerializeField] private int costUpgradeTier2;
    [SerializeField] private int costUpgradeTier3;

    [SerializeField] private GameObject interactionTrigger;
    [SerializeField] private Collider upgradeTrigger;

    private UIManager uiManager;
    private GameManager gameManager;
    private PurchaseState currentState = PurchaseState.Idle;

    public bool playerInRange = false;
    private bool canPurchase = true;
    private bool isUpgraded = false;
    private int currentBuildingLevel = 0;

    public enum PurchaseState
    {
        Idle,
        Upgrading,
    }

    void Start()
    {
        uiManager = UIManager.Instance;
        gameManager = GameManager.Instance;
        SetTier1Active();
    }

    public void SetTier1Active()
    {
        BuildingLevels[0].SetActive(true);

        for (int i = 1; i < BuildingLevels.Count; i++)
        {
            BuildingLevels[i].SetActive(false);
        }
    }

    public void SetLastTierActive()
    {
        BuildingLevels[BuildingLevels.Count].SetActive(true);
    }

    public void SetTriggerActive()
    {
        interactionTrigger.gameObject.SetActive(true);
    }

    public void PurchaseButtonPressed()
    {
        if (currentState == PurchaseState.Idle && canPurchase)
        {
            NextBuildingTier();
            isUpgraded = true;
        }
    }

    public void NextBuildingTier()
    {
        if (currentBuildingLevel + 1 < BuildingLevels.Count)
        {
            if (costUpgradeTier1 < gameManager.wood && currentBuildingLevel == 0)
            {
                // Upgrade to tier 1
                currentState = PurchaseState.Upgrading;

                currentBuildingLevel = 1;
                gameManager.SubstractResource(GameManager.ResourceType.Wood, costUpgradeTier1);

                BuildingLevels[0].SetActive(false);
                BuildingLevels[1].SetActive(true);

                currentState = PurchaseState.Idle;
                canPurchase = true; // Set to true after successful upgrade
            }
            else if (costUpgradeTier2 < gameManager.wood && currentBuildingLevel == 1)
            {
                // Upgrade to tier 2
                currentState = PurchaseState.Upgrading;

                currentBuildingLevel = 2;
                gameManager.SubstractResource(GameManager.ResourceType.Wood, costUpgradeTier2);

                BuildingLevels[1].SetActive(false);
                BuildingLevels[2].SetActive(true);

                currentState = PurchaseState.Idle;
                canPurchase = true; // Set to true after successful upgrade
            }
            else if (costUpgradeTier3 < gameManager.wood && currentBuildingLevel == 2)
            {
                // Upgrade to tier 3
                currentState = PurchaseState.Upgrading;

                currentBuildingLevel = 3;
                gameManager.SubstractResource(GameManager.ResourceType.Wood, costUpgradeTier3);

                BuildingLevels[2].SetActive(false);
                BuildingLevels[3].SetActive(true);

                currentState = PurchaseState.Idle;
                canPurchase = true; // Set to true after successful upgrade
            }
        }
        if (currentBuildingLevel == BuildingLevels.Count - 1)
        {
            UnlockBPPassive(FindObjectOfType<BlueprintUI>().currentBPbyIndex);
        }
    }

    private void UnlockBPPassive(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("The cabin is called");
                // Enable effect for index 1
                // For example, increase player speed
                // Call the player movement script and modify speed
                break;

            case 1:
                // Enable effect for index 2
                // For example, enable a specific ability
                // Call the ability activation method
                // AbilityManager.Instance.ActivateAbility(abilityIndex);
                break;

            // Add more cases as needed for different indices

            default:
                // Default case for unrecognized index
                break;
        }
    }

}
