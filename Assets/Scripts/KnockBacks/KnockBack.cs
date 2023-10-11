using UnityEngine;
using UnityEngine.AI;

namespace KnockBacks
{
    [RequireComponent(typeof(KnockBackEvent))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class KnockBack : MonoBehaviour
    {
        [SerializeField] private float knockBackDuration = 0.5f;
        [SerializeField] private float knockBackForce = 10.0f;
        private KnockBackEvent _knockBackEvent;
        private NavMeshAgent _navMeshAgent;
        private Coroutine _knockBackRoutine;
        private Vector3 _knockBackDirection;
        private bool _isKnockBackActive;

        private void Awake()
        {
            _knockBackEvent = GetComponent<KnockBackEvent>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            _knockBackEvent.OnKnockBack += KnockBackEvent_OnKnockBack;
        }

        private void OnDisable()
        {
            _knockBackEvent.OnKnockBack -= KnockBackEvent_OnKnockBack;
        }
        
        private void Update()
        {
            if (!_isKnockBackActive)
            {
                return;
            }

            transform.position += Vector3.Lerp(transform.position, transform.position + _knockBackDirection, knockBackForce * Time.deltaTime);
        }

        private void KnockBackEvent_OnKnockBack(KnockBackEvent knockBack, KnockBackEventArgs knockBackEventArgs)
        {
            StartKnockBack(knockBackEventArgs.direction, knockBackEventArgs.externalMultiplier);
        }
        
        /// <summary>
        /// Disables the NavMeshAgent. Also sets parameters for the knock back. Returns early if its already getting knocked back.
        /// </summary>
        /// <param name="direction">The direction in which we get knocked</param>
        /// <param name="externalMultiplier">External force which is added to the internal knock back force</param>
        private void StartKnockBack(Vector3 direction, float externalMultiplier)
        {
            if (_isKnockBackActive)
            {
                return;
            }
            _knockBackDirection = direction.normalized * (knockBackForce * externalMultiplier);
            _isKnockBackActive = true;
            _navMeshAgent.enabled = false;
            if (_knockBackRoutine != null)
            {
                StopCoroutine(_knockBackRoutine);
            }
            Invoke(nameof(EndKnockBack), knockBackDuration);
        }
        
        
        /// <summary>
        /// Sets the NavmeshAgent back to be active and makes the enemy knockable again
        /// </summary>
        private void EndKnockBack()
        {
            _navMeshAgent.enabled = true;
            _isKnockBackActive = false;
        }

        public bool IsKnockBackActive() => _isKnockBackActive;
    }
}