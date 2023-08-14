using UnityEngine;
using UnityEngine.UI;

public class BlueprintUI : MonoBehaviour
{
    [SerializeField] private Image[] iconImages;
    [SerializeField] private Text[] nameLabel;

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < iconImages.Length; i++)
        {
            if (BlueprintManager.Instance.IsBlueprintUnlocked(i))
            {
                BlueprintData blueprintData = BlueprintManager.Instance.GetBlueprint(i);
                if (blueprintData != null)
                {
                    iconImages[i].sprite = blueprintData.icon;
                    nameLabel[i].text = blueprintData.blueprintName;
                }
            }
            else
            {
                iconImages[i].sprite = null;
                nameLabel[i].text = "Locked";
            }
        }
    }
}
