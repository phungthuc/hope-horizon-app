using System.Linq;
using Game.Core;
using Game.Scripts.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Scripts.Level
{
    public class LevelLoader : MonoBehaviour
    {
        public LevelController CurrentLevelController { get; private set; }

        private readonly string _levelPrefix = "prefab_level_";
        private int _levelIndex;
        private string _currentLevelKey;
        private const int RandomSeed = 1032847;

        public UnityEvent LoadLevelCompleted;

        public void LoadLevel(int levelIndex)
        {
            _levelIndex = levelIndex;

            var levelKey = _levelPrefix + _levelIndex;

            if (!AddressableUtility.IsAddressableKeyExists(levelKey))
            {
                LoadRandomLevel();
                return;
            }

            LoadLevel(levelKey);
        }

        public void LoadLevel(string levelKey)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(levelKey);
            _currentLevelKey = levelKey;
            handle.Completed += OnLoadLevelPrefabCompleted;
        }

        public void ReloadCurrentLevel()
        {
            LoadLevel(_currentLevelKey);
        }

        private void Awake()
        {

        }

        private void LoadRandomLevel()
        {
            var randomRange = Enumerable.Range(1, GameConstant.TOTAL_UNIQUE_LEVEL).ToList();
            var hashValue = hash(_levelIndex * RandomSeed);
            var levelIndex = randomRange[hashValue % randomRange.Count];

            var preHashValue = hash((_levelIndex - 1) * RandomSeed);
            var preLevelIndex = randomRange[preHashValue % randomRange.Count];

            if (levelIndex == preLevelIndex)
            {
                levelIndex = randomRange[(hashValue + 1) % randomRange.Count];

                var nextHashValue = hash((_levelIndex + 1) * RandomSeed);
                var nextLevelIndex = randomRange[nextHashValue % randomRange.Count];

                if (levelIndex == nextLevelIndex)
                {
                    levelIndex = randomRange[(hashValue - 1) % randomRange.Count];
                }
            }
            var levelKey = _levelPrefix + levelIndex;
            _levelIndex = levelIndex;
            LoadLevel(levelKey);
        }

        private void OnLoadLevelPrefabCompleted(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Result == null)
            {
                LoadRandomLevel();
                return;
            }

            if (CurrentLevelController)
            {
                DestroyCurrentLevel();
            }

            var levelPrefab = handle.Result;
            var level = Instantiate(levelPrefab, transform);
            CurrentLevelController = level.GetComponent<LevelController>();
            LoadLevelCompleted?.Invoke();
        }

        public void DestroyCurrentLevel()
        {
            Destroy(CurrentLevelController.gameObject);
        }

        private int hash(int x)
        {
            x = ((x >> 16) ^ x) * 0x45d9f3b;
            x = ((x >> 16) ^ x) * 0x45d9f3b;
            x = (x >> 16) ^ x;
            return x;
        }
    }
}
