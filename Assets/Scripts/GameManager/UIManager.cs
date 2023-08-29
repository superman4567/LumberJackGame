using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    public Button[] buttons;

    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => SelectButton(button));
        }

        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        GameManager gameManager = GameManager.Instance;

        foreach (Button button in buttons)
        {
            bool shouldBeSelected = ShouldButtonBeSelected(button, gameManager);
            button.interactable = shouldBeSelected;
        }

        EnsureOneButtonSelected();
    }

    private bool ShouldButtonBeSelected(Button button, GameManager gameManager)
    {
        // Implement your logic here based on the button's properties and GameManager variables
        // For example:
        if (button.name == "Button1")
        {
            return true;  // Button 1 is always enabled
        }
        //else if ((button.name == "Button2" || button.name == "Button3") && gameManager.IsUnlocked())
        {
            // Enable Button 2 and 3 only if the game manager indicates they are unlocked
            return true;
        }

        return false;
    }

    private void SelectButton(Button selectedButton)
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

        selectedButton.interactable = false;
    }

    private void EnsureOneButtonSelected()
    {
        bool anyButtonSelected = false;
        foreach (Button button in buttons)
        {
            if (!button.interactable)
            {
                anyButtonSelected = true;
                break;
            }
        }

        if (!anyButtonSelected)
        {
            buttons[0].interactable = false;
        }
    }
}
