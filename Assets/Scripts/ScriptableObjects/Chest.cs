using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private int blueprintIndex;

    public override void InteractComplete()
    {
        base.InteractComplete();
        BlueprintManager.Instance.UnlockBlueprint(blueprintIndex);


        //Show in the UI what you have unlocked
        Invoke("DestroyChest", 60f);
    }

    private void DestroyChest()
    {
        Destroy(gameObject);
    }
}
