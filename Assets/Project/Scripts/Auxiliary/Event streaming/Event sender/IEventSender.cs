using System;

namespace Orion.Auxiliary.EventStreaming
{
    public interface IEventSender<T> where T : GameEvent
    {
        Guid ID { get; }
        void Bind(IEventLink<T> link);
    }
}