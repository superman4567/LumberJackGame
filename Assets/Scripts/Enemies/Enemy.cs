using Attributes;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Health))]
    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        public EnemyMovement EnemyMovement { get; private set; }
        public Health Health { get; private set; }
        private void Awake()
        {
            EnemyMovement = GetComponent<EnemyMovement>();
            Health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            Health.OnHealthChanged += Health_OnHealthChanged;
        }

        private void OnDisable()
        {
            Health.OnHealthChanged -= Health_OnHealthChanged;
        }

        private void Health_OnHealthChanged(float damage)
        {
            if (Health.IsDead())
            {
                Destroy(gameObject);
            }
        }
    }
}