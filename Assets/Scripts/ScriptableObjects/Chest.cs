using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [SerializeField] protected Transform playerSocket;
    [SerializeField] private Animator animator;

    public bool isOpen = false;
    private PlayerMovement playerMovement;
    private GameObject player;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ChestInteract()
    {
        if (!isOpen)
        {
            animator.SetTrigger("OpenChestTrigger");

            player.transform.SetParent(playerSocket);
            player.transform.position = playerSocket.transform.position;
            player.transform.rotation = playerSocket.transform.rotation;
            playerMovement.enabled = false;
            isOpen = true;

            Invoke("UnlockPlayer", 1.8f);
            Invoke("DestroyChest", 30f);
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
