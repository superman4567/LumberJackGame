using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableRock : MonoBehaviour
{
    private RockPool rockPool;

    private void Awake()
    {
        rockPool = FindObjectOfType<RockPool>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("ReturnToPool", 5f);
    }

    private void ReturnToPool()
    {
        rockPool.ReturnRock(gameObject);
    }
}
