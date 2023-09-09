using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BuildCampfire : MonoBehaviour
{
    [SerializeField] private GameObject campFirePrefab;
    [SerializeField] private float distanceFromPlayer = 1.0f;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerMovement.enabled = false;
            Invoke("BuildDone", 2f);
        }
    }

    private void BuildDone()
    {
        // Get the player's position and rotation
        Vector3 playerPosition = transform.position;
        Quaternion playerRotation = transform.rotation;

        // Calculate the position in front of the player
        Vector3 spawnPosition = playerPosition + playerRotation * Vector3.forward * distanceFromPlayer;

        // Instantiate the campfire at the calculated position and rotation
        Instantiate(campFirePrefab, spawnPosition, playerRotation);
        playerMovement.enabled = true;
    }
}
