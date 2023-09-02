using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [SerializeField] protected Transform playerSocket;
    [SerializeField] private Animator animator;

    public bool isOpen = false;
    private bool isopening = false;
    private PlayerMovement playerMovement;
    private GameObject player;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ChestInteract()
    {
        if (!isopening && !isOpen)
        {
            player.transform.SetParent(playerSocket);
            playerMovement.enabled = false;
            animator.SetTrigger("OpenChestTrigger");
            
            Invoke("UnlockPlayer", 1.8f);
            isOpen = true;
            Invoke("DestroyChest", 30f);

            isopening = true;
        }
    }

    public override void InteractComplete()
    {
        base.InteractComplete();
    }

    private void UnlockPlayer()
    {
        PowerUpManager.Instance.GrantRandomPowerUpToPlayer();
        player.transform.SetParent(null);
        playerMovement.enabled = true;
    }

    private void DestroyChest()
    {
        Destroy(gameObject);
    }
}
