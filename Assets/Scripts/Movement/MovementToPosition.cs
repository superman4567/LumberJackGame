using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(MovementToPositionEvent))]
    [DisallowMultipleComponent]
    public class MovementToPosition : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private MovementToPositionEvent _movementToPositionEvent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        }

        private void OnEnable()
        {
            _movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_MovementToPosition;
        }

        private void OnDisable()
        {
            _movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_MovementToPosition;
        }

        private void MovementToPositionEvent_MovementToPosition(MovementToPositionEvent movementToPositionEvent, MovementToPositionArgs movementToPositionArgs)
        {
            if (!_navMeshAgent.isActiveAndEnabled)
            {
                return;
            }
            _navMeshAgent.speed = movementToPositionArgs.moveSpeed;
            _navMeshAgent.SetDestination(movementToPositionArgs.movePosition);
        }
    }
}