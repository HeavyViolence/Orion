using System;

namespace Orion.Auxiliary.EventStreaming
{
    public interface IEventLink<T> where T : GameEvent
    {
        void Send();
        void Send(T gameEvent);
        void ErrorOut(Exception ex);
        void Complete();
    }
}