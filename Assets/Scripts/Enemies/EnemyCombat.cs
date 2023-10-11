using System;
using Attributes;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyWeapon))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Health))]
    //[RequireComponent(typeof(AttackEvent))]
    [DisallowMultipleComponent]
    public class EnemyCombat : MonoBehaviour
    {
        public event Action OnAttackStarted;
        
        private const float DIFFICULTY_0_Damage = 25.0f;
        private const float DIFFICULTY_1_Damage = 34.0f;
        private const float DIFFICULTY_2_Damage = 50.0f;

        [SerializeField] private float attackCooldown = 2.0f;
        [SerializeField] private OrcTextHealth floatingTextPrefab;
        //private EnemyWeapon _enemyWeapon;
        private EnemyMovement _enemyMovement;
        private Health _health;
        //private AttackEvent _attackEvent;
        private float _currentCooldown = 0.0f;
        private float _damage;
        
        
        private bool _chasingPlayer;
        private bool _attackingPlayer;

        private void Awake()
        {
            //_enemyWeapon = GetComponent<EnemyWeapon>();
            _enemyMovement = GetComponent<EnemyMovement>();
            //_attackEvent = GetComponent<AttackEvent>();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _enemyMovement.OnPlayerAttackDistanceReached += EnemyMovement_OnPlayerMeleeDistanceReached;
            _enemyMovement.OnPlayerAttackDistanceLeft += EnemyMovement_OnPlayerAttackDistanceLeft;
            _health.OnHealthChanged += Health_OnHealthChanged;
        }

        private void OnDisable()
        {
            _enemyMovement.OnPlayerAttackDistanceReached -= EnemyMovement_OnPlayerMeleeDistanceReached;
            _enemyMovement.OnPlayerAttackDistanceLeft -= EnemyMovement_OnPlayerAttackDistanceLeft;
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

        private void Start()
        {
            CalculateDamage();
        }

        private void Update()
        {
            _currentCooldown += Time.deltaTime;
            if (_attackingPlayer && _currentCooldown >= attackCooldown)
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

        private void CalculateDamage()
        {
            _damage = GameManager.Instance.GetDifficulty() switch
            {
                0 => DIFFICULTY_0_Damage,
                1 => DIFFICULTY_1_Damage,
                2 => DIFFICULTY_2_Damage,
                _ => DIFFICULTY_0_Damage
            };
        }
        
        private void ShowFloatingText(int damageAmount)
        {
            var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            go.Setup(damageAmount);
        }
        
        public void OrcSlashSound()
        {
            // Snow
            AkSoundEngine.PostEvent("Play_Orc_Slash", gameObject);
        }
    }
}