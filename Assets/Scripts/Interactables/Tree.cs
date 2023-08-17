using System.Collections;
using UnityEngine;

public class Tree : Interactable
{
    [SerializeField] private AudioSource TreeBreakingSound;
    [SerializeField] private Transform fallingTreePrefab;
    [SerializeField] private GameObject TreeChild;

    private bool hasInstantiatedPrefab = false;

    public override void InteractComplete()
    {
        base.InteractComplete();
        TreeBreakingSound.Play();

        if (!hasInstantiatedPrefab)
        {
            Instantiate(fallingTreePrefab, transform.position, transform.rotation, transform.parent);
            hasInstantiatedPrefab = true;
            GameManager.Instance.AddWood();
        }
        Destroy(TreeChild);
    }
}
