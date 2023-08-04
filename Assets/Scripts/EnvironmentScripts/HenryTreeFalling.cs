using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenryTreeFalling : MonoBehaviour, IInteractable
{
    [SerializeField] private float waitForTreeToFallTime;
    [SerializeField] private Rigidbody rigidBody;

    private PlayerInteraction playerInteraction;

    private bool isInteracting;
    private bool treeIsStatic;
    private float interactInSeconds;
    private float timer;

    // Start is called before the first frame update
    private void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckIfStatic();
        UpdateInteractionTime();
    }

    private void UpdateInteractionTime()
    {
        if (playerInteraction.GetInteractable() != (IInteractable)this) return;
        if (!isInteracting) return;

        interactInSeconds += Time.deltaTime;
        playerInteraction.holdingDownInteract = interactInSeconds;
    }

    private void CheckIfStatic()
    {
        timer += Time.deltaTime;

        if (timer > waitForTreeToFallTime)
        {
            if (Mathf.Approximately(rigidBody.velocity.magnitude, 0f))
            {
                treeIsStatic = true;
            }
        }
    }

    public void InteractComplete()
    {
        GameManager.Instance.AddWood();
        Destroy(gameObject);
    }

    public void StartInteract()
    {
        if (!treeIsStatic) return;
            
        isInteracting = true;
    }

    public void StopInteract()
    {
        isInteracting = false;
        playerInteraction.holdingDownInteract = 0;
    }

    
}
