using System;
using UnityEngine;

namespace KnockBacks
{
    public class KnockBackEvent : MonoBehaviour
    {
        public event Action<KnockBackEvent, KnockBackEventArgs> OnKnockBack;

        public void CallKnockBackEvent(Vector3 direction, float externalMultiplier)
        {
            OnKnockBack?.Invoke(this, new KnockBackEventArgs { direction = direction, externalMultiplier = externalMultiplier });
        }
    }

    public class KnockBackEventArgs : EventArgs
    {
        public Vector3 direction;
        public float externalMultiplier;
    }
}