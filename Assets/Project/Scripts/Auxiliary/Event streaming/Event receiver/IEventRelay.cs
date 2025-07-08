namespace Orion.Auxiliary.EventStreaming
{
    public interface IEventRelay<T> where T : GameEvent
    {
        void Add(IEventReceiver<T> receiver);
        void Remove(IEventReceiver<T> receiver);
    }
}