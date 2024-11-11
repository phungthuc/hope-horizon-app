using UnityEngine;
using UnityEngine.Events;

namespace Game.Core
{
    public class AnimationEventReceiver : MonoBehaviour
    {
        public UnityEvent AnimationStarted;
        public UnityEvent AnimationCompleted;

        public void OnAnimationStarted()
        {
            AnimationStarted?.Invoke();
        }

        public void OnAnimationCompleted()
        {
            AnimationCompleted?.Invoke();
        }
    }
}