using Attributes;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        // @TODO: Change to work with weapon Upgrades. Only for testing
        [SerializeField] private float knockBackMultiplier = 1.0f;
        public Health Health { get; private set; }
        public HealthEvent HealthEvent { get; private set; }

        private void Awake()
        {
            Health = GetComponent<Health>();
            HealthEvent = GetComponent<HealthEvent>();
        }
        
        // @Todo implement ScriptableObject
        private void SetPlayerHealth()
        {
            Health.SetStartingHealth(100);
        }

        public Vector3 GetPlayerPosition() => transform.position;

        public float GetKnockBackMultiplier() => knockBackMultiplier;
    }
}