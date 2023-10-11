using System.Collections;
using UnityEngine;

namespace Hits
{
    [RequireComponent(typeof(HitEvent))]
    public class Hit : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Material _originalMaterial;
        private HitEvent _hitEvent;

        private void Awake()
        {
            _originalMaterial = _skinnedMeshRenderer.material;
            _hitEvent = GetComponent<HitEvent>();
        }

        private void OnEnable()
        {
            _hitEvent.OnHit += HitEvent_OnHit;
        }

        private void OnDisable()
        {
            _hitEvent.OnHit -= HitEvent_OnHit;
        }

        private void HitEvent_OnHit(HitEvent hitEvent, HitEventArgs hitEventArgs)
        {
            StartCoroutine(HitEffectRoutine(_skinnedMeshRenderer, hitEventArgs.material, hitEventArgs.duration));
        }
        
        /// <summary>
        /// Changes the appearance for the enemy to represent that he is hit.
        /// </summary>
        /// <returns></returns>
        private IEnumerator HitEffectRoutine(SkinnedMeshRenderer meshRenderer, Material mat, float duration)
        {
            _skinnedMeshRenderer.material = mat;
            yield return new WaitForSeconds(duration);
            meshRenderer.material = _originalMaterial;
        }
    }
}