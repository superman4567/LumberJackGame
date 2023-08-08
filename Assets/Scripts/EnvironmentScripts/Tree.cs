using System;
using System.Collections;
using UnityEngine;

public class Tree : Interactable
{
    [SerializeField] private AudioSource TreeBreakingSound;
    [SerializeField] private Transform fallingTreePrefab;

    public override void InteractComplete()
    {
        base.InteractComplete();
        TreeBreakingSound.Play();
        GameManager.Instance.AddWood();
        Instantiate(fallingTreePrefab, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
