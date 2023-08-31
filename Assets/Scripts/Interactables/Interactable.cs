using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    public float savedProgressInSeconds;
    public float interactInSeconds;

    public bool canBeInteractedWith = true;
    protected bool interactionComplete = false;

    
    [SerializeField] protected Image fill;
    [SerializeField] protected GameObject canvas;
    [SerializeField] protected Transform playerSocket;
    protected GameObject player;
    protected PlayerInteraction playerInteraction;
    protected PlayerMovement playerMovement;

    private bool isMovingToSocket = false;
    private Vector3 initialPlayerPosition;
    private Quaternion initialPlayerRotation;

    private void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
        canvas.SetActive(false);
    }

    public virtual void AddProgress(float progressInSeconds)
    {
        if (gameObject.tag == "Chest")
        {
            player.transform.SetParent(playerSocket);
            playerMovement.enabled = false;
        }
        else
        {
            player.transform.SetParent(null);
            playerMovement.enabled = true;
        }

        if (savedProgressInSeconds <= interactInSeconds)
        {
            savedProgressInSeconds += progressInSeconds;
            fill.fillAmount = savedProgressInSeconds / interactInSeconds;
        }
    }

    public virtual bool CheckProgressComplete()
    {
        if (savedProgressInSeconds >= interactInSeconds)
        {
            canvas.SetActive(false); 
        }
        return (savedProgressInSeconds >= interactInSeconds);
    }

    public virtual void InteractComplete()
    {
        interactionComplete = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerInteraction.currentInteractableObject == this)
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}
