using System;
using System.Collections;
using Attributes;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Health))]
    [DisallowMultipleComponent]
    public class EnemyCombat : MonoBehaviour
    {
        [Serializable]
        public struct Weapon
        {
            public EnemyWeapon EnemyWeapon;
            public EnemyWeaponSO EnemyWeaponSO;
        }
        
        public event Action OnAttackStarted;
        
        private float _attackCooldown;
        [SerializeField] private OrcTextHealth floatingTextPrefab;
        [SerializeField] private Material _hitMaterial;
        [SerializeField] private float hitTime = 0.1f;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private Weapon[] enemyWeapons;
        
        private EnemyMovement _enemyMovement;
        private Health _health;
        private float _currentCooldown = 0.0f;
        
        private bool _chasingPlayer;
        private bool _attackingPlayer;

        private Material _originalMaterial;

        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _health = GetComponent<Health>();
            _originalMaterial = skinnedMeshRenderer.material;
        }

        private void OnEnable()
        {
            _enemyMovement.OnPlayerAttackDistanceReached += EnemyMovement_OnPlayerMeleeDistanceReached;
            _enemyMovement.OnPlayerAttackDistanceLeft += EnemyMovement_OnPlayerAttackDistanceLeft;
            _enemyMovement.OnKnockBackBegin += EnemyMovement_OnKnockBackBegin;
            _health.OnHealthChanged += Health_OnHealthChanged;
        }

        private void OnDisable()
        {
            _enemyMovement.OnPlayerAttackDistanceReached -= EnemyMovement_OnPlayerMeleeDistanceReached;
            _enemyMovement.OnPlayerAttackDistanceLeft -= EnemyMovement_OnPlayerAttackDistanceLeft;
            _enemyMovement.OnKnockBackBegin += EnemyMovement_OnKnockBackBegin;
            _health.OnHealthChanged -= Health_OnHealthChanged;
        }

        private void EnemyMovement_OnPlayerMeleeDistanceReached()
        {
            _attackingPlayer = true;
        }
        
        private void EnemyMovement_OnPlayerAttackDistanceLeft()
        {
            _attackingPlayer = false;
        }
        
        private void Health_OnHealthChanged(int damageAmount)
        {
            ShowFloatingText(damageAmount);
        }
        
        private void EnemyMovement_OnKnockBackBegin()
        {
            StartCoroutine(nameof(HitRoutine));
        }

        private void Start()
        {
            var highest = 0.0f;
            foreach (var weapon in enemyWeapons)
            {
                weapon.EnemyWeapon.Initialize(this, weapon.EnemyWeaponSO);
                if (weapon.EnemyWeaponSO.cooldown > highest)
                {
                    highest = weapon.EnemyWeaponSO.cooldown;
                }
            }
            _attackCooldown = highest;
        }

        private void Update()
        {
            _currentCooldown += Time.deltaTime;
            if (_attackingPlayer && _currentCooldown >= _attackCooldown)
            {
                OnAttackStarted?.Invoke();
                _currentCooldown = 0.0f;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Axe") && other.isTrigger)
            {
                _health.TakeDamage(20);
                // Add Orc Getting Hit Sound
                AkSoundEngine.PostEvent("Play_Orc_Getting_Hit", gameObject);
            }
        }
        
        private void ShowFloatingText(int damageAmount)
        {
            var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            go.Setup(damageAmount);
        }

        private IEnumerator HitRoutine()
        {
            skinnedMeshRenderer.material = _hitMaterial;
            yield return new WaitForSeconds(hitTime);
            skinnedMeshRenderer.material = _originalMaterial;
        }
        
        public void OrcSlashSound()
        {
            // Snow
            AkSoundEngine.PostEvent("Play_Orc_Slash", gameObject);
        }
    }
}