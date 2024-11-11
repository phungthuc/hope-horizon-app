using System;
using System.Collections.Generic;

namespace Game.Core
{
    /// <summary>
    /// This class handles event management, and can be used to broadcast events throughout the game, to tell one class (or many) that something's happened.
    /// Events are structs, you can define any kind of events you want. This manager comes with <see cref="StringEvent"/>, which are
    /// basically just made of a string, but you can work with more complex ones if you want.
    /// </summary>
    ///
    /// <example><remarks>
    ///  To trigger a new event, from anywhere, do <code>YOUR_EVENT.Trigger(YOUR_PARAMETERS)</code>
    /// For example will trigger a Save something.<code>StringEvent.Trigger("Save");</code>
    ///
    /// <para>
    /// You can also call <para><code>EventManager.TriggerEvent(YOUR_EVENT);</code></para>
    /// For example, to broadcast an StringEvent named GameStart to all listeners.<para><code>EventManager.TriggerEvent(new StringEvent("GameStart"));</code></para>
    /// </para>
    ///
    /// To start listening to an event from any class, there are 3 things you must do:
    ///<para>
    /// 1 - tell that your class implements the IEventListener interface for that kind of event.
    /// For example:
    /// <code>public class GUIManager : Singleton&lt;GUIManager&lt;IEventListener&lt;StringEvent&lt;</code>
    /// You can have more than one of these (one per event type).
    /// </para>
    /// <para>
    /// 2 - On Enable and Disable, respectively start and stop listening to the event :
    /// <code>
    /// void OnEnable()
    /// {
    ///     EventManager.Subscribe&lt;StringEvent&lt;(this);
    /// }
    /// void OnDisable()
    /// {
    ///     EventManager.UnSubscribe&lt;StringEvent&lt;(this);
    /// }
    /// </code>
    /// </para>
    ///
    /// <para>
    /// 3 - Implement the IEventListener interface for that event. For example :
    /// <code>
    /// public void OnEventRaised(StringEvent gameEvent)
    /// {
    ///     if (gameEvent.EventName == "GameOver")
    ///     {
    ///         // DO SOMETHING
    ///     }
    /// }
    /// </code>
    /// </para>
    /// will catch all events of type MMGameEvent emitted from anywhere in the game, and do something if it's named GameOver
    /// </remarks></example>
    public static class EventManager
    {
        private static readonly Dictionary<Type, List<IEventListener>> _subscribersList = new();
        private static readonly Dictionary<Type, List<IEventListener>> _oneTimeSubscribersList = new();

        /// <summary>
        /// Subscribes the specified event listener to the specified event type.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="listener">The event listener to subscribe.</param>
        public static void Subscribe<T>(IEventListener<T> listener) where T : struct, IEvent
        {
            var type = typeof(T);
            if (!_subscribersList.ContainsKey(type))
            {
                _subscribersList.Add(type, new List<IEventListener>());
            }
            _subscribersList[type].Add(listener);
        }

        /// <summary>
        /// Subscribes the specified event listener to the specified event type on a one-time basis.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="listener">The event listener to subscribe.</param>
        public static void SubscribeOnce<T>(IEventListener<T> listener) where T : struct, IEvent
        {
            var type = typeof(T);
            if (!_oneTimeSubscribersList.ContainsKey(type))
            {
                _oneTimeSubscribersList.Add(type, new List<IEventListener>());
            }
            _oneTimeSubscribersList[type].Add(listener);
        }

        /// <summary>
        /// Unsubscribes the specified event listener from the specified event type.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="listener">The event listener to unsubscribe.</param>
        public static void Unsubscribe<T>(IEventListener<T> listener) where T : struct, IEvent
        {
            var type = typeof(T);
            if (_subscribersList.ContainsKey(type))
            {
                _subscribersList[type].Remove(listener);
            }
        }

        /// <summary>
        /// Triggers the specified event and notifies all subscribed event listeners.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="eventRaiser">The event to trigger.</param>
        public static void TriggerEvent<T>(T eventRaiser) where T : struct, IEvent
        {
            var type = typeof(T);
            if (_subscribersList.ContainsKey(type))
            {
                foreach (var listener in _subscribersList[type])
                {
                    (listener as IEventListener<T>)?.OnEventTriggered(eventRaiser);
                }
            }

            if (_oneTimeSubscribersList.ContainsKey(type))
            {
                foreach (var listener in _oneTimeSubscribersList[type])
                {
                    (listener as IEventListener<T>)?.OnEventTriggered(eventRaiser);
                }
                _oneTimeSubscribersList[type].Clear();
            }
        }
    }
}
