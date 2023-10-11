using System;
using Enemies;
using UnityEngine;

namespace Attacks
{
    public class AttackEvent : MonoBehaviour
    {
        public event Action<AttackEvent, AttackEventArgs> OnAttack;
        public event Action<AttackEvent, AttackEventArgs> OnAttackEnd;

        public void CallAttackEvent(EnemyWeapon weapon, float damage)
        {
            OnAttack?.Invoke(this, new AttackEventArgs { enemyWeapon = weapon, damage = damage });
        }

        public void CallAttackEndEvent(EnemyWeapon weapon)
        {
            OnAttackEnd?.Invoke(this, new AttackEventArgs { enemyWeapon = weapon });
        }
    }

    public class AttackEventArgs : EventArgs
    {
        public EnemyWeapon enemyWeapon;
        public float damage;
    }
}