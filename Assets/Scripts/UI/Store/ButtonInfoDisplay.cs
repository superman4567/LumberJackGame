using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoMouseCanvas;
    [SerializeField] private RectTransform infoMouseRect;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillDescriptionText;

    private bool isMouseOver = false; // Flag to track if the mouse is over the UI element

    private void Start()
    {
        // Hide the info panel initially
        infoMouseCanvas.SetActive(false);
    }

    private void Update()
    {
        // Only update the position if the info panel is active and the mouse is over the UI element
        if (infoMouseCanvas.activeSelf && isMouseOver)
        {
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Convert the screen space mouse position to canvas space
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                infoMouseRect.parent as RectTransform,
                mousePosition,
                null,
                out Vector2 localMousePosition
            );

            // Set the position of the panel so that its top-left corner is at the mouse position
            infoMouseRect.localPosition = localMousePosition;
        }
    }

    public void OnPointerEnter(UpdatePrice so)
    {
        skillNameText.text = so.holdingSkill.skillName;
        skillDescriptionText.text = so.holdingSkill.skillDescription;

        // Show the info panel
        infoMouseCanvas.SetActive(true);

        // Set the flag to true when the mouse enters the UI element
        isMouseOver = true;
    }

    public void OnPointerExit()
    {
        // Hide the info panel
        infoMouseCanvas.SetActive(false);

        // Set the flag to false when the mouse exits the UI element
        isMouseOver = false;
    }
}
