using UnityEngine;

public class Chest : Interactable
{
    private PowerUpManager.PowerUpType storedPowerUp;

    public void AssignPowerUp(PowerUpManager.PowerUpType powerUpType)
    {
        storedPowerUp = powerUpType;
        Debug.Log("Assigned Power-Up to Chest: " + powerUpType);
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
}
