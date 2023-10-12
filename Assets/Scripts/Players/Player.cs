using Attributes;
using GameResources;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(PlayerCombat))]
    public class Player : MonoBehaviour
    {
        public Health Health { get; private set; }

        private void Awake()
        {
            Health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            Health.OnHealthChanged += Health_OnHealthChanged;
        }

        private void OnDisable()
        {
            Health.OnHealthChanged -= Health_OnHealthChanged;
        }

        private void Health_OnHealthChanged(float amount)
        {
            if (Health.IsDead())
            {
                RoundManager.Instance.ChangeDiffCompleteText(false);
                SteamAchievementManager.instance.UnlockAchievement(SteamAchievementsDb.DEATH_ACHIEVEMENT);
                Invoke(nameof(FreezeTime), 7.0f);
            }
        }
        
        public void FreezeTime()
        {
            Time.timeScale = 0.05f;
        }

        public Vector3 GetPosition() => transform.position;
    }
}