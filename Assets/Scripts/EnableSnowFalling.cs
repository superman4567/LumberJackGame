using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSnowFalling : MonoBehaviour
{
    public ParticleSystem snow;

    private void Start()
    {
        snow.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            snow.Play();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            snow.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            snow.Stop();
        }
    }
}
