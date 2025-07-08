using System;
using System.Collections.Generic;

namespace Orion.Auxiliary.EventStreaming
{
    public sealed class EventDispatcher : IDisposable
    {
        private readonly Dictionary<Type, object> _channels = new();

        public EventDispatcher Register<T>(IEventSender<T> sender) where T : GameEvent
        {
            if (_channels.TryGetValue(typeof(EventChannel<T>), out object entry) == true &&
                entry is IEventLink<T> link)
            {
                sender.Bind(link);
            }
            else
            {
                EventChannel<T> newChannel = new();
                sender.Bind(newChannel);
                _channels.Add(typeof(EventChannel<T>), newChannel);
            }

            return this;
        }

        public EventDispatcher Register<T>(IEventReceiver<T> receiver) where T : GameEvent
        {
            if (_channels.TryGetValue(typeof(EventChannel<T>), out object entry) == true &&
                entry is IEventRelay<T> relay)
            {
                relay.Add(receiver);
            }
            else
            {
                EventChannel<T> newChannel = new();
                newChannel.Add(receiver);
                _channels.Add(typeof(EventChannel<T>), newChannel);
            }

            return this;
        }

        public EventDispatcher Deregister<T>(IEventReceiver<T> receiver) where T : GameEvent
        {
            if (_channels.TryGetValue(typeof(EventChannel<T>), out object entry) == true &&
                entry is IEventRelay<T> relay)
            {
                relay.Remove(receiver);
            }

            return this;
        }

        public void Dispose()
        {
            foreach (var channel in _channels.Values)
            {
                if (channel is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _channels.Clear();
        }
    }
}