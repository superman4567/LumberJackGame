using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc_Animations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent orcNavMeshAgent;

    private int OrcVelocityHash;
    private float OrcIdleAnimationsHash;

    private Orc_Health orcHealth;
    private float timeSinceLastIdleChange = 0f;

    private void Start()
    {
        orcHealth = GetComponent<Orc_Health>();
        OrcIdleAnimationsHash = Animator.StringToHash("OrcIdleAnimations");
        OrcVelocityHash = Animator.StringToHash("OrcVelocity");
    }

    private void Update()
    {
        OrcVelocity();
    }

    private void IdleChange()
    {
        timeSinceLastIdleChange += Time.deltaTime;

        if (timeSinceLastIdleChange >= Random.Range(3f, 6f))
        {
            float newIdleState = Random.Range(0f, 1f);

            animator.SetFloat("OrcIdleAnimations", newIdleState);

            timeSinceLastIdleChange = 0f;
        }
    }

    private void OrcVelocity()
    {
        if (orcNavMeshAgent.speed == 0)
        {
            IdleChange();
            UpdateMovementAnimationState(0f);
        }
        else if (orcNavMeshAgent.speed >= 1f )
        {
            UpdateMovementAnimationState(1f);
        }
    }

    private void UpdateMovementAnimationState(float value)
    {
        animator.SetFloat(OrcVelocityHash, value);
    }

    public void OrcSlashSound()
    {
        // Snow
        AkSoundEngine.PostEvent("Play_Orc_Slash", gameObject);
    }

    public void OrcThrowRockSound()
    {
        // Snow
        AkSoundEngine.PostEvent("Play_Orc_Throwing_Rock_SFX", gameObject);
    }

    public void OrcFootStepSound()
    {
        AkSoundEngine.PostEvent("Play_Foosteps_Orc_Snow", gameObject);
    }
}
