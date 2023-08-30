using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startSpawing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<OrcSpawner>().startToSpawnOrcs = true;
        }
    }
}
