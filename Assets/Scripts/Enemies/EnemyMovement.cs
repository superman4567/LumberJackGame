using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    [DisallowMultipleComponent]
    public class EnemyMovement : MonoBehaviour
    {
        public event Action OnPlayerAttackDistanceReached;
        public event Action OnPlayerAttackDistanceLeft;
        public event Action<float> OnMovementSpeedChanged; 
        
        private const int TARGET_FRAME_RATE_TO_SPREAD_PATHFINDING = 60;
        private Vector3 _playerReferencePosition;
        private NavMeshAgent _agent;
        [SerializeField] private float movementSpeed = 5.0f;
        [SerializeField] private float attackingDistance = 5.0f;
        
        private bool _chasePlayer = true;
        private int _updateFrameNumber;
        private bool _isMovementBlocked;
        private bool _isMoving;

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

        public float GetVelocity() => _agent.velocity.magnitude;
        
        public void OrcFootStepSound()
        {
            AkSoundEngine.PostEvent("Play_Foosteps_Orc_Snow", gameObject);
        }
    }
}