using System;
using System.Collections.Generic;

namespace Orion.Auxiliary
{
    public static class MyMath
    {
        public static float RandomNormal => UnityEngine.Random.Range(-1f, 1f);
        public static float RandomUnit => UnityEngine.Random.Range(0f, 1f);
        public static bool RandomBool => RandomNormal > 0f;
        public static float RandomSign => RandomBool ? 1f : -1f;

        public static T RandomEnum<T>() where T : Enum
        {
            T[] values = (T[])Enum.GetValues(typeof(T));
            int seed = UnityEngine.Random.Range(0, values.Length);

            return values[seed];
        }

        public static IEnumerable<T> RandomEnum<T>(int amount) where T : Enum
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(amount)} must be positive!");
            }

            T[] values = (T[])Enum.GetValues(typeof(T));
            List<T> result = new(amount);

            for (int i = 0; i < amount; i++)
            {
                int seed = UnityEngine.Random.Range(0, values.Length);
                result.Add(values[seed]);
            }

            return result;
        }
    }
}