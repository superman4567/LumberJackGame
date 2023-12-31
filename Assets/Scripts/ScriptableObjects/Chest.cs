using System;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [SerializeField] protected Transform playerSocket;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject canvas;

    public static Action destroyChestAction;
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
            // Add Chest Opening Sound
            AkSoundEngine.PostEvent("Play_Chest_Opening_SFX", gameObject);
            player.transform.SetParent(playerSocket);
            player.transform.position = playerSocket.transform.position;
            player.transform.rotation = playerSocket.transform.rotation;
            playerMovement.enabled = false;
            isOpen = true;

            GameManager.Instance.ChestsOpenedAdd();
            GameManager.Instance.thisRunChestsOpened++;
            Invoke("UnlockPlayer", 1.8f);
        }
    }

    public override void InteractComplete()
    {
        base.InteractComplete();
    }

    private void UnlockPlayer()
    {
        canvas.SetActive(false);
        PowerUpManager.Instance.GrantRandomPowerUpToPlayer();
        player.transform.SetParent(null);
        playerMovement.enabled = true;

        destroyChestAction?.Invoke();
        Invoke("DestroyChest", 5f);
        
    }

    private void DestroyChest()
    {
        Destroy(gameObject);
    }
}
