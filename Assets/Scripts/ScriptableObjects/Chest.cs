using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [SerializeField] Image interactIcon;

    private void Start()
    {
        interactIcon.enabled = false;
    }

    public override void InteractComplete()
    {
        if (interactionComplete) { return; }
        base.InteractComplete();

        PowerUpManager.Instance.GrantRandomPowerUpToPlayer();

        Invoke("DestroyChest", 30f);
    }

    private void DestroyChest()
    {
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
