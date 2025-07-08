using System;

using UnityEngine;

namespace Orion.Auxiliary.EventStreaming
{
    public abstract class GameEvent
    {
        public Guid SenderID { get; }
        public DateTime DateTime { get; }
        public Vector2 Position { get; }

        public GameEvent(Guid senderID, Vector2 position)
        {
            SenderID = senderID;
            DateTime = DateTime.UtcNow;
            Position = position;
        }
    }
}