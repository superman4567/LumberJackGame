using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableRock : MonoBehaviour
{
    private ObjectPool rockPool;
    private Orc_DealDamage damageScript;

    private void Awake()
    {
        rockPool = FindObjectOfType<ObjectPool>();
        damageScript = GetComponent<Orc_DealDamage>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        damageScript.enabled= false;
        Invoke("ReturnToPool", 5f);
    }

    private void ReturnToPool()
    {
        damageScript.enabled = true;
        rockPool.ReturnRock(gameObject);
    }
}
