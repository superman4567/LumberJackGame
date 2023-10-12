using Enemies;
using UnityEngine;

namespace Attacks
{
    [RequireComponent(typeof(AttackEvent))]
    [DisallowMultipleComponent]
    public class Attack : MonoBehaviour
    {
        private AttackEvent _attackEvent;

        private void Awake()
        {
            _attackEvent = GetComponent<AttackEvent>();
        }

        private void OnEnable()
        {
            _attackEvent.OnAttack += AttackEvent_OnAttack;
            _attackEvent.OnAttackEnd -= AttackEvent_OnAttackEnd;
        }
        
        private void OnDisable()
        {
            _attackEvent.OnAttack -= AttackEvent_OnAttack;
            _attackEvent.OnAttackEnd -= AttackEvent_OnAttackEnd;
        }
        private void AttackEvent_OnAttack(AttackEvent attackEvent, AttackEventArgs attackEventArgs)
        {
            StartAttack(attackEventArgs.enemyWeapon, attackEventArgs.damage);   
        }
        
        private void AttackEvent_OnAttackEnd(AttackEvent obj, AttackEventArgs attackEventArgs)
        {
            EndAttack(attackEventArgs.enemyWeapon);
        }


        private void StartAttack(EnemyWeapon weapon, float damage)
        {
            weapon.CanDealDamage = true;
        }

        private void EndAttack(EnemyWeapon weapon)
        {
            weapon.CanDealDamage = false;
        }
    }
}