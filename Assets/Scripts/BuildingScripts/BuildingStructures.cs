using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameManager = GameManager.Instance;

        foreach (Transform child in transform)
        {
            BuildingLevels.Add(child.gameObject);
        }

        BuildingLevels[0].SetActive(true);
        BuildingLevels[1].SetActive(false);
        BuildingLevels[2].SetActive(false);
        BuildingLevels[3].SetActive(false);
    }

    private void Update()
    {
        NextBuildingTier();
    }

    private void NextBuildingTier()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentBuildingLevel + 1 < BuildingLevels.Count)
            {
                //I think I whould use a getter/setter
                if (costUpgradeTier1 < gameManager.rawWood && currentBuildingLevel == 0)
                {
                    currentBuildingLevel = 1;
                    gameManager.SubstractWood(costUpgradeTier1);

                    BuildingLevels[0].SetActive(false);
                    BuildingLevels[1].SetActive(true);
                    return;
                }

                else if (costUpgradeTier2 < gameManager.rawWood && currentBuildingLevel == 1)
                {
                    currentBuildingLevel = 2;
                    gameManager.SubstractWood(costUpgradeTier2);

                    BuildingLevels[1].SetActive(false);
                    BuildingLevels[2].SetActive(true);
                    return;
                }

                else if (costUpgradeTier3 < gameManager.rawWood && currentBuildingLevel == 2)
                {
                    currentBuildingLevel = 3;
                    gameManager.SubstractWood(costUpgradeTier3);

                    BuildingLevels[2].SetActive(false);
                    BuildingLevels[3].SetActive(true);
                    return;
                }
            }
        }
    }
}
