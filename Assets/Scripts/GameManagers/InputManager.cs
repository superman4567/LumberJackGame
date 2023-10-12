using System;
using UnityEngine;

namespace GameManagers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public event Action OnRightMouseButtonDown;
        public event Action OnLeftMouseButtonDown;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More then one InputManager instance");
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                OnRightMouseButtonDown?.Invoke();
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnLeftMouseButtonDown?.Invoke();
            }
        }
    }
}