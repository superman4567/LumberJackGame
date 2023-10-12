using Helpers;
using UnityEngine;

namespace Enemies
{
    [DisallowMultipleComponent]
    public class EnemyWeapon : MonoBehaviour
    {
        private const float DIFFICULTY_0_DAMAGE_MULTIPLIER = 1.0f;
        private const float DIFFICULTY_1_DAMAGE_MULTIPLIER = 1.5f;
        private const float DIFFICULTY_2_DAMAGE_MULTIPLIER = 2.0f;
        [field: SerializeField] public float AttackCooldown { get; private set; } = 3.0f;
        [field: SerializeField] public float Damage { get; private set; } = 5.0f;
        public bool CanDealDamage { get; set; }
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }

        public void Initialize(EnemyCombat enemyCombat, EnemyWeaponSO weaponSO)
        {
            Damage = weaponSO.damage;
            AttackCooldown = weaponSO.cooldown;
            enemyCombat.OnAttackStarted += () =>
            {
                _collider.enabled = true;
                CanDealDamage = true;
            };
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CanDealDamage && StaticHelpers.IsPlayerLayer(other))
            {
                Debug.Log($"Damaged: {Damage}");
                _collider.enabled = false;
                CanDealDamage = false;
            }
            else if (StaticHelpers.IsTotemTag(other) && other.TryGetComponent(out HealingTotem healingTotem))
            {
                healingTotem.TakeDamage(Damage);
                _collider.enabled = false;
                CanDealDamage = false;
            }
        }
        
        private void CalculateDamage()
        {
            Damage = GameManager.Instance.GetDifficulty() switch
            {
                0 => Damage * DIFFICULTY_0_DAMAGE_MULTIPLIER,
                1 => Damage * DIFFICULTY_2_DAMAGE_MULTIPLIER,
                2 => Damage * DIFFICULTY_1_DAMAGE_MULTIPLIER,
                _ => Damage * DIFFICULTY_0_DAMAGE_MULTIPLIER
            };
        }
    }
}