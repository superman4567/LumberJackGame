﻿using System.Collections;
using UnityEngine;

namespace Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private bool usePostHitImmunity;
        [SerializeField] private float immunityTime = 0.2f;
        private int _startingHealth;
        private int _currentHealth;
        private HealthEvent _healthEvent;
        private Coroutine _postHitCoroutine;
        private bool _immuneToDamage;

        private void Start()
        {
            CallHealthEvent(0);
        }
        
        /// <summary>
        /// Calls the OnHealthChanged event from the Health event class.
        /// </summary>
        /// <param name="damageAmount">The amount of damage that was taken</param>
        private void CallHealthEvent(int damageAmount)
        {
            _healthEvent.CallHealthChangedEvent((float)_currentHealth / _startingHealth, _currentHealth, damageAmount);
        }
        
        /// <summary>
        /// Takes the amount of damage. Clamped to 0.
        /// </summary>
        /// <param name="damageAmount">The amount of damage to take</param>
        public void TakeDamage(int damageAmount)
        {
            if (_immuneToDamage)
            {
                return;
            }
            _currentHealth = Mathf.Max(_currentHealth - damageAmount, 0);
            CallHealthEvent(damageAmount);
            PostHitImmunity();
        }
        
        /// <summary>
        /// If usePostHitImmunity = true this handles the immunity to damage for the specified time in immunityTime
        /// </summary>
        private void PostHitImmunity()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            if (usePostHitImmunity)
            {
                if (_postHitCoroutine != null)
                {
                    StopCoroutine(_postHitCoroutine);
                }

                _postHitCoroutine = StartCoroutine(nameof(PostHitImmunityRoutine));
            }
        }
        
        /// <summary>
        /// Sets the _immuneToDamage to true, so all damage is prevented for the specified time in immunityTime
        /// </summary>
        private IEnumerator PostHitImmunityRoutine()
        {
            _immuneToDamage = true;
            yield return new WaitForSeconds(immunityTime);
            _immuneToDamage = false;
        }
        
        /// <summary>
        /// Set the starting health. Also sets the currentHealth to the startinHealth.
        /// </summary>
        /// <param name="startingHealth">The amount of health a character starts with</param>
        public void SetStartingHealth(int startingHealth)
        {
            _startingHealth = startingHealth;
            _currentHealth = startingHealth;
        }
        
        /// <summary>
        /// Gets the starting health (and also the maximum health).
        /// </summary>
        /// <returns>The starting (maximum) amount a character can have.</returns>
        public int GetStartingHealth() => _startingHealth;
        
        /// <summary>
        /// Adds a percent value based on the starting health. Use Integer values like 20 and not 0,2.
        /// </summary>
        /// <param name="healthPercent">The percent amount added based on startingHealth. e.g. 20 for 20%</param>
        public void AddHealthPercent(int healthPercent)
        {
            var healthIncrease = Mathf.RoundToInt(_startingHealth * healthPercent / 100.0f);
            var totalHealth = _currentHealth + healthIncrease;
            _currentHealth = Mathf.Max(totalHealth, _startingHealth);
            CallHealthEvent(0);
        }
        
        /// <summary>
        /// Adds flat value to the currentHealth. Clamped to the maximum health.
        /// </summary>
        /// <param name="healAmount">An flat value added to the currentHealth.</param>
        public void AddHealth(int healAmount)
        {
            var totalHealth = _currentHealth + healAmount;
            _currentHealth = Mathf.Max(totalHealth, _startingHealth);
            CallHealthEvent(0);
        }
    }
}