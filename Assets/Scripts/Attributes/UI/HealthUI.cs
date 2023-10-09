using Players;
using TMPro;
using UnityEngine;

namespace Attributes.UI
{
    public class HealthUI : MonoBehaviour
    {
        private TextMeshProUGUI _healthText;
        private HealthEvent _healthEvent;

        private void Awake()
        {
            _healthText = transform.Find("HealthAmount").GetComponent<TextMeshProUGUI>();
            _healthEvent = FindObjectOfType<Player>().HealthEvent;
        }

        private void Start()
        {
            _healthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
        }
        
        /// <summary>
        /// Callback function which gets called when the OnHealthChanged event gets fired.
        /// </summary>
        /// <param name="healthEvent"></param>
        /// <param name="healthEventArgs"></param>
        private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
        {
            SetHealthValue(healthEventArgs.healthAmount);
        }
        
        /// <summary>
        /// Sets the healthUI value to the input amount
        /// </summary>
        /// <param name="healthAmount">The current health</param>
        private void SetHealthValue(int healthAmount)
        {
            _healthText.SetText(healthAmount.ToString());
        }
    }
}