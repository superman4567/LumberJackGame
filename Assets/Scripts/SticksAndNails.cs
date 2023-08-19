using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticksAndNails : MonoBehaviour
{
    [SerializeField] private SwapToBroken swapToBroken;
    
    private void OnEnable()
    {
        swapToBroken.dropSpikesAndSticks += AddSpikesAndSticks;
    }

    private void OnDisable()
    {
        swapToBroken.dropSpikesAndSticks -= AddSpikesAndSticks;
    }

    private void AddSpikesAndSticks()
    {
        GameManager.Instance.AddResource(GameManager.ResourceType.WoodenSpikes, 5);
        GameManager.Instance.AddResource(GameManager.ResourceType.Sticks, 5);
    }
}