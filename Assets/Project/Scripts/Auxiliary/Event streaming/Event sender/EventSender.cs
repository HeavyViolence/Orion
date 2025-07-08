using System;

namespace Orion.Auxiliary.EventStreaming
{
    public sealed class EventSender<T> : IEventSender<T>, IDisposable where T : GameEvent
    {
        private IEventLink<T> _link;

        public Guid ID { get; } = Guid.NewGuid();

        public void Bind(IEventLink<T> link) => _link ??= link;
        public void Send() => _link?.Send();
        public void Send(T gameEvent) => _link?.Send(gameEvent);
        public void ErrorOut(Exception ex) => _link?.ErrorOut(ex);
        public void Complete() => _link?.Complete();
        public void Dispose() => _link = null;
    }
}