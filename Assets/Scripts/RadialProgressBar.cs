using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private Image radialFill;
    [SerializeField] private Image radialBG;
    public TreeScript treeScript;
    private Camera mainCamera;
    public float alphaValue = 0.5f;

    private void Start()
    {
        radialFill.fillAmount = 0;
        radialFill = GetComponent<Image>();
        mainCamera = Camera.main;
        SetImageAlpha(0f);
    }

    private void Update()
    {
        UpdateRadialImage();
        PlayerIsInteracting();
    }

    private void PlayerIsInteracting()
    {
        //if (Player.isHoldingKey == true)
        //{
        //    SetImageAlpha(1f);
        //}
        //else if (Player.isHoldingKey == false)
        //{
        //    SetImageAlpha(0f);
        //}
    }

    public void SetImageAlpha(float alpha)
    {
        // Ensure alpha is clamped between 0 and 1
        alpha = Mathf.Clamp01(alpha);

        // Get the current color of the Image
        Color imageColorRadialFill = radialFill.color;
        Color imageColorRadialBG = radialBG.color;

        // Update the alpha value of the color
        imageColorRadialFill.a = alpha;
        imageColorRadialBG.a = alpha;

        // Assign the updated color back to the Image
        radialFill.color = imageColorRadialFill;
        radialBG.color = imageColorRadialBG;
    }

    private void UpdateRadialImage()
    {
        radialFill.fillAmount = treeScript.GetTreeValue() / 100f; // Assuming treeValue is an integer from 0 to 100
    }
}
