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
        DisableRagdollOnStart();
    }

    private void DisableRagdollOnStart()
    {
        foreach (var rigidbody in ragdollBodies)
        {
            rigidbody.isKinematic = true;
            foreach (var collider in ragdollColliders)
            {
                collider.enabled = false;

                if (collider.name == "hand.r" || collider.name == "hand.l")
                {
                    collider.enabled = true;
                }
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

                if (collider.name == "hand.r" || collider.name == "hand.l")
                {
                    collider.enabled = false;
                }
            }
        }
    }
}
