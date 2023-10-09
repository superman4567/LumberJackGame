using UnityEngine;

namespace Enemies
{
    [DisallowMultipleComponent]
    public class EnemyMovement : MonoBehaviour
    {
        private const int TARGET_FRAME_RATE_TO_SPREAD_PATHFINDING = 60;
        
        private Enemy _enemy;
        private Vector3 _playerReferencePosition;
        private float _movementSpeed;
        private bool _chasePlayer;
        private float _chasingDistance;

        private int _updateFrameNumber;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void Start()
        {
            _playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();
        }

        private void Update()
        {
            if (!_chasePlayer &&
                Vector3.Distance(transform.position, GameManager.Instance.GetPlayer().GetPlayerPosition()) <
                _chasingDistance)
            {
                _chasePlayer = true;
            }

            if (!_chasePlayer)
            {
                return;
            }

            if (Time.frameCount % TARGET_FRAME_RATE_TO_SPREAD_PATHFINDING != _updateFrameNumber)
            {
                return;
            }

            _playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();
            _enemy.MovementToPositionEvent.CallMovementToPositionEvent(_playerReferencePosition, transform.position, _movementSpeed, (_playerReferencePosition - transform.position).normalized);
        }
        
        /// <summary>
        /// Set the frame number that the enemy path will be recalculated on - to avoid performance spikes
        /// </summary>
        public void SetUpdateFrameNumber(int updateFrameNumber)
        {
            _updateFrameNumber = updateFrameNumber;
        }
    }
}