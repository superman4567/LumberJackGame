using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Animations : MonoBehaviour
{
    [SerializeField] Animator animator;

    private int OrcVelocityHash;
    private int OrcIdleAnimationsHash;

    private float idleAnimation1Frequency = 80f;
    private float idleAnimation1Frequency2 = 20f;
    private float timeSinceLastIdleChange = 0f;

    // Start is called before the first frame update
    void Start()
    {
        OrcIdleAnimationsHash = Animator.StringToHash("OrcIdleAnimations");
        OrcVelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        IdleChange();
    }

    private void IdleChange()
    {
        timeSinceLastIdleChange += Time.deltaTime;

        if (timeSinceLastIdleChange >= Random.Range(3f, 6f))
        {
            if (Random.Range(0f, 100f) <= idleAnimation1Frequency)
            {
                animator.SetFloat(OrcIdleAnimationsHash, 0f);
            }
            else if (Random.Range(0f, 100f) >= idleAnimation1Frequency2)
            {
                animator.SetFloat(OrcIdleAnimationsHash, 0.5f);
            }
            else
            {
                animator.SetFloat(OrcIdleAnimationsHash, 1f);
            }

            timeSinceLastIdleChange = 0f;
        }
    }
}
