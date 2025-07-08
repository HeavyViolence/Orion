using System;
using System.Collections.Generic;

namespace Orion.Auxiliary.EventStreaming
{
    public sealed class IEventReceiverComparer<T> :
        IEqualityComparer<IEventReceiver<T>> where T : GameEvent
    {
        public bool Equals(IEventReceiver<T> x, IEventReceiver<T> y)
        {
            if (x is null || y is null)
            {
                return false;
            }

            return x.ID == y.ID;
        }

        public int GetHashCode(IEventReceiver<T> obj) =>
            HashCode.Combine(obj.ID);
    }
}