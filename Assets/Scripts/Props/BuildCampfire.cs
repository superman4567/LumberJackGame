using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildCampfire : MonoBehaviour
{
    [SerializeField] private Image campfireCooldown;
    [SerializeField] private GameObject campFirePrefab;
    [SerializeField] private PlayerThrowAxe playerThrowAxe;
    [SerializeField] private float distanceFromPlayer = 1.0f;
    [SerializeField] private int campfireCost = 10;
    public float campfireDuration = 6f;
    private PlayerMovement playerMovement;
    private Animator animator;
    private bool canBuildCampfire = true;
    private float cooldownTimer = 0f;
    private Image childImage;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        childImage = campfireCooldown.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            UpdateCooldownUI();
        }

        if (Input.GetKeyDown(KeyCode.C) && GameManager.Instance.GetWood() >= campfireCost && canBuildCampfire)
        {
            animator.SetBool("Pickups", true);
            canBuildCampfire = false;
            playerThrowAxe.enabled = false;
            playerMovement.enabled = false;
            GameManager.Instance.SubstractResource(GameManager.ResourceType.Wood, campfireCost);
            cooldownTimer = 1.6f;
            UpdateCooldownUI(); 

            if (childImage != null)
            {
                childImage.color = Color.gray;
            }

            StartCoroutine(BuildCampfireAfterCooldown());
        }
    }

    private void UpdateCooldownUI()
    {
        if (campfireCooldown != null)
        {
            // Calculate the fill amount based on the remaining cooldown time
            float fillAmount = 1f - Mathf.Clamp01(cooldownTimer / 1.6f); // Inverted fill calculation
            campfireCooldown.fillAmount = fillAmount;
        }
    }

    private IEnumerator BuildCampfireAfterCooldown()
    {
        yield return new WaitForSeconds(1.6f);

        playerThrowAxe.enabled = true;
        canBuildCampfire = true;

        // Get the player's position and rotation
        Vector3 playerPosition = transform.position;
        Quaternion playerRotation = transform.rotation;

        // Calculate the position in front of the player
        Vector3 spawnPosition = playerPosition + playerRotation * Vector3.forward * distanceFromPlayer;

        // Instantiate the campfire at the calculated position and rotation
        Instantiate(campFirePrefab, spawnPosition, playerRotation);
        animator.SetBool("Pickups", false);
        playerMovement.enabled = true;

        // Reset the cooldown timer
        cooldownTimer = 0f;
        UpdateCooldownUI(); // Update the UI to reset the fill

        // Reset the color of the child image
        if (childImage != null)
        {
            childImage.color = Color.white;
        }
    }
}
