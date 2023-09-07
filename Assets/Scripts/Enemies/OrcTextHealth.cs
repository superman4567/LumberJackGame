using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcTextHealth : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] Vector3 offset = new Vector3(0, 2, 0);
    [SerializeField] Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 1);
    
    void Start()
    {
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
        Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
        Destroy(gameObject, destroyTime);
    }
}
