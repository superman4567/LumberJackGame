using KnockBacks;
using Movement;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    [DisallowMultipleComponent]
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private string animationVelocityParameterName = "OrcVelocity";
        private int _animationVelocityHash;
        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _animationVelocityHash = Animator.StringToHash(animationVelocityParameterName);
        }

        private void OnEnable()
        {
            _enemy.MovementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;
            _enemy.KnockBackEvent.OnKnockBack += KnockBackEvent_OnKnockBack;
        }

        private void OnDisable()
        {
            _enemy.MovementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;
            _enemy.KnockBackEvent.OnKnockBack -= KnockBackEvent_OnKnockBack;
        }

        private void MovementToPositionEvent_OnMovementToPosition(MovementToPositionEvent movementToPositionEvent, MovementToPositionArgs movementToPositionArgs)
        {
            SetMovementAnimatorParameters();
        }
        
        private void KnockBackEvent_OnKnockBack(KnockBackEvent arg1, KnockBackEventArgs arg2)
        {
            SetKnockBackAnimatorParameters();
        }

        private void SetMovementAnimatorParameters()
        {
            _enemy.Animator.SetFloat(_animationVelocityHash, 1.0f);
        }

        private void SetKnockBackAnimatorParameters()
        {
            _enemy.Animator.SetFloat(_animationVelocityHash, 0.0f);
        }
    }
}