using System;
using System.Collections.Generic;

namespace Orion.Auxiliary.EventStreaming
{
    public sealed class EventReceiver<T> : IEventReceiver<T>, IDisposable where T : GameEvent
    {
        private readonly List<Action> _actions = new();
        private readonly List<Action<T>> _parameterizedActions = new();
        private readonly List<Action<Exception>> _exceptionActions = new();
        private readonly List<Action> _completionActions = new();

        public Guid ID { get; } = Guid.NewGuid();

        public EventReceiver<T> AddBindings(params Action[] actions)
        {
            _actions.AddRange(actions);
            return this;
        }

        public EventReceiver<T> RemoveBindings(params Action[] actions)
        {
            foreach (var action in _actions)
            {
                _actions.Remove(action);
            }

            return this;
        }

        public EventReceiver<T> AddParameterizedBindings(params Action<T>[] actions)
        {
            _parameterizedActions.AddRange(actions);
            return this;
        }

        public EventReceiver<T> RemoveParameterizedBindings(params Action<T>[] actions)
        {
            foreach (var action in _parameterizedActions)
            {
                _parameterizedActions.Remove(action);
            }

            return this;
        }

        public EventReceiver<T> AddExceptionBindings(params Action<Exception>[] actions)
        {
            _exceptionActions.AddRange(actions);
            return this;
        }

        public EventReceiver<T> RemoveExceptionBindings(params Action<Exception>[] actions)
        {
            foreach (var action in _exceptionActions)
            {
                _exceptionActions.Remove(action);
            }

            return this;
        }

        public EventReceiver<T> AddCompletionBindings(params Action[] actions)
        {
            _completionActions.AddRange(actions);
            return this;
        }

        public EventReceiver<T> RemoveCompletionBindings(params Action[] actions)
        {
            foreach(var action in _completionActions)
            {
                _completionActions.Remove(action);
            }

            return this;
        }

        public void OnEvent()
        {
            foreach (var action in _actions)
            {
                action();
            }
        }

        public void OnEvent(T gameEvent)
        {
            if (gameEvent is null)
            {
                throw new ArgumentNullException(nameof(gameEvent));
            }

            foreach(var action in _parameterizedActions)
            {
                action(gameEvent);
            }
        }

        public void OnError(Exception ex)
        {
            if (ex is null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            foreach (Action<Exception> action in _exceptionActions)
            {
                action(ex);
            }
        }

        public void OnComplete()
        {
            foreach (Action action in _completionActions)
            {
                action();
            }
        }

        public void Dispose()
        {
            _actions.Clear();
            _parameterizedActions.Clear();
            _exceptionActions.Clear();
            _completionActions.Clear();
        }
    }
}