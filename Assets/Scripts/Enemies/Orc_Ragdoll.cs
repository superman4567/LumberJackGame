using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] ragdollBodies;
    [SerializeField] private Collider[] ragdollColliders;
    [SerializeField] private GameObject child;

    void Awake()
    {
        ragdollBodies = child.GetComponentsInChildren<Rigidbody>();
        ragdollColliders = child.GetComponentsInChildren<Collider>();
        DisableRagdoll();
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in ragdollBodies)
        {
            rigidbody.isKinematic = true;
            foreach (var collider in ragdollColliders)
            {
                collider.enabled = false;
            }
        }

    }

    public void EnableRagdoll()
    {
        foreach (var rigidbody in ragdollBodies)
        {
            rigidbody.isKinematic = false;
            foreach (var collider in ragdollColliders)
            {
                collider.enabled = true;
            }
        }
    }
}
