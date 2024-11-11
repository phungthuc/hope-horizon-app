namespace Game.Core
{
    public struct StringEvent : IEvent
    {

        public StringEvent(string name)
        {
            Name = name;
        }

        public static void Trigger(string name)
        {
            EventManager.TriggerEvent(new StringEvent(name));
        }

        public string Name { get; }
    }
}
