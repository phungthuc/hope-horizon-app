using System;
using UnityEngine;
using UnityEngine.Events;

namespace LaserPathPuzzle.Scripts.Components.Screens
{
    public class ScreenController : MonoBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private ScreenManager screenManager;

        public UnityEvent Activated;
        public UnityEvent Deactivated;

        private bool isActive;
        public bool IsActive
        {
            get => isActive;
        }

        private void Awake()
        {
            isActive = gameObject.activeSelf;
        }

        public string Key => key;
        public void Active()
        {
            isActive = true;
            gameObject.SetActive(true);
            Activated?.Invoke();
        }

        public void Deactive()
        {
            if (isActive)
            {
                isActive = false;
                gameObject.SetActive(false);
                Deactivated?.Invoke();
            }
        }
    }
}
