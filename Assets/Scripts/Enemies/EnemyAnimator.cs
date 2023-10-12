using Attributes;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyCombat))]
    [DisallowMultipleComponent]
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private string animationVelocityParameterName = "OrcVelocity";
        [SerializeField] private string animationAttackingParameterName = "OrcMeleeAttack";
        private int _animationVelocityHash;
        private int _animationAttackHash;
        private EnemyMovement _enemyMovement;
        private EnemyCombat _enemyCombat;
        private Health _health;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyCombat = GetComponent<EnemyCombat>();
            _animationVelocityHash = Animator.StringToHash(animationVelocityParameterName);
            _animationAttackHash = Animator.StringToHash(animationAttackingParameterName);
        }

        private void OnEnable()
        {
            _enemyCombat.OnAttackStarted += EnemyCombat_OnAttackStarted;
            _enemyMovement.OnMovementSpeedChanged += EnemyMovement_OnMovementSpeedChanged;
            _enemyMovement.OnKnockBackBegin += () =>
            {
                _animator.ResetTrigger(_animationAttackHash);
                _animator.enabled = false;
            };
            _enemyMovement.OnKnockBackEnd += () =>
            {
                _animator.enabled = true;
            };
        }

        private void OnDisable()
        {
            _enemyCombat.OnAttackStarted -= EnemyCombat_OnAttackStarted;
            _enemyMovement.OnMovementSpeedChanged -= EnemyMovement_OnMovementSpeedChanged;
        }

        private void EnemyCombat_OnAttackStarted()
        {
            if (!_animator.isActiveAndEnabled)
            {
                return;
            }
            SetAttackAnimatorParameters();
        }
        
        private void EnemyMovement_OnMovementSpeedChanged(float movementSpeed)
        {
            if (!_animator.isActiveAndEnabled)
            {
                return;
            }
            SetMovementAnimatorParameters(movementSpeed);
        }
        
        private void SetMovementAnimatorParameters(float magnitude)
        {
            if (!_animator.isActiveAndEnabled)
            {
                return;
            }
            _animator.SetFloat(_animationVelocityHash, magnitude);
        }

        private void SetAttackAnimatorParameters()
        {
            _animator.ResetTrigger(_animationAttackHash);
            _animator.SetTrigger(_animationAttackHash);
        }
        
    }
}