using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<OrcSpawner>().startToSpawnOrcs = true;
        }
    }
}
