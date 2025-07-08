using System;
using System.Collections.Generic;

namespace Orion.Auxiliary.EventStreaming
{
    public sealed class EventChannel<T> : IEventLink<T>, IEventRelay<T>, IDisposable where T : GameEvent
    {
        private readonly HashSet<IEventReceiver<T>> _receivers = new(new IEventReceiverComparer<T>());

        public void Add(IEventReceiver<T> receiver) => _receivers.Add(receiver);
        public void Remove(IEventReceiver<T> receiver) => _receivers.Remove(receiver);

        public void Send()
        {
            if (_receivers.Count == 0)
            {
                return;
            }

            foreach (IEventReceiver<T> receiver in _receivers)
            {
                receiver.OnEvent();
            }
        }

        public void Send(T gameEvent)
        {
            if (_receivers.Count == 0)
            {
                return;
            }

            foreach (IEventReceiver<T> receiver in _receivers)
            {
                receiver.OnEvent(gameEvent);
            }
        }

        public void ErrorOut(Exception ex)
        {
            if (_receivers.Count == 0)
            {
                return;
            }

            foreach (IEventReceiver<T> receiver in _receivers)
            {
                receiver.OnError(ex);
            }
        }

        public void Complete()
        {
            if (_receivers.Count == 0)
            {
                return;
            }

            foreach (IEventReceiver<T> receiver in _receivers)
            {
                receiver.OnComplete();
            }
        }

        public void Dispose()
        {
            if (_receivers.Count == 0)
            {
                return;
            }

            _receivers.Clear();
        }
    }
}