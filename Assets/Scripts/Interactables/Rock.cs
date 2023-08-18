using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Rock : Interactable
{
    [SerializeField] private AudioSource RockBreakingSound;
    [SerializeField] private Transform brokenRock;
    [SerializeField] private int addRocksMin = 3;
    [SerializeField] private int addRocksMax = 10;
    private int randomAddRocks;

    public override void InteractComplete()
    {
        base.InteractComplete();
        RockBreakingSound.Play();

        randomAddRocks = Random.Range(addRocksMin, addRocksMax);

        GameManager.Instance.AddResource(ResourceType.Rock, randomAddRocks);
        Instantiate(brokenRock, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
