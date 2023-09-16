using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BuildCampfire : MonoBehaviour, IDataPersistance
{
    [SerializeField] private GameObject campFirePrefab;
    [SerializeField] private PlayerThrowAxe playerThrowAxe;
    [SerializeField] private float distanceFromPlayer = 1.0f;
    [SerializeField] private int campfireCost = 10;
    public float campfireDuration = 6f;
    private PlayerMovement playerMovement;
    private Animator animator;
    private bool canBuildCampfire = true;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();    
    }

    public void LoadData(GameData data)
    {
        this.campfireDuration = data.campfireDuration;
    }

    public void SaveData(GameData data)
    {
        data.campfireDuration = this.campfireDuration;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && GameManager.Instance.GetWood() >= campfireCost && canBuildCampfire)
        {
            animator.SetBool("Pickups", true);
            canBuildCampfire = false;
            playerThrowAxe.enabled= false;
            playerMovement.enabled = false;
            GameManager.Instance.SubstractResource(GameManager.ResourceType.Wood, campfireCost);
            Invoke("BuildDone", 1.6f);
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
