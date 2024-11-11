using UnityEngine;

namespace Game.Core
{
    public abstract class MonoBehaviourEventListener<T> : MonoBehaviour, IEventListener<T> where T : struct, IEvent
    {
        public virtual void OnEnable()
        {
            EventManager.Subscribe(this);
        }

        public virtual void OnDisable()
        {
            EventManager.Unsubscribe(this);
        }

        public abstract void OnEventTriggered(T e);
    }


    public abstract class MonoBehaviourEventListener<T1, T2> : MonoBehaviourEventListener<T1>,  IEventListener<T2>
        where T1 : struct, IEvent
        where T2 : struct, IEvent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            EventManager.Subscribe<T2>(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            EventManager.Unsubscribe<T2>(this);
        }

        public abstract void OnEventTriggered(T2 e);
    }

    public abstract class MonoBehaviourEventListener<T1, T2, T3> : MonoBehaviourEventListener<T1, T2>, IEventListener<T3>
        where T1 : struct, IEvent
        where T2 : struct, IEvent
        where T3 : struct, IEvent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            EventManager.Subscribe<T3>(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            EventManager.Unsubscribe<T3>(this);
        }

        public abstract void OnEventTriggered(T3 e);
    }

    public abstract class MonoBehaviourEventListener<T1, T2, T3, T4> : MonoBehaviourEventListener<T1, T2, T3>, IEventListener<T4>
        where T1 : struct, IEvent
        where T2 : struct, IEvent
        where T3 : struct, IEvent
        where T4 : struct, IEvent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            EventManager.Subscribe<T4>(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            EventManager.Unsubscribe<T4>(this);
        }

        public abstract void OnEventTriggered(T4 e);
    }

    public abstract class MonoBehaviourEventListener<T1, T2, T3, T4, T5> : MonoBehaviourEventListener<T1, T2, T3, T4>, IEventListener<T5>
        where T1 : struct, IEvent
        where T2 : struct, IEvent
        where T3 : struct, IEvent
        where T4 : struct, IEvent
        where T5 : struct, IEvent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            EventManager.Subscribe<T5>(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            EventManager.Unsubscribe<T5>(this);
        }

        public abstract void OnEventTriggered(T5 e);
    }

    public abstract class MonoBehaviourEventListener<T1, T2, T3, T4, T5, T6> : MonoBehaviourEventListener<T1, T2, T3, T4, T5>, IEventListener<T6>
        where T1 : struct, IEvent
        where T2 : struct, IEvent
        where T3 : struct, IEvent
        where T4 : struct, IEvent
        where T5 : struct, IEvent
        where T6 : struct, IEvent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            EventManager.Subscribe<T6>(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            EventManager.Unsubscribe<T6>(this);
        }

        public abstract void OnEventTriggered(T6 e);
    }

    public abstract class MonoBehaviourEventListener<T1, T2, T3, T4, T5, T6, T7> : MonoBehaviourEventListener<T1, T2, T3, T4, T5, T6>, IEventListener<T7>
        where T1 : struct, IEvent
        where T2 : struct, IEvent
        where T3 : struct, IEvent
        where T4 : struct, IEvent
        where T5 : struct, IEvent
        where T6 : struct, IEvent
        where T7 : struct, IEvent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            EventManager.Subscribe<T7>(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            EventManager.Unsubscribe<T7>(this);
        }

        public abstract void OnEventTriggered(T7 e);
    }

    public abstract class MonoBehaviourEventListener<T1, T2, T3, T4, T5, T6, T7, T8> : MonoBehaviourEventListener<T1, T2, T3, T4, T5, T6, T7>, IEventListener<T8>
        where T1 : struct, IEvent
        where T2 : struct, IEvent
        where T3 : struct, IEvent
        where T4 : struct, IEvent
        where T5 : struct, IEvent
        where T6 : struct, IEvent
        where T7 : struct, IEvent
        where T8 : struct, IEvent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            EventManager.Subscribe<T8>(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            EventManager.Unsubscribe<T8>(this);
        }

        public abstract void OnEventTriggered(T8 e);
    }

    public abstract class MonoBehaviourEventListener<T1, T2, T3, T4, T5, T6, T7, T8, T9> : MonoBehaviourEventListener<T1, T2, T3, T4, T5, T6, T7, T8>, IEventListener<T9>
        where T1 : struct, IEvent
        where T2 : struct, IEvent
        where T3 : struct, IEvent
        where T4 : struct, IEvent
        where T5 : struct, IEvent
        where T6 : struct, IEvent
        where T7 : struct, IEvent
        where T8 : struct, IEvent
        where T9 : struct, IEvent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            EventManager.Subscribe<T9>(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            EventManager.Unsubscribe<T9>(this);
        }

        public abstract void OnEventTriggered(T9 e);
    }

    public abstract class MonoBehaviourEventListener<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : MonoBehaviourEventListener<T1, T2, T3, T4, T5, T6, T7, T8, T9>, IEventListener<T10>
        where T1 : struct, IEvent
        where T2 : struct, IEvent
        where T3 : struct, IEvent
        where T4 : struct, IEvent
        where T5 : struct, IEvent
        where T6 : struct, IEvent
        where T7 : struct, IEvent
        where T8 : struct, IEvent
        where T9 : struct, IEvent
        where T10 : struct, IEvent
    {
        public override void OnEnable()
        {
            base.OnEnable();
            EventManager.Subscribe<T10>(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            EventManager.Unsubscribe<T10>(this);
        }

        public abstract void OnEventTriggered(T10 e);
    }
}
