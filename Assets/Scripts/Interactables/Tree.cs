using System.Collections;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;

public class Tree : Interactable
{
    [SerializeField] private AudioSource TreeBreakingSound;
    [SerializeField] private Transform fallingTreePrefab;
    [SerializeField] private GameObject canvas;

    public override void InteractComplete()
    {
        base.InteractComplete();
        TreeBreakingSound.Play();
       
        Instantiate(fallingTreePrefab, transform.position, transform.rotation, transform.parent);
        GameManager.Instance.AddResource(GameManager.ResourceType.Wood, 5);
        canvas.SetActive(false);

        Destroy(gameObject);
    }
}
