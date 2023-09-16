using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoMouseCanvas; 
    [SerializeField] private UpdatePrice currentButtonPrice; 

    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillDescriptionText;

    void Start()
    {
        infoMouseCanvas.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject; // Get the GameObject that was hit

            Button button = hitObject.GetComponent<Button>();
            if (button != null)
            {
                // Get the UpdatePrice script from the button
                currentButtonPrice = button.GetComponent<UpdatePrice>();

                if (currentButtonPrice != null)
                {
                    infoMouseCanvas.SetActive(true);
                    skillNameText.text = currentButtonPrice.holdingSkill.skillName;
                    skillDescriptionText.text = currentButtonPrice.holdingSkill.skillDescription;

                    return; // Exit the loop once a button is found under the mouse
                }
            }
            else
            {
                Debug.Log("button is null");
            }
        }

        // No button is under the mouse, hide the infoCanvas
        infoMouseCanvas.SetActive(false);
    }
}


