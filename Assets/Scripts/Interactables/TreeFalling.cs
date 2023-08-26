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
            if (Mathf.Approximately(rigidBody.velocity.magnitude, 0f))
            {
                treeIsStatic = true;
                canBeInteractedWith = true;
                rigidBody.isKinematic = true;
            }
            else
            {
                treeIsStatic = false;
                canBeInteractedWith = false;
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

            Destroy(gameObject);
        }
    }
}
