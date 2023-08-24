using UnityEngine;

public class Chest : Interactable
{
    public override void InteractComplete()
    {
        base.InteractComplete();
        
        Invoke("DestroyChest", 30f);
    }

    private void DestroyChest()
    {
        Destroy(gameObject);
    }
}
