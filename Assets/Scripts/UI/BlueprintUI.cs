using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintUI : MonoBehaviour
{
    [Header("Blueprint icons")]
    [SerializeField] private Image[] bp_retrievedIcons;

    [Header("Blueprint titles")]
    [SerializeField] private TextMeshProUGUI[] bp_retrievedTitles;

    [Header("Blueprint descriptions")]
    [SerializeField] private TextMeshProUGUI[] bp_retrievedDescription;

    [Header("Blueprint default components")]
    [SerializeField] private Sprite lockedIcon;
    [SerializeField] private string defaultTitle = "aaaa";
    [SerializeField] private string defaultDescription = "bbbb";

    private void OnEnable()
    {
        RetreiveSOData();
    }

    private void RetreiveSOData()
    {
        int blueprintCount = BlueprintManager.Instance.blueprints.Length;
        Image[] iconImages = new Image[blueprintCount];
        TextMeshProUGUI[] nameLabel = new TextMeshProUGUI[blueprintCount];
        TextMeshProUGUI[] descriptionLabel = new TextMeshProUGUI[blueprintCount];

        for (int i = 0; i < blueprintCount; i++)
        {
            if (BlueprintManager.Instance.IsBlueprintUnlocked(i))
            {
                BlueprintData blueprintData = BlueprintManager.Instance.GetBlueprint(i);
                if (blueprintData != null)
                {
                    bp_retrievedIcons[i].sprite = blueprintData.icon;
                    bp_retrievedTitles[i].text = blueprintData.blueprintName;
                    bp_retrievedDescription[i].text = blueprintData.description;

                    iconImages[i].sprite = blueprintData.icon;
                    nameLabel[i].text = blueprintData.blueprintName;
                    descriptionLabel[i].text = blueprintData.description;
                }
            }
            else
            {
                iconImages[i].sprite = lockedIcon;
                nameLabel[i].text = defaultTitle;
                descriptionLabel[i].text = defaultDescription;
            }
        }

        AssignSOData(iconImages, nameLabel, descriptionLabel);
    }

    private void AssignSOData(Image[] iconImages, TextMeshProUGUI[] nameLabel, TextMeshProUGUI[] descriptionLabel)
    {
        for (int i = 0; i < iconImages.Length; i++)
        {
            bp_retrievedIcons[i].sprite = iconImages[i].sprite;
            bp_retrievedTitles[i].text = nameLabel[i].text;
            bp_retrievedDescription[i].text = descriptionLabel[i].text;
        }
    }
}
