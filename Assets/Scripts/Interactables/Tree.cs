using System.Collections;
using System.Resources;
using UnityEngine;
using static GameManager;

public class Tree : Interactable
{
    [SerializeField] private AudioSource TreeBreakingSound;
    [SerializeField] private Transform fallingTreePrefab;

    public override void InteractComplete()
    {
        base.InteractComplete();
        TreeBreakingSound.Play();
       
        Instantiate(fallingTreePrefab, transform.position, transform.rotation, transform.parent);
        GameManager.Instance.AddResource(ResourceType.Wood, 5);
        
        Destroy(gameObject);
    }
}
