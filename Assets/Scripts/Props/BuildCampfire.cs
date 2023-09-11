using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BuildCampfire : MonoBehaviour
{
    [SerializeField] private GameObject campFirePrefab;
    [SerializeField] private PlayerThrowAxe playerThrowAxe;
    [SerializeField] private float distanceFromPlayer = 1.0f;
    [SerializeField] private int campfireCost = 10;
    private PlayerMovement playerMovement;
    private Animator animator;
    private bool canBuildCampfire = true;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && GameManager.Instance.GetWood() >= campfireCost && canBuildCampfire)
        {
            canBuildCampfire = false;
            playerThrowAxe.enabled= false;
            GameManager.Instance.SubstractResource(GameManager.ResourceType.Wood, campfireCost);
            animator.SetBool("Pickups", true);
            playerMovement.enabled = false;
            Invoke("BuildDone", 2.6f);
        }
    }

    private void BuildDone()
    {
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

    }
}
