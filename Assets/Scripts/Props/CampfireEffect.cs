using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireEffect : MonoBehaviour
{
    private static CampfireEffect currentCampFire;
    private PlayerStats player;
    private BuildCampfire buildCampfire;

    private void Awake()
    {
        buildCampfire = FindObjectOfType<BuildCampfire>();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        Destroy(gameObject, buildCampfire.campfireDuration);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (currentCampFire == null) 
            {
                currentCampFire = this;
            }

            if (currentCampFire != this) { return; }

            player.AddHealth(.5f * Time.deltaTime);
            player.AddStamina(.8f * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;

        if (currentCampFire == this)
        {
            currentCampFire= null;
        }
    }

    private void OnDestroy()
    {
        if (currentCampFire == this)
        {
            currentCampFire = null;
        }
    }
}
