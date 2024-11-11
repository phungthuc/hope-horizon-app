using UnityEngine;
using UnityEngine.Events;

namespace Game.Core
{
    public class StringEventListener : MonoBehaviourEventListener<StringEvent>
    {
        [Header("String event")]
        [Tooltip("The name of the event to listen to.")]
        public string eventName = "load";
        public UnityEvent EventRaised;

        public override void OnEventTriggered(StringEvent stringEvent)
        {
            if (stringEvent.Name == eventName)
            {
                EventRaised.Invoke();
            }
        }
    }
}
