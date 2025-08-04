using System;
using System.Collections.Generic;

namespace Orion.Main.Saving
{
    public sealed class ISavableEqualityComparer : IEqualityComparer<ISavable>
    {
        public bool Equals(ISavable x, ISavable y)
        {
            if (x is null || y is null)
            {
                return false;
            }

            if (ReferenceEquals(x, y) == true)
            {
                return true;
            }

            return x.StateName == y.StateName;
        }

        public int GetHashCode(ISavable obj) => HashCode.Combine(obj.StateName);
    }
}