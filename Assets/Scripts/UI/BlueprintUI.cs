using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintUI : MonoBehaviour
{
    [Header("Overview UI")]
    [SerializeField] private Image overviewImage;
    [SerializeField] private TMP_Text overviewTitle;
    [SerializeField] private TMP_Text overviewDescription;

    [Header("Blueprint Prefab")]
    [SerializeField] private Transform blueprintPrefab;
    public int currentBPbyIndex;

    [Header("UI Components")]
    [SerializeField] private Transform prefabParent;

    [Header("Blueprint default components")]
    [SerializeField] private Sprite lockedIcon;
    [SerializeField] private string defaultTitle = "aaaa";
    [SerializeField] private string defaultDescription = "bbbb";

    private void OnEnable()
    {
        ResetChildren();
        InstatiatePrefabs();
        SetFirstPrefabToPreview();
    }

   

    private void ResetChildren()
    {
        foreach (Transform child in prefabParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void InstatiatePrefabs()
    {
        BlueprintData[] blueprints = BlueprintManager.Instance.blueprints;

        for (int i = 0; i < blueprints.Length; i++)
        {
            Transform prefab = Instantiate(blueprintPrefab, prefabParent);

            if (blueprints[i].isUnlocked)
            {
                prefab.GetChild(0).GetComponent<Image>().sprite = blueprints[i].icon;
                prefab.GetChild(0).GetComponentInChildren<TMP_Text>().text = blueprints[i].blueprintName;
            } else
            {
                prefab.GetChild(0).GetComponent<Image>().sprite = lockedIcon;
                prefab.GetChild(0).GetComponentInChildren<TMP_Text>().text = defaultTitle;
                prefab.GetComponent<Button>().interactable = false;
            }
            
            int prefabValue = i;

            prefab.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (!blueprints[prefabValue].isUnlocked) return;
                currentBPbyIndex = prefabValue;

                overviewImage.sprite = blueprints[prefabValue].icon;
                overviewTitle.text = blueprints[prefabValue].blueprintName;
                overviewDescription.text = blueprints[prefabValue].description;
            });
        }
    }

    private void SetFirstPrefabToPreview()
    {
        BlueprintData[] blueprints = BlueprintManager.Instance.blueprints;

        if (blueprints[0].isUnlocked)
        {
            overviewImage.sprite = blueprints[0].icon;
            overviewTitle.text = blueprints[0].blueprintName;
            overviewDescription.text = blueprints[0].description;

            prefabParent.GetChild(0).GetComponent<Button>().Select();
        }
    }

    public void BuildBP()
    {
       FindObjectOfType<PlacingStructures>().SelectObject(currentBPbyIndex);
    }

}
