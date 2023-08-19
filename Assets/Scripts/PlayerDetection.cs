using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] UpgradingStructures UpgradingStructures;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpgradingStructures.playerInRange = true;
            UIManager.Instance.panels[2].SetActive(true);
            PlayerInteraction.Instance.SetBuildingToUpgrade(GetComponentInParent<UpgradingStructures>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpgradingStructures.playerInRange = false;
            UIManager.Instance.panels[2].SetActive(false);
            PlayerInteraction.Instance.ClearBuildingToUpgrade();
        }
    }
}
