using System;
using UnityEngine;

namespace Hits
{
    public class HitEvent : MonoBehaviour
    {
        public event Action<HitEvent, HitEventArgs> OnHit;

        public void CallHitEvent(Material material, float duration)
        {
            OnHit?.Invoke(this, new HitEventArgs { material = material, duration = duration});
        }
    }

    public class HitEventArgs : EventArgs
    {
        public Material material;
        public float duration;
    }
}