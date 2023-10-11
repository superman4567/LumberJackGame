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
        }

        private void OnDisable()
        {
            _enemyCombat.OnAttackStarted -= EnemyCombat_OnAttackStarted;
            _enemyMovement.OnMovementSpeedChanged -= EnemyMovement_OnMovementSpeedChanged;
        }
        
        private void EnemyCombat_OnAttackStarted()
        {
            SetAttackAnimatorParameters();
        }
        
        private void EnemyMovement_OnMovementSpeedChanged(float movementSpeed)
        {
            SetMovementAnimatorParameters(movementSpeed);
        }
        
        private void SetMovementAnimatorParameters(float magnitude)
        {
            _animator.SetFloat(_animationVelocityHash, magnitude);
        }

        private void SetAttackAnimatorParameters()
        {
            _animator.ResetTrigger(_animationAttackHash);
            _animator.SetTrigger(_animationAttackHash);
        }
        
    }
}