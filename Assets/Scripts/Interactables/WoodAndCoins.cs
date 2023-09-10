using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodAndCoins : MonoBehaviour
{
    [SerializeField] private SwapToBroken swapToBroken;
    
    private void OnEnable()
    {
        swapToBroken.dropWoodAndCoins += AddWoodAndCoins;
    }

    private void OnDisable()
    {
        swapToBroken.dropWoodAndCoins -= AddWoodAndCoins;
    }

    private void AddWoodAndCoins()
    {
        int a = Random.Range(1, 2);
        int b = Random.Range(1, 3);
        GameManager.Instance.AddResource(GameManager.ResourceType.Wood, a) ;
        GameManager.Instance.AddResource(GameManager.ResourceType.Coins, b);
    }
}
