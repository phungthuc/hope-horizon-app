using System;
using FarmerEscape.Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private LevelLoader levelLoader;

        public UnityEvent LevelCompleted;
        public UnityEvent LevelFailed;

        public void ResetAll()
        {
            PlayerData.Instance.ResetAll();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.L))
            {
                LoadLevel();
            }

            if (Input.GetKey(KeyCode.R))
            {
                ReloadLevel();
            }
        }

        public void OnLoadLevelCompleted()
        {
            levelLoader.CurrentLevelController.Won += OnLevelWon;
            levelLoader.CurrentLevelController.Lose += OnLevelLose;
        }

        public void LoadLevel()
        {
            var currentLevel = PlayerData.Instance.CurrentLevel;
            levelLoader.LoadLevel(currentLevel);
        }

        public void OnLevelWon()
        {
            LevelCompleted?.Invoke();
        }

        public void OnLevelLose()
        {
            LevelFailed?.Invoke();
        }

        public void NextLevel()
        {
            PlayerData.Instance.CurrentLevel++;
            if(PlayerData.Instance.CurrentLevel > PlayerData.Instance.HighestLevel)
            {
                PlayerData.Instance.HighestLevel = PlayerData.Instance.CurrentLevel;
            }
            levelLoader.LoadLevel(PlayerData.Instance.CurrentLevel);
        }

        public void ReloadLevel()
        {
            levelLoader.ReloadCurrentLevel();
        }

        public void OnInventoryItemEquipped()
        {
           levelLoader.CurrentLevelController.OnInventoryItemEquipped();
        }
    }
}
