using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        public event Action Started;
        public event Action Won;
        public event Action Lose;

        public UnityEvent InventoryItemEquipped;

        public void Start()
        {
        }

        public void OnStart()
        {
            Started?.Invoke();
        }

        public void OnWin()
        {
            Won?.Invoke();
        }

        public void OnLose()
        {
            Lose?.Invoke();
        }

        public void OnInventoryItemEquipped()
        {
            InventoryItemEquipped?.Invoke();
        }
    }
}
