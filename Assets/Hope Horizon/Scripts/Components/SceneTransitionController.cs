using DG.Tweening;
using HopeHorizon.Scripts.Singletons;
using UnityEngine;
using UnityEngine.Events;

namespace HopeHorizon.Scripts.Components
{
    public class SceneTransitionController : MonoBehaviourSingleton<SceneTransitionController>
    {
        [SerializeField]
        private DOTweenAnimation animationStart;

        public UnityEvent AnimationFadeInStarted;
        public UnityEvent AnimationFadeInCompleted;

        public UnityEvent AnimationFadeOutStarted;
        public UnityEvent AnimationFadeOutCompleted;

        public void Play()
        {
            OnAnimationFadeInStarted();
            animationStart.DORestartById(animationStart.id);
        }

        public void OnAnimationFadeInStarted()
        {
            AnimationFadeInStarted?.Invoke();
        }

        public void OnAnimationFadeInCompleted()
        {
            AnimationFadeInCompleted?.Invoke();
        }

        public void OnAnimationFadeOutStarted()
        {
            AnimationFadeOutStarted?.Invoke();
        }

        public void OnAnimationFadeOutCompleted()
        {
            AnimationFadeOutCompleted?.Invoke();
        }
    }
}
