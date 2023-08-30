using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] ragdollBodies;
    [SerializeField] private GameObject child;

    void Awake()
    {
        ragdollBodies = child.GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in ragdollBodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void EnableRagdoll()
    {
        foreach (var rigidbody in ragdollBodies)
        {
            rigidbody.isKinematic = false;
        }
    }
}
