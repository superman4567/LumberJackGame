using UnityEngine;

namespace UI.FloatingText
{
    public class FloatingTextSpawner : MonoBehaviour
    {
        [SerializeField] private FloatingText floatingTextPrefab;
        [SerializeField] private DamageText floatingDamageTextPrefab;

        private static FloatingTextSpawner _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError("More then one FloatingTextSpawner GameObjects");
                Destroy(gameObject);
            }
        }

        public static void SpawnFloatingDamageText(Transform transformLocation, float damageAmount)
        {
            var floatingText = Instantiate(_instance.floatingDamageTextPrefab, transformLocation.position, Quaternion.identity, transformLocation);
            floatingText.SetText(damageAmount.ToString());
        }
    }
}