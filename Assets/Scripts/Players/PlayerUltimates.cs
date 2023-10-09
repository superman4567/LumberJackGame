using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUltimates : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private Skill damageSkill;
    [SerializeField] private Image damageImage;
    [SerializeField] private Image damageLockedImage;
    [SerializeField] private float damageUltCoolDown = 3f;
    [SerializeField] public float damageUltDuration = 3f;

    [SerializeField] private float damageUltAmount = 10f;
    [SerializeField] private float damageUltRadius = 1f;
    [SerializeField] private GameObject damageCircle;
    private Image damagechildImage;

    [Header("Healing")]
    [SerializeField] private Skill healingSkill;
    [SerializeField] private Image healingImage;
    [SerializeField] private Image healingLockedImage;
    [SerializeField] private float healingUltCoolDown = 3f;
    [SerializeField] public float healingUltDuration = 3f;

    [SerializeField] private float healingUltAmount = 10f;
    [SerializeField] private float healingUltRadius = 1f;
    [SerializeField] private GameObject healingTotum;
    private Image healingchildImage;

    [Header("Survival")]
    [SerializeField] private Skill survivalSkill;
    [SerializeField] private Image survivalImage;
    [SerializeField] private Image survivalLockedImage;
    [SerializeField] private float survivalUltCoolDown = 3f;
    [SerializeField] public float survivalUltDuration = 3f;

    [SerializeField] private GameObject survivalShields;
    [SerializeField] private float survivalUltShieldRadius = 3f;

    private Coroutine ultimateCooldownCoroutine;
    private Image survivalchildImage;

    [SerializeField] private PlayerEmmissionChange playerEmmissionChange;
    [SerializeField] private PlayerStats playerStats;

    private void Awake()
    {
        playerEmmissionChange = GetComponent<PlayerEmmissionChange>();
        playerStats = GetComponent<PlayerStats>();

        healingchildImage = healingImage.transform.GetChild(0).GetComponent<Image>();
        damagechildImage = damageImage.transform.GetChild(0).GetComponent<Image>();
        survivalchildImage = survivalImage.transform.GetChild(0).GetComponent<Image>();
    }

    void Start()
    {
        damageCircle.SetActive(false);
        UltimateSpriteUnlockCheck();
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }
        UltimateActivation();
    }

    public void UltimateSpriteUnlockCheck()
    {
        if (damageSkill.isUnlocked == false)
        {
            damageLockedImage.enabled = true;
        }
        else
        {
            playerEmmissionChange.damageActive = true;
            damageLockedImage.enabled = false;
        }

        if (healingSkill.isUnlocked == false)
        {
            healingLockedImage.enabled = true;
        }
        else
        {
            playerEmmissionChange.healingActive = true;
            healingLockedImage.enabled = false;
        }

        if (survivalSkill.isUnlocked == false)
        {
            survivalLockedImage.enabled = true;
        }
        else
        {
            playerEmmissionChange.surviveActive = true;
            survivalLockedImage.enabled = false;
        }
    }

    private void UltimateActivation()
    {
        Collider[] nearbyOrcs = Physics.OverlapSphere(transform.position, 18, LayerMask.GetMask("Enemy"));


        //Activate the Damagfe Ultimate
        if (playerEmmissionChange.damageActive && Input.GetKey(KeyCode.Alpha1))
        {
            if (playerEmmissionChange.isWaiting == false)
            {
                damagechildImage.color = Color.gray;
                StartUltimateCooldown(damageUltCoolDown, damageImage, damagechildImage);
                StartCoroutine(playerEmmissionChange.ActivateUltimateprites(damageUltDuration, playerEmmissionChange.emissionColorDamage, playerEmmissionChange.emissionIntensityTriggered));
                playerEmmissionChange.ColorChange(playerEmmissionChange.emissionColorDamage, playerEmmissionChange.emissionIntensityTriggered);
                StartCoroutine(DamageUltimate());
            }
        }
        //Activate the Healing Ultimate
        else if (playerEmmissionChange.healingActive && Input.GetKey(KeyCode.Alpha2))
        {
            if (playerEmmissionChange.isWaiting == false)
            {
                healingchildImage.color = Color.gray;
                StartUltimateCooldown(healingUltCoolDown, healingImage, healingchildImage);
                StartCoroutine(playerEmmissionChange.ActivateUltimateprites(healingUltDuration, playerEmmissionChange.emissionColorHealing, playerEmmissionChange.emissionIntensityTriggered));
                playerEmmissionChange.ColorChange(playerEmmissionChange.emissionColorHealing, playerEmmissionChange.emissionIntensityTriggered);
                StartCoroutine(HealingUltimate());
            }
        }

        //Activate the Survive Ultimate
        else if (playerEmmissionChange.surviveActive && Input.GetKey(KeyCode.Alpha3))
        {
            if (playerEmmissionChange.isWaiting == false)
            {
                survivalchildImage.color = Color.gray;
                StartUltimateCooldown(survivalUltCoolDown, survivalImage, survivalchildImage);
                StartCoroutine(playerEmmissionChange.ActivateUltimateprites(survivalUltDuration, playerEmmissionChange.emissionColorSurvive, playerEmmissionChange.emissionIntensityTriggered));
                playerEmmissionChange.ColorChange(playerEmmissionChange.emissionColorSurvive, playerEmmissionChange.emissionIntensityTriggered);
                StartCoroutine(SurvivalUltimate());
            }
        }

        else
        {
            if (nearbyOrcs.Length > 0 && playerEmmissionChange.isWaiting == false)
            {
                playerEmmissionChange.ColorChange(playerEmmissionChange.emissionColorRage, playerEmmissionChange.emissionIntensityTriggered);
            }

            if (nearbyOrcs.Length == 0 && playerEmmissionChange.isWaiting == false)
            {
                playerEmmissionChange.ResetEmmision();
            }
        }
    }

    private IEnumerator DamageUltimate()
    {
        float damageUltTimeElapsed = 0f;
        damageCircle.SetActive(true);
        while (damageUltTimeElapsed < damageUltDuration)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageUltRadius);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out Orc_Health enemyHealth))
                {
                    enemyHealth.TakeDamage(10f);
                }
            }
            damageUltTimeElapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
        damageCircle.SetActive(false);
    }

    private IEnumerator HealingUltimate()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 1f;
        Instantiate(healingTotum, spawnPosition, Quaternion.identity);
        yield return null;
    }

    private IEnumerator SurvivalUltimate()
    {
        survivalShields.SetActive(true);
        playerStats.ult3 = true;
        playerStats.reducer = 10;
        
        yield return new WaitForSeconds(survivalUltDuration);
        
        survivalShields.SetActive(false);
        playerStats.ult3 = false;
        playerStats.reducer = 1;
    }

    private void StartUltimateCooldown(float ultDuration, Image fillImage, Image abilityIcon)
    {
        if (ultimateCooldownCoroutine != null)
        {
            StopCoroutine(ultimateCooldownCoroutine);
        }

        ultimateCooldownCoroutine = StartCoroutine(UltimateCooldown(ultDuration, fillImage, abilityIcon));
    }

    private IEnumerator UltimateCooldown(float ultDuration, Image fillImage, Image abilityIcon)
    {
        float timer = 0f;
        fillImage.fillAmount = 0f; // Start with fill at 0

        while (timer < ultDuration)
        {
            float fillAmount = Mathf.Clamp01(timer / ultDuration); // Inverted fill calculation
            fillImage.fillAmount = fillAmount;
            timer += Time.deltaTime;

            yield return null;
        }

        fillImage.fillAmount = 1f; // Set fill to 1 when cooldown is complete
        abilityIcon.color = Color.white;
        ultimateCooldownCoroutine = null; // Reset the coroutine reference
    }
}
