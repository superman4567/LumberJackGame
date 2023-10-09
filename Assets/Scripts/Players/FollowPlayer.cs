using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform selectedObject; // Reference to the player's transform

    private void Update()
    {
        if (selectedObject != null)
        {
            if (selectedObject.tag == "Axe")
            {
                transform.position = selectedObject.position;
                transform.rotation = Quaternion.identity; 
            }
            else
            {
                transform.position = selectedObject.position;
            }
        }
    }
}
