using UnityEngine;

public class BlueprintManager : MonoBehaviour
{
    [SerializeField] public BlueprintData[] blueprints;
    public bool[] unlockedBlueprints;

    private static BlueprintManager instance;
    public static BlueprintManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockBlueprint(int blueprintIndex)
    {
        if (blueprintIndex >= 0 && blueprintIndex < blueprints.Length)
        {
            blueprints[blueprintIndex].isUnlocked = true;
        }
    }

    public bool IsBlueprintUnlocked(int blueprintIndex)
    {
        if (blueprintIndex >= 0 && blueprintIndex < unlockedBlueprints.Length)
        {
            return unlockedBlueprints[blueprintIndex];
        }
        return false;
    }

    public BlueprintData GetBlueprint(int blueprintIndex)
    {
        if (blueprintIndex >= 0 && blueprintIndex < blueprints.Length)
        {
            return blueprints[blueprintIndex];
        }
        return null;
    }
}
