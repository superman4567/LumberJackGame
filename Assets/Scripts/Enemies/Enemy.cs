using Attributes;
using Hits;
using KnockBacks;
using Movement;
using UI.FloatingText;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(HealthEvent))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(MovementToPosition))]
    [RequireComponent(typeof(MovementToPositionEvent))]
    [RequireComponent(typeof(KnockBack))]
    [RequireComponent(typeof(KnockBackEvent))]
    [RequireComponent(typeof(Hit))]
    [RequireComponent(typeof(HitEvent))]
    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Material hitMaterial;
        [SerializeField] private float hitDuration = 0.1f;
        public Animator Animator { get; private set; }
        public Health Health { get; private set; }
        public HealthEvent HealthEvent { get; private set; }
        public EnemyMovement EnemyMovement { get; private set; }
        public MovementToPosition MovementToPosition { get; private set; }
        public MovementToPositionEvent MovementToPositionEvent { get; private set; }
        public KnockBack KnockBack { get; private set; }
        public KnockBackEvent KnockBackEvent { get; private set; }
        public Hit Hit { get; private set; }
        public HitEvent HitEvent { get; private set; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Health = GetComponent<Health>();
            HealthEvent = GetComponent<HealthEvent>();
            EnemyMovement = GetComponent<EnemyMovement>();
            MovementToPosition = GetComponent<MovementToPosition>();
            MovementToPositionEvent = GetComponent<MovementToPositionEvent>();
            KnockBack = GetComponent<KnockBack>();
            KnockBackEvent = GetComponent<KnockBackEvent>();
            Hit = GetComponent<Hit>();
            HitEvent = GetComponent<HitEvent>();
        }

        private void OnEnable()
        {
            HealthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
        }

        private void OnDisable()
        {
            HealthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
        }

        private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
        {
            if (healthEventArgs.healthAmount <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                HitEvent.CallHitEvent(hitMaterial, hitDuration);
                KnockBackEvent.CallKnockBackEvent((GameManager.Instance.GetPlayer().GetPlayerPosition() - transform.position).normalized, GameManager.Instance.GetPlayer().GetKnockBackMultiplier());
            }
            FloatingTextSpawner.SpawnFloatingDamageText(transform, healthEventArgs.damageAmount);
        }
    }
}