using System;

namespace Orion.Auxiliary.EventStreaming
{
    public interface IEventReceiver<T> where T : GameEvent
    {
        Guid ID { get; }

        void OnEvent();
        void OnEvent(T gameEvent);
        void OnError(Exception ex);
        void OnComplete();
    }
}