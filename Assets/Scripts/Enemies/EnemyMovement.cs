using System;
using Attributes;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(Health))]
    [DisallowMultipleComponent]
    public class EnemyMovement : MonoBehaviour
    {
        public event Action OnPlayerAttackDistanceReached;
        public event Action OnPlayerAttackDistanceLeft;
        public event Action<float> OnMovementSpeedChanged;
        public event Action OnKnockBackBegin;
        public event Action OnKnockBackEnd;
        
        private const int TARGET_FRAME_RATE_TO_SPREAD_PATHFINDING = 60;
        private Vector3 _playerReferencePosition;
        private NavMeshAgent _agent;
        [SerializeField] private float movementSpeed = 5.0f;
        [SerializeField] private float attackingDistance = 5.0f;
        [SerializeField] private float knockBackForce = 1.0f;
        [SerializeField] private float knockBackDuration = 0.1f;
        [SerializeField] private float knockBackCooldown = 0.5f;
        
        private bool _chasePlayer = true;
        private int _updateFrameNumber;
        private bool _isMovementBlocked;
        private bool _isMoving;
        
        private bool _isKnockBackActive;
        private Vector3 _knockBackDirection;
        private float _currentKnockBackCooldown = float.MaxValue;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        

        private void Start()
        {
            _playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();
            _agent.speed = movementSpeed;
        }

        private void Update()
        {
            HandleKnockBack();
            HandleMovement();
        }
        
        private void HandleKnockBack()
        {
            _currentKnockBackCooldown += Time.deltaTime;
            if (_isKnockBackActive)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + _knockBackDirection,
                    knockBackForce * Time.deltaTime);
            }
        }

        private void HandleMovement()
        {
            if (!_agent.isActiveAndEnabled)
            {
                return;
            }

            OnMovementSpeedChanged?.Invoke(_agent.velocity.magnitude);
            if (_isMovementBlocked || Time.frameCount % TARGET_FRAME_RATE_TO_SPREAD_PATHFINDING != _updateFrameNumber)
            {
                return;
            }

            _playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();

            if (Vector3.Distance(transform.position, _playerReferencePosition) > attackingDistance)
            {
                if (!_chasePlayer)
                {
                    OnPlayerAttackDistanceLeft?.Invoke();
                    _chasePlayer = true;
                }

                var targetPoint = Random.insideUnitCircle * 3.0f;
                _agent.SetDestination(_playerReferencePosition + new Vector3(targetPoint.x, targetPoint.y, 0.0f));
            }
            else
            {
                if (_chasePlayer)
                {
                    OnPlayerAttackDistanceReached?.Invoke();
                    _chasePlayer = false;
                }

                _agent.SetDestination(_playerReferencePosition);
            }
        }

        /// <summary>
        /// Set the frame number that the enemy path will be recalculated on - to avoid performance spikes
        /// </summary>
        public void SetUpdateFrameNumber(int updateFrameNumber)
        {
            _updateFrameNumber = updateFrameNumber;
        }

        public void KnockBack(Vector3 direction, float externalMultiplier)
        {
            //StartCoroutine(HitEffect(skinnedMeshRenderer, hitMaterial, 0.1f));
            _isKnockBackActive = true;
            OnKnockBackBegin?.Invoke();
            _agent.enabled = false;
            _knockBackDirection = direction.normalized * knockBackForce * externalMultiplier;
            Debug.Log(knockBackForce);
            Invoke(nameof(EndKnockBack), knockBackDuration);
        }

        private void EndKnockBack()
        {
            _agent.enabled = true;
            _currentKnockBackCooldown = 0.0f;
            OnKnockBackEnd?.Invoke();
            _isKnockBackActive = false;
        }

        public bool CanBeKnockBacked() => _currentKnockBackCooldown >= knockBackCooldown;
        
        public void OrcFootStepSound()
        {
            AkSoundEngine.PostEvent("Play_Foosteps_Orc_Snow", gameObject);
        }
    }
}