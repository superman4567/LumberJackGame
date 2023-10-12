using System;
using System.Collections;
using Attributes;
using GameManagers;
using Interfaces;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Health))]
    [DisallowMultipleComponent]
    public class PlayerCombat : MonoBehaviour, ICombatComponent
    {
        [SerializeField] private GameObject axeModel;
        [SerializeField] private GameObject throwPoint;
        [SerializeField] private AxeDetection axe;
        [SerializeField] private Rigidbody axeRb;
        [SerializeField] private Transform throwSpawnPoint;
        [SerializeField] private string retrieveAxeSFX = "Play_Retrieve_Axe_SFX";
        [SerializeField] private float returnSpeed = 5.0f;
        [SerializeField] private float spinForce = 1500.0f;
        [SerializeField] private float throwForce = 50.0f;
        [SerializeField] private float throwForceMultiplier = 1.0f;
        public event Action OnAxeThrown;
        public event Action OnAxeRecall;
        
        private Health _health;
        
        private bool _isAxeThrown;
        private bool _isAxeReturning;
        private float _timeSinceAxeThrown;

        public bool IsAxeThrown() => _isAxeThrown;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void Start()
        {
            axeRb.useGravity = false;
            axeRb.isKinematic = true;
        }

        private void OnEnable()
        {
            InputManager.Instance.OnLeftMouseButtonDown += InputManager_OnRightMouseButtonDown;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnLeftMouseButtonDown -= InputManager_OnRightMouseButtonDown;
        }

        private void InputManager_OnRightMouseButtonDown()
        {
            ThrowAndReceiveAxe();
        }

        private void Update()
        {
            _timeSinceAxeThrown += Time.deltaTime;
            if (_isAxeReturning)
            {
                ReturnAxe();
            }
        }

        private void ThrowAndReceiveAxe()
        {
            if (!_isAxeThrown)
            {
                OnAxeThrown?.Invoke();
                _isAxeThrown = true;
                _isAxeReturning = false;
                _timeSinceAxeThrown = 0.0f;
                AkSoundEngine.PostEvent("Play_Throw_Axe_1_SFX", gameObject);
            }
            if (axe.axeHitSomething || _timeSinceAxeThrown >= 0.4f)
            {
                OnAxeRecall?.Invoke();
            }
        }
        
        /// <summary>
        /// Animation Event
        /// </summary>
        public void ThrowAxe()
        {
            axeModel.transform.parent = null;
            _isAxeThrown = true;
            axeRb.isKinematic = false;
            axeRb.useGravity = true;
            axeRb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            var spinAxis = transform.up;
            axeRb.angularVelocity = spinAxis * spinForce;

            var throwDirection = (throwPoint.transform.position - throwSpawnPoint.position).normalized;
            axeRb.velocity = throwDirection * (throwForce * throwForceMultiplier);
        }

        private void ReturnAxe()
        {
            _isAxeReturning = true;
            if (_isAxeThrown)
            {
                _isAxeReturning = true;
                axeRb.useGravity = true;
                axeRb.velocity = Vector3.zero;
                axeRb.angularVelocity = Vector3.zero;
            }
            StartCoroutine(nameof(AxeReturnLerpRoutine));
        }

        private IEnumerator AxeReturnLerpRoutine()
        {
            while (_isAxeReturning)
            {
                Vector3 targetPosition = Vector3.Lerp(axeModel.transform.position, throwSpawnPoint.position, returnSpeed * Time.deltaTime);
                axeModel.transform.position = targetPosition;
                yield return new WaitForEndOfFrame();
                
                if (Vector3.Distance(axeModel.transform.position, throwSpawnPoint.position) < 2.0f)
                {
                    axeRb.isKinematic = true;
                    axeModel.transform.parent = throwSpawnPoint;
                    axeModel.transform.localPosition = Vector3.zero;
                    axeModel.transform.localRotation = Quaternion.identity;

                    _isAxeThrown = false;
                    _isAxeReturning = false;
                    axe.axeHitSomething = false;
                    AkSoundEngine.PostEvent(retrieveAxeSFX, gameObject);
                    break;
                }
            }
        }

        public void TakeDamage(float damageAmount)
        {
            _health.TakeDamage(damageAmount);
            PlayHitSound();
        }

        public void PlayHitSound()
        {
        }
    }
}