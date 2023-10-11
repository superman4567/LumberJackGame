using Helpers;
using UnityEngine;

namespace Enemies
{
    [DisallowMultipleComponent]
    public class EnemyWeapon : MonoBehaviour
    {
        [field: SerializeField] public float AttackRange { get; private set; } = 10.0f;
        [field: SerializeField] public float AttackCooldown { get; private set; } = 3.0f;
        public bool CanDealDamage { get; set; }
        public float Damage { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (CanDealDamage && StaticHelpers.IsPlayerLayer(other))
            {
                // Player Take Damage
            }
            else if (StaticHelpers.IsTotemTag(other) && other.TryGetComponent(out HealingTotem healingTotem))
            {
                healingTotem.TakeDamage(Damage);
            }
        }
    }
}