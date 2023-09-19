using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmmissionChange : MonoBehaviour
{
    public SkinnedMeshRenderer playerRenderer;
    public TrailRenderer trailRenderer;
    public float lerpSpeed = .5f;
    public bool isWaiting = false;

    public float currentEmissionIntensity;
    private Color currentEmissionColor;

    public float emissionIntensityDefault = 0.1f;
    public float emissionIntensityTriggered = 15f;

    public Color emissionColorDefault;
    public Color emissionColorRage;

    public Color emissionColorHealing;
    public Color emissionColorDamage;
    public Color emissionColorSurvive;

    public bool healingActive = false;
    public bool damageActive = false;
    public bool surviveActive = false;

    void Start()
    {
        playerRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        currentEmissionIntensity = emissionIntensityDefault;
        currentEmissionColor = emissionColorDefault;
        playerRenderer.material.SetColor("_EmissionColor", currentEmissionColor * currentEmissionIntensity);
    }

    public IEnumerator ActivateUltimateprites(float delay, Color selectedColor, float targetIntensity)
    {
        isWaiting = true;

        float initialIntensity = playerRenderer.material.GetColor("_EmissionColor").a;
        float elapsedTime = 0f;

        while (elapsedTime < delay)
        {
            ColorChange(selectedColor, targetIntensity);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        isWaiting = false;
        ResetEmmision();
    }

    public void ColorChange(Color selectedColor, float intensity)
    {
        currentEmissionColor = Color.Lerp(currentEmissionColor, selectedColor, lerpSpeed * Time.deltaTime);
        currentEmissionIntensity = Mathf.Lerp(currentEmissionIntensity, intensity, lerpSpeed * Time.deltaTime);
        playerRenderer.material.SetColor("_EmissionColor", currentEmissionColor * currentEmissionIntensity);

        Material trailMaterial = trailRenderer.material;
        trailMaterial.SetColor("_Color", selectedColor);
        trailMaterial.SetColor("_EmissionColor", selectedColor);
    }

    public void ResetEmmision()
    {
        currentEmissionColor = Color.Lerp(currentEmissionColor, emissionColorDefault, lerpSpeed * Time.deltaTime);
        currentEmissionIntensity = Mathf.Lerp(currentEmissionIntensity, emissionIntensityDefault, lerpSpeed * Time.deltaTime);
        playerRenderer.material.SetColor("_EmissionColor", emissionColorDefault);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 18f);
    }
}
