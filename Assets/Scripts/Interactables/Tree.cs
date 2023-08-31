using System.Collections;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class Tree : Interactable
{
    [SerializeField] private AudioSource TreeBreakingSound;
    [SerializeField] private Transform fallingTreePrefab;
    [SerializeField] Image interactIcon;

    private void Start()
    {
        interactIcon.enabled = false;
    }

    public override void InteractComplete()
    {
        base.InteractComplete();
        TreeBreakingSound.Play();
       
        Instantiate(fallingTreePrefab, transform.position, transform.rotation, transform.parent);
        GameManager.Instance.AddResource(ResourceType.Wood, 5);
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactIcon.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactIcon.enabled = false;
        }
    }
}
