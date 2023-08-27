using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc_Animations : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent orcNavMeshAgent;

    private int OrcVelocityHash;
    private float OrcIdleAnimationsHash;

    private List<string> idleAnimationStates = new List<string>();
    private float timeSinceLastIdleChange = 0f;

    private void Start()
    {
        OrcIdleAnimationsHash = Animator.StringToHash("OrcIdleAnimations");
        OrcVelocityHash = Animator.StringToHash("OrcVelocity");

        // Add your idle animation state strings here
        idleAnimationStates.Add("Idle1");
        idleAnimationStates.Add("Idle2");
        idleAnimationStates.Add("Idle3");
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
            string newIdleState = GetRandomIdleState();

            // Only update the idle animation if the state has changed
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(newIdleState))
            {
                UpdateMovementAnimationState(idleAnimationStates.IndexOf(newIdleState) * 0.5f);
            }

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
        else if (orcNavMeshAgent.speed >= 0.1f )
        {
            UpdateMovementAnimationState(1f);
        }
    }

    private void UpdateMovementAnimationState(float value)
    {
        animator.SetFloat(OrcVelocityHash, value);
    }

    private string GetRandomIdleState()
    {
        int randomIndex = Random.Range(0, idleAnimationStates.Count);
        return idleAnimationStates[randomIndex];
    }
}
