using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class BuildingStructures : MonoBehaviour
{
    [Header("List of building objects")]
    [SerializeField] private List<GameObject> BuildingLevels = new List<GameObject>();
    private GameManager gameManager;

    [Header("Set cost of upgrading")]
    [SerializeField] private int costUpgradeTier1 = 30;
    [SerializeField] private int costUpgradeTier2 = 60;
    [SerializeField] private int costUpgradeTier3 = 90;
    private int currentBuildingLevel = 0;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ReferenceChildAndSetDefault();
    }

    private void Update()
    {
        NextBuildingTier();
    }

    private void ReferenceChildAndSetDefault()
    {
        foreach (Transform child in transform)
        {
            BuildingLevels.Add(child.gameObject);
        }

        BuildingLevels[0].SetActive(true);
        BuildingLevels[1].SetActive(false);
        BuildingLevels[2].SetActive(false);
        BuildingLevels[3].SetActive(false);
    }

    private void NextBuildingTier()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentBuildingLevel + 1 < BuildingLevels.Count)
            {
                //I think I whould use a getter/setter
                if (costUpgradeTier1 < gameManager.wood && currentBuildingLevel == 0)
                {
                    currentBuildingLevel = 1;
                    gameManager.SubstractResource(ResourceType.Wood, costUpgradeTier1);

                    BuildingLevels[0].SetActive(false);
                    BuildingLevels[1].SetActive(true);
                    return;
                }

                else if (costUpgradeTier2 < gameManager.wood && currentBuildingLevel == 1)
                {
                    currentBuildingLevel = 2;
                    gameManager.SubstractResource(ResourceType.Wood, costUpgradeTier2);

                    BuildingLevels[1].SetActive(false);
                    BuildingLevels[2].SetActive(true);
                    return;
                }

                else if (costUpgradeTier3 < gameManager.wood && currentBuildingLevel == 2)
                {
                    currentBuildingLevel = 3;
                    gameManager.SubstractResource(ResourceType.Wood, costUpgradeTier3);

                    BuildingLevels[2].SetActive(false);
                    BuildingLevels[3].SetActive(true);
                    return;
                }

                else if (currentBuildingLevel == 3)
                {
                    //enable powerup after interacting
                }
            }
        }
    }
}
