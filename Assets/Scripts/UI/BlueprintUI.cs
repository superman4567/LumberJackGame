using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintUI : MonoBehaviour
{
    [SerializeField] private Image lockedImage;
    [SerializeField] private TextMeshProUGUI lockedTitle;
    [SerializeField] private TextMeshProUGUI lockedDescription;

    private Image[] iconImages;
    private TextMeshProUGUI[] nameLabel;
    private TextMeshProUGUI[] descriptionLabel;

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
                    descriptionLabel[i].text = blueprintData.description;
                }
            }
            else
            {
                iconImages[i].sprite = lockedImage.sprite;
                nameLabel[i].text = "Locked";
                descriptionLabel[i].text = "...";
            }
        }
    }
}
