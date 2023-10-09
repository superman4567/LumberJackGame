using UnityEngine;

namespace UI.FloatingText
{
    public class DamageText : FloatingText
    {
        [SerializeField] private float destroyTime = 3f;
        [SerializeField] private Vector3 offset = new (0, 2, 0);
        [SerializeField] private Vector3 randomizeIntensity = new (0.5f, 0, 1);

        private void Start()
        {
            transform.localPosition += offset;
            transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
                Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
                Random.Range(-randomizeIntensity.z, randomizeIntensity.z));
            Destroy(gameObject, destroyTime);
        }
    }
}