using UnityEngine;
using UnityEngine.Events;

namespace HopeHorizon.Scripts.Components.Scenes
{
    public class PlayTransitionScene : MonoBehaviour
    {

        public UnityEvent AnimationFadeInStarted;
        public UnityEvent AnimationFadeInCompleted;

        public UnityEvent AnimationFadeOutStarted;
        public UnityEvent AnimationFadeOutCompleted;

        public void Play()
        {
            RegisterEvents();
            SceneTransitionController.Instance.Play();
        }

        private void OnAnimationFadeInStarted()
        {
            AnimationFadeInStarted?.Invoke();
        }

        private void OnAnimationFadeInCompleted()
        {
            AnimationFadeInCompleted?.Invoke();
        }

        private void OnAnimationFadeOutStarted()
        {
            AnimationFadeOutStarted?.Invoke();
        }

        private void OnAnimationFadeOutCompleted()
        {
            AnimationFadeOutCompleted?.Invoke();
            ClearEvents();
        }

        private void RegisterEvents()
        {
            ClearEvents();
            SceneTransitionController.Instance.AnimationFadeInStarted.AddListener(OnAnimationFadeInStarted);
            SceneTransitionController.Instance.AnimationFadeInCompleted.AddListener(OnAnimationFadeInCompleted);
            SceneTransitionController.Instance.AnimationFadeOutStarted.AddListener(OnAnimationFadeOutStarted);
            SceneTransitionController.Instance.AnimationFadeOutCompleted.AddListener(OnAnimationFadeOutCompleted);
        }

        private void ClearEvents()
        {
            SceneTransitionController.Instance.AnimationFadeInStarted.RemoveListener(OnAnimationFadeInStarted);
            SceneTransitionController.Instance.AnimationFadeInCompleted.RemoveListener(OnAnimationFadeInCompleted);
            SceneTransitionController.Instance.AnimationFadeOutStarted.RemoveListener(OnAnimationFadeOutStarted);
            SceneTransitionController.Instance.AnimationFadeOutCompleted.RemoveListener(OnAnimationFadeOutCompleted);
        }
    }
}
