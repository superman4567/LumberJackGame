using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;

    void Start()
    {
        // Loop through child empty GameObjects
        foreach (Transform spawnPoint in transform)
        {
            GameObject newChest = Instantiate(chestPrefab, spawnPoint.position, spawnPoint.rotation);
            // You can also set chest properties or upgrades here
        }
    }
    
}
