using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodAndCoins : MonoBehaviour
{
    [SerializeField] private SwapToBroken swapToBroken;
    
    private void OnEnable()
    {
        swapToBroken.dropWoodAndCoins += AddSpikesAndSticks;
    }

    private void OnDisable()
    {
        swapToBroken.dropWoodAndCoins -= AddSpikesAndSticks;
    }

    private void AddSpikesAndSticks()
    {
        GameManager.Instance.AddResource(GameManager.ResourceType.Wood, 5);
        GameManager.Instance.AddResource(GameManager.ResourceType.Coins, 2);
    }
}
