namespace Game.Core
{

    public interface IEvent
    {
    }

    public interface IEventListener
    {

    }

    public interface IEventListener<in T> : IEventListener where T : struct, IEvent
    {
        public void OnEventTriggered(T e);
    }
}
