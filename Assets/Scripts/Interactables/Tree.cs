using System.Collections;
using System.Resources;
using UnityEngine;
using static GameManager;

public class Tree : Interactable
{
    [SerializeField] private AudioSource TreeBreakingSound;
    [SerializeField] private Transform fallingTreePrefab;
    [SerializeField] private GameObject TreeChild;
    
    private GameManager gameManager;
    private bool hasInstantiatedPrefab = false;

    public override void InteractComplete()
    {
        base.InteractComplete();
        TreeBreakingSound.Play();

        if (!hasInstantiatedPrefab)
        {
            Instantiate(fallingTreePrefab, transform.position, transform.rotation, transform.parent);
            hasInstantiatedPrefab = true;
            GameManager.Instance.AddResource(ResourceType.Wood, 5);
        }
        Destroy(TreeChild);
    }
}
