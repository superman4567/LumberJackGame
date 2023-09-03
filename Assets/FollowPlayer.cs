using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    private void Update()
    {
        if (player != null)
        {
            // Set the position of this GameObject to follow the player with an offset
            transform.position = player.position;
        }
    }
}
