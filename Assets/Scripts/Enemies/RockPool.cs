using System.Collections.Generic;
using UnityEngine;

public class RockPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    public Transform parentTransform; // Reference to the parent empty GameObject

    private Queue<GameObject> pooledObjects;

    private void Start()
    {
        pooledObjects = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parentTransform);
            obj.SetActive(false);
            pooledObjects.Enqueue(obj);
        }
    }

    public GameObject GetRock()
    {
        if (pooledObjects.Count > 0)
        {
            GameObject obj = pooledObjects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    public void ReturnRock(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(parentTransform); // Set the parent when returning the object
        pooledObjects.Enqueue(obj);
    }
}
