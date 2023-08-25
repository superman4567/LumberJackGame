using UnityEngine;

public class Chest : Interactable
{
    public enum ChestState
    {
        Inactive,   // No powerup available
        Active,     // Powerup available
        Used        // Powerup collected, waiting to destroy
    }

    public ChestState currentState = ChestState.Inactive;
    public float powerupDuration = 10f; // Duration of the powerup effect

    public override void InteractComplete()
    {
        base.InteractComplete();

        switch (currentState)
        {
            case ChestState.Inactive:
                Debug.Log("No powerup available.");
                break;
            case ChestState.Active:
                //ApplyPowerup();
                currentState = ChestState.Used;
                Invoke("DestroyChest", powerupDuration);
                break;
            case ChestState.Used:
                Debug.Log("Chest has already been used.");
                break;
        }

        Invoke("DestroyChest", 30f);
    }

    private void DestroyChest()
    {
        Destroy(gameObject);
    }
}
