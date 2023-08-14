using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private int blueprintIndex;

    public override void InteractComplete()
    {
        base.InteractComplete();
        BlueprintManager.Instance.UnlockBlueprint(blueprintIndex);
        Destroy(gameObject);
    }
}
