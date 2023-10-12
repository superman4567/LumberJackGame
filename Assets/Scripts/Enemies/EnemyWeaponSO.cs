using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "new Enemy Weapon", menuName = "Enemies/new Enemy Weapon")]
    public class EnemyWeaponSO : ScriptableObject
    {
        public float damage;
        public float cooldown;
    }
}