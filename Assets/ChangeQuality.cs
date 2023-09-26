using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeQuality : MonoBehaviour
{
    public TextMeshProUGUI qualityText;
    public Toggle vSyncToggle;
    private bool isVSyncEnabled = true;
    private int currentQualityIndex;

    private void Start()
    {
        currentQualityIndex = QualitySettings.GetQualityLevel();
        isVSyncEnabled = QualitySettings.vSyncCount > 0;
        UpdateQualityText();
        vSyncToggle.isOn = isVSyncEnabled; // Set the toggle's state based on V-Sync status
    }

    public void IncreaseQuality()
    {
        if (currentQualityIndex < QualitySettings.names.Length - 1)
        {
            currentQualityIndex++;
            QualitySettings.SetQualityLevel(currentQualityIndex);
            UpdateQualityText();
        }
    }

    public void DecreaseQuality()
    {
        if (currentQualityIndex > 0)
        {
            currentQualityIndex--;
            QualitySettings.SetQualityLevel(currentQualityIndex);
            UpdateQualityText();
        }
    }

    private void UpdateQualityText()
    {
        qualityText.text = "Quality: " + QualitySettings.names[currentQualityIndex];
    }

    public void ToggleVSync()
    {
        isVSyncEnabled = vSyncToggle.isOn;
        QualitySettings.vSyncCount = isVSyncEnabled ? 1 : 0;
    }
}
