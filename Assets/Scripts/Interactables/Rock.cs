using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Interactable
{
    [SerializeField] private AudioSource RockBreakingSound;
    [SerializeField] private Transform brokenRock;

    public override void InteractComplete()
    {
        base.InteractComplete();
        RockBreakingSound.Play();
        GameManager.Instance.AddWood();
        Instantiate(brokenRock, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
