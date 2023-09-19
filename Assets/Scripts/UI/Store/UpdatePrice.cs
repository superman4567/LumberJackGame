using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePrice : MonoBehaviour
{
    private TextMeshProUGUI textMeshProText;
    private StoreManager storeManager;
    [SerializeField] public Skill holdingSkill;

    private void Start()
    {
        storeManager = GameObject.FindGameObjectWithTag("StoreManager").GetComponent<StoreManager>();
        textMeshProText = GameObject.FindGameObjectWithTag("CostAmount").GetComponent<TextMeshProUGUI>();
        textMeshProText.text = "";
    }

    public void UpdateTextOnClick()
    {
        //Set the price amount UI
        textMeshProText.text = holdingSkill.skillCost.ToString();
        storeManager.selectedAbility = holdingSkill;
        storeManager.UpdatePurchaseStateSprite();
    }
}
