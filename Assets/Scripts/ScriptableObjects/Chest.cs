using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
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
}
