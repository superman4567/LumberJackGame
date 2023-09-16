using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmmissionChange : MonoBehaviour, IDataPersistance
{
    public SkinnedMeshRenderer playerRenderer;
    public TrailRenderer trailRenderer;
    public float lerpSpeed = .5f;
    private bool isWaiting = false;

    private float currentEmissionIntensity;
    private Color currentEmissionColor;

    private float emissionIntensityDefault = 0.1f;
    private float emissionIntensityTriggered = 15f;

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

    public void LoadData(GameData data)
    {
        this.healingActive = data.healingActive;
        this.damageActive = data.damageActive;
        this.surviveActive = data.surviveActive;
    }

    public void SaveData(GameData data)
    {
        data.healingActive = this.healingActive;
        data.damageActive = this.damageActive;
        data.surviveActive = this.surviveActive;


    }

    void Update()
    {
        AxeDetectionEnemies();
    }

    private void AxeDetectionEnemies()
    {
        Collider[] nearbyOrcs = Physics.OverlapSphere(transform.position, 18, LayerMask.GetMask("Enemy"));

        //Activate the Healing Ultimate
        if (healingActive && Input.GetKey(KeyCode.Alpha1))
        {
            if (!isWaiting)
            {
                StartCoroutine(ActivateUltimateWithDelay(2f, emissionColorHealing, emissionIntensityTriggered));
                ColorChange(emissionColorHealing, emissionIntensityTriggered);
            }
        }

        //Activate the Damagfe Ultimate
        else if (damageActive && Input.GetKey(KeyCode.Alpha2))
        {
            if (!isWaiting)
            {
                StartCoroutine(ActivateUltimateWithDelay(2f, emissionColorDamage, emissionIntensityTriggered));
                ColorChange(emissionColorDamage, emissionIntensityTriggered);
            }
        }

        //Activate the Survive Ultimate
        else if (surviveActive && Input.GetKey(KeyCode.Alpha3))
        {
            if (!isWaiting)
            {
                StartCoroutine(ActivateUltimateWithDelay(2f, emissionColorSurvive, emissionIntensityTriggered));
                ColorChange(emissionColorSurvive, emissionIntensityTriggered);
            }
        }

        else
        {
            if (nearbyOrcs.Length > 0 && !isWaiting)
            {
                ColorChange(emissionColorRage, emissionIntensityTriggered);
            }

            if (nearbyOrcs.Length == 0 && !isWaiting)
            {
                ResetEmmision();
            }
        }
    }

    private IEnumerator ActivateUltimateWithDelay(float delay, Color selectedColor, float targetIntensity)
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

    private void ColorChange(Color selectedColor, float intensity)
    {
        currentEmissionColor = Color.Lerp(currentEmissionColor, selectedColor, lerpSpeed * Time.deltaTime);
        currentEmissionIntensity = Mathf.Lerp(currentEmissionIntensity, intensity, lerpSpeed * Time.deltaTime);
        playerRenderer.material.SetColor("_EmissionColor", currentEmissionColor * currentEmissionIntensity);

        Material trailMaterial = trailRenderer.material;
        trailMaterial.SetColor("_Color", selectedColor);
        trailMaterial.SetColor("_EmissionColor", selectedColor);
    }

    private void ResetEmmision()
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
