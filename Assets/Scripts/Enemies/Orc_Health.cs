using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Orc_Health : MonoBehaviour
{
    [SerializeField] Orc_Ragdoll orc_Ragdoll;
    [SerializeField] Orc_Animations orc_Animations;
    [SerializeField] Orc_Attack orc_Attack;
    [SerializeField] Collider hitBox; // Change this to a trigger collider

    [SerializeField] private float knockbackForce = 10.0f;
    [SerializeField] private float knockbackDuration = 0.5f;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject floatingTextPrefab;
    private RoundManager roundManager;
    private AxeDetection axeDetection;

    public float maxHealth = 100;
    private float currentHealth;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    public bool isKnockbackActive = false;
    public bool orcIsDeath = false;
    private Vector3 knockbackDirection;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    private Material originalMat;

    private void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        axeDetection = FindObjectOfType<AxeDetection>();
    }

    private void Start()
    {
        OrcHealthStatemachine();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        SetNavMeshPriorityBasedOnDistance();
        if (isKnockbackActive)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + knockbackDirection, knockbackForce * Time.deltaTime);
        }
    }

    private void SetNavMeshPriorityBasedOnDistance()
    {
        if (navMeshAgent == null || player == null)
        {
            // Ensure the NavMeshAgent and player references are valid
            return;
        }

        // Calculate the distance between the orc and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Adjust the NavMeshAgent's priority based on distance
        // You can adjust these values as needed for your specific behavior
        if (distanceToPlayer < 10f)
        {
            navMeshAgent.avoidancePriority = 50;
        }
        else if (distanceToPlayer < 50f)
        {
            navMeshAgent.avoidancePriority = 20;
        }
    }


    public void KnockBack(Vector3 direction)
    {
        StartCoroutine(HitEffect(skinnedMeshRenderer, hitMaterial, 0.1f));
        isKnockbackActive = true;
        navMeshAgent.enabled = false;
        animator.SetFloat("OrcVelocity", 0f);
        knockbackDirection = direction.normalized * knockbackForce;
        Invoke("EndKnockback", knockbackDuration);
    }

    private void EndKnockback()
    {
        isKnockbackActive = false;
        if (orcIsDeath) { return; }

        navMeshAgent.enabled = true;
        animator.SetFloat("OrcVelocity", 1f);
        navMeshAgent.destination = player.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Axe") && other.isTrigger)
        {
            if (currentHealth > 0)
            {
                currentHealth -= axeDetection.axeDamage;
                ShowFloatingText();
            }
            else
            {
                animator.enabled = false;
                orc_Animations.enabled = false;
                orc_Attack.enabled = false;
                navMeshAgent.enabled = false;
                orc_Attack.navMeshAgent.enabled = false;
                hitBox.enabled = false;

                orc_Ragdoll.EnableRagdoll();
                roundManager.OrcKilled();
                Die();
            }
        }
    }

    protected IEnumerator HitEffect(SkinnedMeshRenderer meshRenderer, Material hitMaterial, float duration)
    {
        if (originalMat == null)
        {
            originalMat = meshRenderer.material;
        }

        meshRenderer.material = hitMaterial;
        yield return new WaitForSecondsRealtime(duration);
        meshRenderer.material = originalMat;
    }

    private void ShowFloatingText()
    {
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = axeDetection.axeDamage.ToString();
    }

    private void OrcHealthStatemachine()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3) 
        {
            maxHealth = 100;
            return; 
        }

        switch (GameManager.Instance.GetDifficulty())
        {
            case 0:
                if (roundManager.currentRound == 1) {return; }
                maxHealth = (maxHealth + (roundManager.currentRound * 1.25f));
                break;

            case 1:
                if (roundManager.currentRound == 1) { return; }
                maxHealth = (maxHealth + (roundManager.currentRound * 1.5f));
                break;

            case 2:
                if (roundManager.currentRound == 1) { return; }
                maxHealth = (maxHealth + (roundManager.currentRound * 2f));
                break;

            default:
                break;
        }
    }

    private void Die()
    {
        orcIsDeath = true;
        GameManager.Instance.AddResource(GameManager.ResourceType.Coins, Random.Range(1, 2));
        orc_Attack.Disablehands();
        Invoke("RemoveOrc", 10f);
    }

    private void RemoveOrc()
    {
        Destroy(gameObject);
    }
}
