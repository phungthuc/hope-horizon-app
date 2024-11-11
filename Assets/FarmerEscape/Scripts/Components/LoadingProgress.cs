using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FarmerEscape.Scripts.Components
{
    public class LoadingProgress : MonoBehaviour
    {
        [SerializeField]
        private float delayTime;

        [SerializeField]
        private float speed;

        public UnityEvent<float> ProgressUpdated;
        public UnityEvent Loaded;

        private float _progress;
        private float _currentProgress;
        private bool _isCompleted;

        public void IncreaseLoadingProgress(float amount)
        {
            if (_progress + amount <= 1.01f) // 1 is max value of progress
            {
                _progress += amount;
                _isCompleted = false;
            }
        }

        private void Update()
        {
            if (_isCompleted)
            {
                return;
            }

            if (_currentProgress >= _progress)
            {
                _isCompleted = true;
                CheckLoadingCompleted();
                return;
            }

            _currentProgress += speed * Time.timeScale;
            _currentProgress = Mathf.Clamp(_currentProgress, 0f, 1f);
            ProgressUpdated?.Invoke(_currentProgress);
        }

        private void CheckLoadingCompleted()
        {
            if (_progress >= 1f && _isCompleted)
            {
                StartCoroutine(DelayLoadingCompletion());
            }
        }

        private IEnumerator DelayLoadingCompletion()
        {
            yield return new WaitForSeconds(delayTime);
            Loaded?.Invoke();
        }
    }
}
